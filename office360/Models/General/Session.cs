using office360.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.General
{
    public static class Session_Manager
    {
        public static string Name{get { return HttpContext.Current.Session["Name"] as string; }set { HttpContext.Current.Session["Name"] = value; }}
        public static string UserName{get { return HttpContext.Current.Session["UserName"] as string; }set { HttpContext.Current.Session["UserName"] = value; }}
        public static string Password{get { return HttpContext.Current.Session["Password"] as string; }set { HttpContext.Current.Session["Password"] = value; }}
        public static string EmailAddress{get { return HttpContext.Current.Session["EmailAddress"] as string; }set { HttpContext.Current.Session["EmailAddress"] = value; }}
        public static string MobileNumber{get { return HttpContext.Current.Session["MobileNumber"] as string; }set { HttpContext.Current.Session["MobileNumber"] = value; }}
        public static bool? IsLogIn {get { return (bool?) HttpContext.Current.Session["IsLogIn"] ; }set { HttpContext.Current.Session["IsLogIn"] = value; }}
        public static string AllowedCampusIds { get { return HttpContext.Current.Session["AllowedCampusIds"] as string; }set { HttpContext.Current.Session["AllowedCampusIds"] = value; }}
        public static DateTime CreatedOn{get { return (DateTime)HttpContext.Current.Session["CreatedOn"]; }set { HttpContext.Current.Session["CreatedOn"] = value; }}
        public static DateTime UpdatedOn{get { return (DateTime)HttpContext.Current.Session["CreatedOn"]; }set { HttpContext.Current.Session["CreatedOn"] = value; }}
        public static bool? IsDeveloper{get { return (bool?)HttpContext.Current.Session["IsDeveloper"]; }set { HttpContext.Current.Session["IsDeveloper"] = value; }}
        public static int? UserId
        {
            get
            {
                if (HttpContext.Current.Session["UserId"] is int UserId)
                {
                    return UserId;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session["UserId"] = value;
            }
        }
        public static int? RoleId
        {
            get
            {
                if (HttpContext.Current.Session["RoleId"] is int RoleId)
                {
                    return RoleId;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session["RoleId"] = value;
            }
        }
        public static Guid? CreatedBy
        {
            get
            {
                if (HttpContext.Current.Session["CreatedBy"] is Guid CreatedBy)
                {
                    return CreatedBy;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session["CreatedBy"] = value;
            }
        }
        public static Guid? UpdatedBy
        {
            get
            {
                if (HttpContext.Current.Session["UpdatedBy"] is Guid UpdatedBy)
                {
                    return UpdatedBy;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session["UpdatedBy"] = value;
            }
        }
        public static Guid? EmployeeId
        {
            get
            {
                if (HttpContext.Current.Session["EmployeeId"] is Guid EmployeeId)
                {
                    return EmployeeId;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session["EmployeeId"] = value;
            }
        }
        public static int? BranchId
        {
            get
            {
                if (HttpContext.Current.Session["BranchId"] is int BranchId)
                {
                    return BranchId;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session["BranchId"] = value;
            }
        }
        #region COMPANY SESSION
        public static int? CompanyId
        {
            get
            {
                if (HttpContext.Current.Session["CoyId"] is int CompanyId)
                {
                    return CompanyId;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session["CoyId"] = value;
            }
        }
        public static Guid? CityId
        {
            get
            {
                if (HttpContext.Current.Session["CityId"] is Guid CityId)
                {
                    return CityId;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session["CityId"] = value;
            }
        }
        public static Guid? CountryId
        {
            get
            {
                if (HttpContext.Current.Session["CountryId"] is Guid CountryId)
                {
                    return CountryId;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session["CityId"] = value;
            }
        }
        public static string CompanyName
        {
            get { return HttpContext.Current.Session["CompanyName"] as string; }
            set { HttpContext.Current.Session["CompanyName"] = value; }
        }
        public static string AddressLine
        {
            get { return HttpContext.Current.Session["AddressLine"] as string; }
            set { HttpContext.Current.Session["AddressLine"] = value; }
        }
        public static string PhoneNumber
        {
            get { return HttpContext.Current.Session["PhoneNumber"] as string; }
            set { HttpContext.Current.Session["PhoneNumber"] = value; }
        }
        public static string CoyEmailAddress
        {
            get { return HttpContext.Current.Session["CoyEmailAddress"] as string; }
            set { HttpContext.Current.Session["CoyEmailAddress"] = value; }
        }
        public static string CompanyWebsite
        {
            get { return HttpContext.Current.Session["CompanyWebsite"] as string; }
            set { HttpContext.Current.Session["CompanyWebsite"] = value; }
        }
        public static string UploadLogo
        {
            get { return HttpContext.Current.Session["UploadLogo"] as string; }
            set { HttpContext.Current.Session["UploadLogo"] = value; }
        }
        public static bool? Status
        {
            get { return (bool?)HttpContext.Current.Session["Status"]; }
            set { HttpContext.Current.Session["Status"] = value; }
        }
        public static string CityName
        {
            get { return HttpContext.Current.Session["CityName"] as string; }
            set { HttpContext.Current.Session["CityName"] = value; }
        }
        public static string CountryName
        {
            get { return HttpContext.Current.Session["CountryName"] as string; }
            set { HttpContext.Current.Session["CountryName"] = value; }
        }
        public static string CallingCode
        {
            get { return HttpContext.Current.Session["CallingCode"] as string; }
            set { HttpContext.Current.Session["CallingCode"] = value; }
        }
        public static string BranchName
        {
            get { return HttpContext.Current.Session["BranchName"] as string; }
            set { HttpContext.Current.Session["BranchName"] = value; }
        }
        public static string BranchAddress
        {
            get { return HttpContext.Current.Session["BranchAddress"] as string; }
            set { HttpContext.Current.Session["BranchAddress"] = value; }
        }
        public static string BranchCity
        {
            get { return HttpContext.Current.Session["BranchCity"] as string; }
            set { HttpContext.Current.Session["BranchCity"] = value; }
        }
        #endregion

    }

}