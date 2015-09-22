using System.Collections.Generic;

namespace Presentation.App.Report
{
    public class ReportCategory
    {
        public ReportCategory(string name)
        {
            Name = name;
            Reports = new List<Report>();
        }

        public string Name { get; set; }
        public IList<Report> Reports { get; set; }

        public void AddReport(string name, string fileName)
        {
            Reports.Add(new Report {Name = name, FileName = fileName});
        }
    }

    public class Report
    {
        public string Name { get; set; }
        public string FileName { get; set; }
    }
}