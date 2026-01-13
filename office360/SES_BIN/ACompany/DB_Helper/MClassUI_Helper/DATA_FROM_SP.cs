using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Areas.ACompany.DB_Helper.MClassUI_Helper
{
    public class DATA_FROM_SP
    {
        #region HELPER FOR :: GET DATA USING STORED PROCEDURE ::-- MAIN DB

        public static List<AppClass_GetListByParam_Result> GET_MT_APPCLASS_BYPARAM(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                List<AppClass_GetListByParam_Result> DATA = new List<AppClass_GetListByParam_Result>();
                DATA = db.AppClass_GetListByParam(
                                                       PostedData.DB_IF_PARAM,
                                                       Session_Manager.CompanyId,
                                                       Session_Manager.BranchId,
                                                       PostedData.SessionId,
                                                       PostedData.ClassIds,
                                                       PostedData.CampusId,
                                                       PostedData.SearchParameter,
                                                       Session_Manager.AllowedCampusIds
                                                       ).ToList();
                return DATA;
            }
        }

        #endregion
    }
}