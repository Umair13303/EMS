using office360.Areas.HiringAndRecruitmentModule.Data;
using office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper;
using office360.CommonHelper;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Areas.HiringAndRecruitmentModule.Controllers
{
    public class ApplicantsController : Controller
    {
        // GET: HiringAndRecruitmentModule/
        // 
        public ActionResult CreateResumeForJobTitle(string AdvertismentCode)
        {
            string userRightName = "CreateResumeForJobTitle";
            int? statusCode; // Declare a variable to store the StatusCode
            CommonHelper.LogInAuthentication.IsUserAuthorized(userRightName, out statusCode); // Pass the variable to the method
            ViewBag.AdvertismentCode = AdvertismentCode;
            if (statusCode == 200)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginPage", "Home");
            }
        }

        [HttpPost]
        public ActionResult CreateResumeForJobTitle(HRApplicants HRApplicants, List<HREducationDetails> HREducationDetails, List<HREmploymentHistory> HREmploymentHistory)
        {
            string Message = "";
            try
            {
                #region DOCUMENT CODE
                string DocumentCode = "APL" + '-' + DateTime.Now.ToShortDateString().Replace("/", "").Replace("0", "");
                #endregion DOCUMENT CODE
                string result = InsertIntoDB.InsertIntoHRApplicants(HRApplicants, HREducationDetails, HREmploymentHistory, DocumentCode, out int? StatusCode);
                #region STATUS CODE RESPONSE
                DBStatusMessages.GetMessage(StatusCode, HRApplicants.Name);
                Message = DBStatusMessages.GetMessage(StatusCode, HRApplicants.Name);
                return Json(new { StatusCode = StatusCode, Message = Message }, JsonRequestBehavior.AllowGet);
                #endregion
            }
            catch(Exception Ex)
            {
                DBStatusMessages.GetMessage(500, HRApplicants.Name);
                Message = DBStatusMessages.GetMessage(500, HRApplicants.Name);
                return Json(new { StatusCode = 500, Message = Message }, JsonRequestBehavior.AllowGet);

            }



        }

        public ActionResult ApplicantsListForJob(HtmlViewerFilters Filters)
        {
            string userRightName = "ApplicantsListJobWise";
            int? statusCode; // Declare a variable to store the StatusCode
            CommonHelper.LogInAuthentication.IsUserAuthorized(userRightName, out statusCode); // Pass the variable to the method
            ViewBag.AdvertismentCode = Filters.AdvertismentCode;
            if (statusCode == 200)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginPage", "Home");
            }
        }
        public ActionResult GetAdvertismentDetailsByParamters(HtmlViewerFilters Filters)
        {
            var data = FrontDetailsHelper.GetAdvertismentDetailsByParamters(Filters);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
    }
}