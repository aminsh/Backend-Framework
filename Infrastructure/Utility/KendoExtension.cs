using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Kendo.DynamicLinq;
using Newtonsoft.Json;
using Utility.Attributes;

namespace Utility
{
    public static class KendoExtension
    {
        public static DataSourceResult ToDataSourceResult<T, TResult>(this IQueryable<T> queryable,
            DataSourceRequest request, Func<T, TResult> mapper)
        {
            if (request.Filter != null && request.Filter.Field == null && !request.Filter.Filters.Any())
                request.Filter = null;

            var result = queryable.ToDataSourceResult(request);
            var data = result.Data.As<IEnumerable<T>>();

            result.Data = data.Select(mapper);

            return result;
        }

        public static DataSourceRequest ToDataSourceRequest(this HttpRequestMessage request)
        {
            try
            {
                return JsonConvert.DeserializeObject<DataSourceRequest>(
                    request.RequestUri.ParseQueryString().GetKey(0)
                    );
            }
            catch (Exception e)
            {
                return new DataSourceRequest();
            }
        }

        public static DataSourceResult ToDataSourceResult<T>(
            this DataSourceRequest request,
            string storedProcedureName) where T : class, new()
        {
            var parameters = new SqlParameter[]
            {
                GetFilter<T>(request.Filter == null ? new List<Filter>() : request.Filter.Filters),
                GetSort<T>(request.Sort ?? new List<Sort>()),
                new SqlParameter("@intTake", request.Take == 0 ? 1000000 : request.Take),
                new SqlParameter("@intSkip", request.Skip)
            };

            var ds = new SqlHelper().RunProcedure(
                storedProcedureName,
                parameters);

            var total = ds.Tables[0].Rows[0][0].As<int>();
            var data = ds.Tables[1].ToList<T>();


            return new DataSourceResult
            {
                Data = data,
                Total = total
            };
        }

        public static void Add(this Filter filter, Filter newFilter)
        {
            if (filter == null)
                filter = new Filter() {Filters = new List<Filter>()};
            if (filter.Filters == null)
                filter.Filters = new List<Filter>();

            var filterList = filter.Filters.ToList();
            filterList.Add(filter);

            filter.Filters = filterList;
        }

        public static DataSourceRequest AddFilter(DataSourceRequest request, string field, string opr, object value)
        {
            var filter = request.Filter ?? new Filter() {Filters = new List<Filter>()};

            if (filter.Filters == null)
                filter.Filters = new List<Filter>();

            var filterList = filter.Filters.ToList();
            filterList.Add(new Filter {Field = field, Operator = opr, Value = value});
            filter.Filters = filterList;
            request.Filter = filter;
            return request;
        }

        private static SqlParameter GetFilter<T>(IEnumerable<Filter> filter)
        {
            var filters = new List<string>();

            filter.ForEach(f =>
            {
                var prop = typeof (T).GetProperty(f.Field.ToPascalCase());

                if (prop == null)
                {
                    filters.Add(string.Format("{0}{1}{2}",
                        f.Field,
                        GetOperator(f.Operator),
                        f.Value));
                }
                else
                {
                    var mapAttr = prop.GetCustomAttribute<MapFieldAttribute>();


                    if (mapAttr.MapType == MapType.ForeignKey)
                    {
                        filters.Add(string.Format("{0} = {1}", mapAttr.Name, f.Value));
                    }

                    if (mapAttr.MapType == MapType.Date)
                    {
                        filters.Add(string.Format("{0} {1} {2}",
                            mapAttr.Name ?? f.Field,
                            GetOperator(f.Operator),
                            f.Value));
                    }

                    if (mapAttr.MapType == MapType.Integer)
                    {
                        filters.Add(string.Format("{0}{1}{2}",
                            mapAttr.Name ?? f.Field,
                            GetOperator(f.Operator),
                            f.Value
                            ));
                    }

                    if (mapAttr.MapType == MapType.String)
                    {
                        filters.Add(string.Format("{0} LIKE N'%{1}%'",
                            mapAttr.Name ?? f.Field,
                            f.Value
                            ));
                    }

                    if (mapAttr.MapType == MapType.Enum)
                    {
                        filters.Add(string.Format("{0} = {1}",
                            mapAttr.Name ?? f.Field,
                            f.Value));
                    }
                }
            });

            return filters.Any()
                ? new SqlParameter("@strCondition", string.Join(" AND ", filters.ToArray()))
                : new SqlParameter("@strCondition", DBNull.Value);
        }

        private static SqlParameter GetSort<T>(IEnumerable<Sort> sort)
        {
            var sorts = new List<string>();

            sort.ForEach(s =>
            {
                var mapAttr = typeof (T).GetProperty(s.Field.ToPascalCase()).GetCustomAttribute<MapFieldAttribute>();

                sorts.Add(string.Format("{0} {1}",
                    mapAttr.Name ?? s.Field,
                    s.Dir
                    ));
            });

            return sorts.Any()
                ? new SqlParameter("@strSort", string.Join(",", sorts.ToArray()))
                : new SqlParameter("@strSort", DBNull.Value);
        }

        private static string GetOperator(string opr)
        {
            string targetOpr;

            switch (opr)
            {
                case "eq":
                    targetOpr = "=";
                    break;
                case "lt":
                    targetOpr = "<";
                    break;
                case "lte":
                    targetOpr = ">";
                    break;
                case "gt":
                    targetOpr = ">";
                    break;
                case "gte":
                    targetOpr = ">=";
                    break;
                default:
                    targetOpr = "";
                    break;
            }

            return targetOpr;
        }
    }
}