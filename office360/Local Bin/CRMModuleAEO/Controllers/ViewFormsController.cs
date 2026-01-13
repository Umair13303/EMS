using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Areas.CRMModuleAEO.Controllers
{
    public class ViewFormsController : Controller
    {
        HMSEntities db = new HMSEntities();
        // GET: CRMModuleAEO/ViewForms
        public ActionResult AEOAssementSetup()
        {
            #region CALLING STORE PROCEDURE TO VERIFY THAT IS USER AUTHORIZED TIO VIEW FORM
            var SessionUsersEmail = Session["UsersEmail"];
            var SessionPassword = Session["UsersPassword"];
            var SessionRequestedForm_Name = "AEOAssementSetup";
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
        public ActionResult AddEmployee()
        {
            #region CALLING STORE PROCEDURE TO VERIFY THAT IS USER AUTHORIZED TIO VIEW FORM
            var SessionUsersEmail = Session["UsersEmail"];
            var SessionPassword = Session["UsersPassword"];
            var SessionRequestedForm_Name = "AddEmployee";
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
        public ActionResult EmployeeList()
        {
            #region CALLING STORE PROCEDURE TO VERIFY THAT IS USER AUTHORIZED TIO VIEW FORM
            var SessionUsersEmail = Session["UsersEmail"];
            var SessionPassword = Session["UsersPassword"];
            var SessionRequestedForm_Name = "EmployeeList";
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
                        return RedirectToAction("Home/LoginPage","");
                    }
                }
            }
            return RedirectToAction("Home/LoginPage","");

        }
    }
}