using office360.Models.EDMX;
using System.Web.Mvc;
using office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper;
using office360.Areas.HiringAndRecruitmentModule.Data;

namespace office360.Areas.HiringAndRecruitmentModule.Controllers
{
    public class ReportsController : Controller
    {
        private readonly HMSEntities db;

        public ReportsController()
        {
            db = new HMSEntities(); // Instantiate the HMSEntities here
        }

        // GET: CRMModuleAEO/Reports
        public ActionResult EmployeeReportByCode(HtmlViewerFilters Filters)
        {
            ReportsHelper ReportsHelper = new ReportsHelper();
            byte[] ReportBytes = ReportsHelper.GenerateEmployeeReport(Filters, out byte[] ReportFile);
            return File(ReportBytes, "application/pdf");
        }
        public ActionResult AdvertismentReportbyCode(HtmlViewerFilters Filters)
        {
            ReportsHelper ReportsHelper = new ReportsHelper();
            byte[] ReportBytes = ReportsHelper.GenerateAdvertismentReport(Filters, out byte[] ReportFile);
            return File(ReportBytes, "application/pdf");
        }

        public ActionResult CVReportApplicantByCode(HtmlViewerFilters Filters)
        {
            int CompanyId = (int)Session["CompanyId"];
            ReportsHelper ReportsHelper = new ReportsHelper();
            byte[] ReportBytes = ReportsHelper.GenerateApplicantCVReport(Filters, out byte[] ReportFile);
            return File(ReportBytes, "application/pdf");
        }


    }
}