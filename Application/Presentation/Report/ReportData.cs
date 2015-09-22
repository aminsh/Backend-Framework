using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Presentation.App.Report
{
    public class ReportData
    {
        public ReportData()
        {
            ReportCategories = new List<ReportCategory>();
            ReportPath = ConfigurationManager.AppSettings["ReportPath"];
            ConnectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ToString();

            var categoryOne = new ReportCategory("Category one");
            ReportCategories.Add(categoryOne);
            categoryOne.AddReport("Report one", "3756C7D9-5DC9-400B-981F-A03D13DACC34.mrt");
            categoryOne.AddReport("Report one", "3756C7D9-5DC9-400B-981F-A03D13DACC34.mrt");
            categoryOne.AddReport("Report one", "3756C7D9-5DC9-400B-981F-A03D13DACC34.mrt");
            categoryOne.AddReport("Report one", "3756C7D9-5DC9-400B-981F-A03D13DACC34.mrt");
            categoryOne.AddReport("Report one", "3756C7D9-5DC9-400B-981F-A03D13DACC34.mrt");
            categoryOne.AddReport("Report one", "3756C7D9-5DC9-400B-981F-A03D13DACC34.mrt");
        }

        public string ReportPath { get; private set; }

        public string ConnectionString { get; private set; }

        public IList<ReportCategory> ReportCategories { get; private set; }
    }
}