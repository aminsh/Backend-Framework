using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class ReportMngrController : Controller
    {
        //
        // GET: /Report/
        public ActionResult Index(string category, string reportName, string parameters)
        {
            //new ReportDataInitializer().Run();

            //return new EmptyResult();

            //Session["category"] = category;
            //Session["reportName"] = reportName;
            //Session["parameters"] = parameters  == null ? null : ObjectExtention.Deserialize<IEnumerable<Parameter>>(parameters);
           
            return View();
        }

        public ActionResult Reports()
        {
            // var category = Session["category"] as string;
            //var reportName = Session["reportName"] as string;
            //var dbObject = new DbObject();
            
            //var reportData = new ReportDataServiceXml().Reports(category).First(rpt => rpt.Name == reportName);
            //dbObject.RunProcedure(reportData.StoredProcedure,
            //    new IDataParameter[]
            //    {
            //        new SqlParameter("@viewName", "vm_employee" + DependencyManager.Resolve<ICurrent>().UserId),
            //    });
            //return new EmptyResult();

            return null;

        }

        public ActionResult GetReportSnapshot()
        {
            //var reportMngr = new ReportDataServiceXml();

            //var category = Session["category"] as string;
            //var reportName = Session["reportName"] as string;
            //var parameters = Session["parameters"] as IEnumerable<Parameter> ?? new List<Parameter>();
            
            //var reportdata = reportMngr.Reports(category).First(rpt=> rpt.Name == reportName);
            //var viewName = string.Format("{0}_{1}",reportdata.ViewName ,DependencyManager.Resolve<ICurrent>().UserId.ToString("N"));
            
            //GenerateStoredProcedure.GenerateView(
            //    viewName, 
            //    reportdata.StoredProcedure, 
            //     parameters.Select(p => new SqlParameter(p.Name, p.Value)).ToList()
            //    );

            //var stimulHelper = new StimulHelper(
            //    Path.Combine(ConfigurationManager.AppSettings["reportFile"], reportdata.FileName),
            //    ConfigurationManager.ConnectionStrings["dbConnection"].ToString());

            //var stimulHelper = new StimulHelper(
            //    Path.Combine(ConfigurationManager.AppSettings["reportFile"], "Report.mrt"),
            //    ConfigurationManager.ConnectionStrings["dbConnection"].ToString());

            //return StiMvcViewer.GetReportSnapshotResult(HttpContext, stimulHelper.GetReport());

            return null;
        }

        public ActionResult ActionViewerEvent()
        {
            //return StiMvcViewer.ViewerEventResult(HttpContext);
            return null;
        }
	}
}