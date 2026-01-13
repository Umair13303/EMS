using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Areas.ACompany.DB_Helper.MBranchUI_Helper
{
    public class Document_Detail_By_Id_DBO_FN
    {
        #region GET DETAIL OF DOCUMENT BY ID INT USING DB FUNCTION
        public static List<fn_GetGeneralBranchSettingForCampus_Result> FN_GENERALBRANCHSETTING_INFO_BY_CAMPUSID(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                List<fn_GetGeneralBranchSettingForCampus_Result> DATA = new List<fn_GetGeneralBranchSettingForCampus_Result>();
                DATA = db.fn_GetGeneralBranchSettingForCampus(PostedData.CampusId).ToList();
                return DATA;
            }
        }
        #endregion
    }
}