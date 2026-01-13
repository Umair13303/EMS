using office360.Common.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using static office360.Models.General.DBListCondition;
using static office360.Models.General.Http_Server_Status;

namespace office360.Areas.ACompany.DB_Helper.MClassUI_Helper
{
    public class CUD_Operation
    {

        #region HELPER FOR :: INSERT/UPDATE DATA USING STORED PROCEDURE (APP CLASS) ::-- MAIN DB
        public static int? Update_Insert_AppClass(_SqlParameters PostedData)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        #region DB SETTING
                        if (PostedData.OperationType == nameof(DB_OperationType.INSERT_DATA_INTO_DB))
                        {
                            PostedData.GuID = Uttility.fn_GetHashGuid();
                        }
                        #endregion
                        #region OUTPUT VARIABLES
                        var ResponseParameter = new ObjectParameter("Response", typeof(string));
                        #endregion
                        #region STORE PROCEDURE EXECUTION AppClass_Insert
                        var data = db.AppClass_UpSert(
                                                    PostedData.OperationType,
                                                    PostedData.GuID,
                                                    PostedData.CampusId,
                                                    PostedData.Description,
                                                    PostedData.StudyLevelId,
                                                    PostedData.StudyGroupId,
                                                    DateTime.Now,
                                                    Session_Manager.UserId,
                                                    DateTime.Now,
                                                    Session_Manager.UserId,
                                                    (int?)Models.General.DocumentStatus.DocType.CLASSES,
                                                    (int?)Models.General.DocumentStatus.DocStatus.Working_CLASSES,
                                                    true,
                                                    Session_Manager.BranchId,
                                                    Session_Manager.CompanyId,
                                                    ResponseParameter
                                                    );
                        #endregion
                        #region RESPONSE VALUES IN VARIABLE
                        int? Response = (int)ResponseParameter.Value;
                        #endregion
                        #region TRANSACTION HANDLING DETAIL
                        switch (Response)
                        {
                            case (int?)Http_DB_Response.CODE_SUCCESS:
                            case (int?)Http_DB_Response.CODE_DATA_UPDATED:

                                dbTran.Commit();
                                break;

                            case (int?)Http_DB_Response.CODE_BAD_REQUEST:
                                dbTran.Rollback();
                                break;
                        }
                        #endregion
                        return Http_Server_Status.Http_DB_ResponseByReturnValue(Response);

                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        return Http_Server_Status.Http_DB_Response.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
        #endregion
    }
}