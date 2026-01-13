using office360.Areas.HiringAndRecruitmentModule.Data;
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
    public class ViewFormsController : Controller
    {
        HMSEntities db = new HMSEntities();

      
        public ActionResult ScheduleInterView(HtmlViewerFilters Filters)
        {
            string userRightName = "/ScheduleInterView";
            int? statusCode; // Declare a variable to store the StatusCode
            CommonHelper.LogInAuthentication.IsUserAuthorized(userRightName, out statusCode); // Pass the variable to the method
            ViewBag.AdvertismentCode = Filters.AdvertismentCode;
            ViewBag.ApplicantCode = Filters.ApplicantCode;
            if (statusCode == 200)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginPage", "Home");
            }
        }
        public ActionResult InterViewList()
        {
            string userRightName = "/InterViewList";
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
        public ActionResult AEOAssementSetup()
        {
            string userRightName = "/AEOAssementSetup";
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
        public ActionResult AEOAssementSetupAssignUsers(string Code)
        {
            #region CALLING STORE PROCEDURE TO VERIFY THAT IS USER AUTHORIZED TIO VIEW FORM
            var SessionUsersEmail = Session["UsersEmail"];
            var SessionPassword = Session["UsersPassword"];
            var SessionRequestedForm_Name = "AEOAssementSetupAssignUsers";
            var SessionCompanyId = Session["CompanyId"];
            if (SessionUsersEmail != null && Session != null && SessionCompanyId != null)
            {
                var @param1 = new SqlParameter("@Email", SessionUsersEmail == null ? DBNull.Value : (object)SessionUsersEmail);
                var @param2 = new SqlParameter("@Password", SessionPassword == null ? DBNull.Value : (object)SessionPassword);
                var @param3 = new SqlParameter("@FormName", SessionRequestedForm_Name);
                var @param4 = new SqlParameter("@ImplementedCompanyId", SessionCompanyId == null ? DBNull.Value : (object)SessionCompanyId);
                var data = db.Database.SqlQuery<MenuUsersFormSetting_AccessControl_Result>("MenuUsersFormSetting_AccessControl @Email, @Password, @FormName,@ImplementedCompanyId", @param1, @param2, @param3, @param4).ToList();
                if (data.Count > 0)
                {
                    var Authroization = data.FirstOrDefault();
                    var LoggedInEmail = Authroization.Email.ToString();
                    var LoggedInPassword = Authroization.Password;
                    if (LoggedInEmail != null && LoggedInPassword != null)
                    #endregion
                    {
                        ViewBag.EventCode = Code;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Home/LoginPage");
                    }
                }
            }
            return RedirectToAction("Home/LoginPage");

        }
        public ActionResult AddHRMSalaryAllowances()
        {
            #region CALLING STORE PROCEDURE TO VERIFY THAT IS USER AUTHORIZED TIO VIEW FORM
            var SessionUsersEmail = Session["UsersEmail"];
            var SessionPassword = Session["UsersPassword"];
            var SessionRequestedForm_Name = "/AddHRMSalaryAllowances";
            var SessionCompanyId = Session["CompanyId"];
            if (SessionUsersEmail != null && Session != null && SessionCompanyId != null)
            {
                var @param1 = new SqlParameter("@Email", SessionUsersEmail == null ? DBNull.Value : (object)SessionUsersEmail);
                var @param2 = new SqlParameter("@Password", SessionPassword == null ? DBNull.Value : (object)SessionPassword);
                var @param3 = new SqlParameter("@FormName", SessionRequestedForm_Name);
                var @param4 = new SqlParameter("@ImplementedCompanyId", SessionCompanyId == null ? DBNull.Value : (object)SessionCompanyId);
                var data = db.Database.SqlQuery<MenuUsersFormSetting_AccessControl_Result>("MenuUsersFormSetting_AccessControl @Email, @Password, @FormName,@ImplementedCompanyId", @param1, @param2, @param3, @param4).ToList();
                if (data.Count > 0)
                {
                    var Authroization = data.FirstOrDefault();
                    var LoggedInEmail = Authroization.Email.ToString();
                    var LoggedInPassword = Authroization.Password;
                    if (LoggedInEmail != null && LoggedInPassword != null)
                    #endregion
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Home/LoginPage");
                    }
                }
            }
            return RedirectToAction("Home/LoginPage");

        }
        public ActionResult AddEmployee()
        {
          
            return View();

        }

        public ActionResult EmployeeList()
        {
            return View();

        }
        public ActionResult AddSalaryStructureEmployee(string Code)
        {
            #region CALLING STORE PROCEDURE TO VERIFY THAT IS USER AUTHORIZED TIO VIEW FORM
            var SessionUsersEmail = Session["UsersEmail"];
            var SessionPassword = Session["UsersPassword"];
            var SessionRequestedForm_Name = "AddSalaryStructureEmployee";
            var SessionCompanyId = Session["CompanyId"];
            if (SessionUsersEmail != null && Session != null && SessionCompanyId != null)
            {
                var @param1 = new SqlParameter("@Email", SessionUsersEmail == null ? DBNull.Value : (object)SessionUsersEmail);
                var @param2 = new SqlParameter("@Password", SessionPassword == null ? DBNull.Value : (object)SessionPassword);
                var @param3 = new SqlParameter("@FormName", SessionRequestedForm_Name);
                var @param4 = new SqlParameter("@ImplementedCompanyId", SessionCompanyId == null ? DBNull.Value : (object)SessionCompanyId);
                var data = db.Database.SqlQuery<MenuUsersFormSetting_AccessControl_Result>("MenuUsersFormSetting_AccessControl @Email, @Password, @FormName,@ImplementedCompanyId", @param1, @param2, @param3, @param4).ToList();
                if (data.Count > 0)
                {
                    var Authroization = data.FirstOrDefault();
                    var LoggedInEmail = Authroization.Email.ToString();
                    var LoggedInPassword = Authroization.Password;
                    if (LoggedInEmail != null && LoggedInPassword != null)
                    #endregion
                    {
                        ViewBag.EmployeeCode = Code;

                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Home/LoginPage","");
                    }
                }
            }
            return RedirectToAction("Home/LoginPage","");

        }
    }
}