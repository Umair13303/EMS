using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.General
{

    public static class _Managment
    {
        public static string RoleId { get; set; }
    }
    public static class _Controller
    {
        public static string Home = "Home";

        public static string Challan = "MChallanUI";
        public static string Fee = "MFeeUI";
        public static string AcademicSession = "MAcademicSessionUI";
        public static string Branch = "MBranchUI";
        public static string Class = "MClassUIController";
        public static string Enrollment = "MEnrollmentUIController";

    }
    public static class _Areas
    {
        public static string Account = "AAccount";
        public static string Company = "ACompany";
        public static string Student = "AStudent";
    }
    public class _ActionsURL
    {
        #region CONSUMABLE PAGE 
        public static string InternalServerError = "GetInternalServerError";
        public static string DataDoesNotExist = "DataDoesNotExist";
        public static string LogIn = "LogInPage";
        public static string Logout = "/Home/Logout";
        public static string GET_DASHBOARD_FOR_USER = "GET_DASHBOARD_FOR_USER";

        #endregion
    }
    public class DASHBOARD_BY_ROLEID
    {
        #region DASH BOARD
        public static string GetDashBoard_DEO = "GetDashBoard_DEO";
        public static string GetDashBoard_MANAGER = "GetDashBoard_MANAGER";
        public static string GetDashBoard_DEVELOPER = "GetDashBoard_DEVELOPER";
        public static string GetDashBoard_ADMIN = "GetDashBoard_ADMIN";
        #endregion


    }


}
