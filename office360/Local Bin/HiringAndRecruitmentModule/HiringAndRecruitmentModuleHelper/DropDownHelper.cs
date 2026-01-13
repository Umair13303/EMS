using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper
{
    public class DropDownHelper
    {
        HMSEntities db = new HMSEntities();

        public static List<AEOEvents_GetList_Result> GetAEOEventsList(int CompanyId)
        {
            using (var db = new HMSEntities())
            {
                var companyIdParam = new SqlParameter("@CompanyId", CompanyId);
                var data = db.Database.SqlQuery<AEOEvents_GetList_Result>("AEOEvents_GetList @CompanyId", companyIdParam).ToList();
                return data;
            }
        }
        public static List<HRAdvertisment_GetListByParameter_Result> GetAdvertisements(int CompanyId, string AdvertisementCode)
        {
            using (var db = new HMSEntities())
            {
                var companyIdParam = new SqlParameter("@CompanyId", CompanyId);
                var hrmAdCodeParam = new SqlParameter("@HRMADCode", AdvertisementCode ?? DBNull.Value.ToString());
                var data = db.Database.SqlQuery<HRAdvertisment_GetListByParameter_Result>(
                    "HRAdvertisment_GetListByParameter @CompanyId, @HRMADCode",
                    companyIdParam,
                    hrmAdCodeParam
                ).ToList();
                return data;
            }
          
        }
        public static List<HRApplicants_GetListByParameter_Result> GetApplicantsByAdvertismentCode(int CompanyId, string AdvertisementCode)
        {
            using (var db = new HMSEntities())
            {
                string nullValues = null;
                var param1 = new SqlParameter("@CaseType", 1);
                var param2 = new SqlParameter("@CompanyId", CompanyId);
                var param3 = new SqlParameter("@AdvertismentCode", AdvertisementCode);
                var param4 = new SqlParameter("@ApplicantCode", nullValues);
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
        public static List<HRInterviews_GetListByParamters_Result> GetInterViewList(int companyId)
        {
            using (var db = new HMSEntities())
            {
                var param1 = new SqlParameter("@CaseType", 1);
                var param2 = new SqlParameter("@CompanyId", companyId);
                var data = db.Database.SqlQuery<HRInterviews_GetListByParamters_Result>(
                    "HRInterviews_GetListByParamters @CaseType, @CompanyId",
                    param1,
                    param2
                ).ToList();
                return data;
            }
        }

        public static List<Employees_GetByParameter_Result> GetEmployeeList(int companyId)
        {
            using (var db = new HMSEntities())
            {
                List<Employees_GetByParameter_Result> data = new List<Employees_GetByParameter_Result>();
                data = db.Employees_GetByParameter(companyId, null).ToList();
                return data;
            }
        }

    }
}