using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using DevStorm.Infrastructure.Core;
using DevStorm.Infrastructure.Utility;

namespace DevStorm.Infrastructure.DataAccess
{
    public class EnumMetadata
    {
        public int Id { get; set; }
        public string EnumName { get; set; }
        public int Key { get; set; }
        public string Name { get; set; }
        public string Display { get; set; }

        private static DbContext _context;

        public static void Seed(DbContext context)
        {
            _context = context;

            GetAllEnums().ForEach(type =>
            {
                var enumName = type.Name;

                Enum.GetNames(type).ForEach(name =>
                {
                    var member = type.GetMember(name).First();
                    var attrs = member.GetCustomAttributes(typeof (DisplayAttribute), false);
                    var display = attrs.Any() ? ((DisplayAttribute) attrs.First()).Name : name;
                    var key = Enum.Parse(type, name).As<int>();

                    Insert(enumName, name, display, key);
                });
            });
        }

        private static IEnumerable<Type> GetAllEnums()
        {
            return TypeService.GetTypes(
                type => type.BaseType != null && type.BaseType == typeof (Enum),
                AssemblyNameList.Domain);
        }

        private static void Insert(
            string enumName,
            string name,
            string display,
            int key)
        {
            var newEnumMetadata = new EnumMetadata
            {
                EnumName = enumName,
                Name = name,
                Display = display,
                Key = key
            };

            _context.Set<EnumMetadata>().Add(newEnumMetadata);
        }
    }
}