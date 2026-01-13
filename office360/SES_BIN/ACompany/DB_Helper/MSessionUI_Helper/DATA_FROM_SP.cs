using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Areas.ACompany.DB_Helper.MSessionUI_Helper
{
    public class DATA_FROM_SP
    {
        #region HELPER FOR :: GET DATA USING STORED PROCEDURE ::-- MAIN DB

        public static List<AppSession_GetListByParam_Result> GET_MT_APPSESSION_BYPARAM(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                List<AppSession_GetListByParam_Result> DATA = new List<AppSession_GetListByParam_Result>();
                DATA = db.AppSession_GetListByParam(
                                                    PostedData.DB_IF_PARAM,
                                                    Session_Manager.CompanyId,
                                                    Session_Manager.BranchId,
                                                    PostedData.CampusId,
                                                    Session_Manager.AllowedCampusIds,
                                                    PostedData.SearchParameter
                                                    ).ToList();
                return DATA;
            }
        }
        #endregion
    }
}