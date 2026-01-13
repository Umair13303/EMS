using office360.Models.EDMX;
using office360.Areas.HiringAndRecruitmentModule.Data;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper
{
    public class FrontDetailsHelper
    {
        HMSEntities db = new HMSEntities();



        public static List<HRAdvertisment_GetDetailByParameter_Result> GetAdvertismentDetailsByParamters(HtmlViewerFilters Filters)
        {
            using (var db = new HMSEntities())
            {
                int? CompanyId = Sessions.CompanyId;
                string AdvertismentCode = Filters.AdvertismentCode;
                List<HRAdvertisment_GetDetailByParameter_Result> data = new List<HRAdvertisment_GetDetailByParameter_Result>();
                data = db.HRAdvertisment_GetDetailByParameter(CompanyId,AdvertismentCode).ToList<HRAdvertisment_GetDetailByParameter_Result>();
                return data;
            }
        }
    }
}