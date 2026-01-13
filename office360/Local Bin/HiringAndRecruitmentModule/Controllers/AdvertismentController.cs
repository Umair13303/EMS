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
    public class AdvertismentController : Controller
    {
        // GET: HiringAndRecruitmentModule/Advertisment
        HMSEntities db = new HMSEntities();

        public ActionResult HRMPublishAdvertisment()
        {
            string userRightName = "HRPublishAdvertisment";
            int? statusCode; // Declare a variable to store the StatusCode
            CommonHelper.LogInAuthentication.IsUserAuthorized(userRightName, out statusCode); // Pass the variable to the method
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
        public ActionResult HRMPublishAdvertisment(HRAdvertisments HRAdvertisment)
        {
            try
            {
                #region DOCUMENT CODE
                string DocumentCode = "";
                if (HRAdvertisment.Type == "Hiring of Employee")
                {
                    DocumentCode = "HR-PB" + '-' + DateTime.Now.ToShortDateString().Replace("/", "").Replace("0", "");
                }
                else if (HRAdvertisment.Type == "Equipment Procurment")
                {
                    DocumentCode = "EP-PB" + '-' + DateTime.Now.ToLongTimeString().Replace(":", "").Replace("PM", "");
                }
                #endregion
                string result = InsertIntoDB.InsertIntoHRMAdvertisment(HRAdvertisment, DocumentCode, out int? StatusCode);
                #region STATUS CODE RESPONSE

                DBStatusMessages.GetMessage(StatusCode, HRAdvertisment.Title);
                string Message = DBStatusMessages.GetMessage(StatusCode, HRAdvertisment.Title);
                return Json(new { StatusCode = StatusCode, Message = Message }, JsonRequestBehavior.AllowGet);
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        public ActionResult ViewAdvertismentList()
        {
            string userRightName = "ViewAdvertismentList";
            int? statusCode; // Declare a variable to store the StatusCode
            CommonHelper.LogInAuthentication.IsUserAuthorized(userRightName, out statusCode); // Pass the variable to the method
            if (statusCode == 200)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginPage", "Home");
            }

        }
        public ActionResult PopulateAdvertisments(HtmlViewerFilters Filters)
        {
            var data = ListHelper.PopulateAdvertisments(Filters);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

    }
}
