using office360.Areas.HiringAndRecruitmentModule.Data;
using office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper.ListHelper;


namespace office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper
{
    public class ListHelper
    {
        public static List<AEOEvents_GetList_Result> GetAEOEventsList(HtmlViewerFilters Filters)
        {
            using (var db = new HMSEntities())
            {
                var companyIdParam = new SqlParameter("@CompanyId", Sessions.CompanyId);
                var data = db.Database.SqlQuery<AEOEvents_GetList_Result>("AEOEvents_GetList @CompanyId", companyIdParam).ToList();
                return data;
            }
        }

        // Function to get HRM advertisements list
        public static List<HRAdvertisment_GetListByParameter_Result> PopulateAdvertisments(HtmlViewerFilters Filters)
        {
            using (var db = new HMSEntities())
            {
                var companyIdParam = new SqlParameter("@CompanyId", Sessions.CompanyId);
                var data = db.Database.SqlQuery<HRAdvertisment_GetListByParameter_Result>(
                    "HRAdvertisment_GetListByParameter @CompanyId",
                    companyIdParam
                ).ToList();
                return data;
            }
        }

        // Function to get HRM applicants list by advertisement code
        public static List<HRApplicants_GetListByParameter_Result> GetApplicantsByAdvertismentCode(HtmlViewerFilters Filters)
        {
            using (var db = new HMSEntities())
            {
                var param1 = new SqlParameter("@CaseType", 1);
                var param2 = new SqlParameter("@CompanyId", Sessions.CompanyId == null ? DBNull.Value : (object)Sessions.CompanyId);
                var param3 = new SqlParameter("@AdvertismentCode", Filters.AdvertismentCode == null ? DBNull.Value : (object)Filters.AdvertismentCode);
                var param4 = new SqlParameter("@ApplicantCode", Filters.ApplicantCode == null? DBNull.Value :(object)Filters.ApplicantCode);
                var data = db.Database.SqlQuery<HRApplicants_GetListByParameter_Result>(
                    "HRApplicants_GetListByParameter @CaseType, @CompanyId, @AdvertismentCode, @ApplicantCode",
                    param1,
                    param2,
                    param3,
                    param4
                ).ToList();
                return data;
            }
        }
       

        // Function to get HRM interview list
        public static List<HRInterviews_GetListByParamters_Result> GetInterViewList(HtmlViewerFilters Filters)
        {
            using (var db = new HMSEntities())
            {
                var param1 = new SqlParameter("@CaseType", 1);
                var param2 = new SqlParameter("@CompanyId", Sessions.CompanyId == null ? DBNull.Value : (object)Sessions.CompanyId);
                var param3 = new SqlParameter("@InterViewCode", Filters.InterViewCode == null ? DBNull.Value : (object)Filters.InterViewCode);
                var data = db.Database.SqlQuery<HRInterviews_GetListByParamters_Result>(
                    "HRInterviews_GetListByParamters @CaseType, @CompanyId,@InterViewCode",
                    param1,
                    param2,
                    param3
                ).ToList();
                return data;
            }
        }

        // Function to get employee list
        public static List<Employees_GetByParameter_Result> GetEmployeeList(int companyId)
        {
            using (var db = new HMSEntities())
            {
                var data = db.Employees_GetByParameter(companyId,null).ToList();
                return data;
            }
        }
    }
}