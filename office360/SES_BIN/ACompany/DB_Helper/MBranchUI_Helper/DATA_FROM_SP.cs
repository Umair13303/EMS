using System;
using System.Linq;
using System.Web;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.Http_Server_Status;
using static office360.Models.General.DBListCondition;
using DocumentStatus = office360.Models.General.DocumentStatus;
using System.Data.Entity.Infrastructure;
using office360.Models.EDMX;
using office360.Models.General;
using office360.Common.CommonHelper;
using System.Data.Entity.Core.Objects;
using office360.Extensions;
using System.Collections.Generic;

namespace office360.Areas.ACompany.DB_Helper.MBranchUI_Helper
{
    public class DATA_FROM_SP
    {
        #region HELPER FOR :: GET DATA USING STORED PROCEDURE ::-- MAIN DB

        public static List<GeneralBranch_GetListByParam_Result> GET_MT_GENERALBRANCH_BYPARAM(_SqlParameters PostedData)
        {

            using (SESEntities db = new SESEntities())
            {
                List<GeneralBranch_GetListByParam_Result> DATA = new List<GeneralBranch_GetListByParam_Result>();

                DATA = db.GeneralBranch_GetListByParam(
                                                       PostedData.DB_IF_PARAM,
                                                       Session_Manager.CompanyId,
                                                       PostedData.CampusId ?? Session_Manager.BranchId,
                                                       Session_Manager.AllowedCampusIds,
                                                       (int?)Models.General.DocumentStatus.DocStatus.Working_BRANCHES,
                                                       (int?)Models.General.DocumentStatus.DocStatus.Open_ADMISSION,
                                                       PostedData.SearchParameter
                                                       ).ToList();

                return DATA;
            }
        }

        public static List<GeneralBranch_GetListBySearch_Result> GET_MT_GENERALBRANCH_LIST_BY_SEARCHQUERY(_SqlParameters PostedData)
        {
            List<GeneralBranch_GetListBySearch_Result> List = new List<GeneralBranch_GetListBySearch_Result>();
            using (var db = new SESEntities())
            {
                List = db.GeneralBranch_GetListBySearch(
                                                        Session_Manager.CompanyId,
                                                        Session_Manager.BranchId,
                                                        PostedData.SearchById,
                                                        PostedData.InputText
                                                        ).ToList<GeneralBranch_GetListBySearch_Result>();
            }
            return List;

        }
        #endregion
    }
}