using office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper;
using office360.Models.EDMX;
using office360.Areas.HiringAndRecruitmentModule.Data;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper.ListHelper;
namespace office360.Areas.HiringAndRecruitmentModule.Controllers
{
    public class ListsController : Controller
    {
        HMSEntities db = new HMSEntities();
        public ActionResult AEOEventsList(HtmlViewerFilters Filter)
        {
            var data = ListHelper.GetAEOEventsList(Filter);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        // GET: CRMModuleAEO/Lists/PopulateApplicantsByAdvertismentCode
        public ActionResult PopulateApplicantsByAdvertismentCode(HtmlViewerFilters Filters)
        {
            var data = ListHelper.GetApplicantsByAdvertismentCode(Filters);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        public FileResult Download(string AdvertismentCode, string ApplicantCode)
        {
            ImagePaths ImagePaths = new ImagePaths();
            string filePath = Server.MapPath("/" + AdvertismentCode + "_" + ApplicantCode + ".pdf");
            return File(filePath, "application/pdf", "CV.pdf");
        }



        // GET: CRMModuleAEO/Lists/PopulateInterViewList
        public ActionResult PopulateInterViewList(HtmlViewerFilters Filters)
        {
            var data = ListHelper.GetInterViewList(Filters);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        // GET: CRMModuleAEO/Lists/PopulateEmployeeList
        public ActionResult PopulateEmployeeList()
        {
            int companyId = (int)Session["CompanyId"];
            var data = ListHelper.GetEmployeeList(companyId);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
    }
}