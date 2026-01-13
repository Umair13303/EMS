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
using System;

namespace office360.Areas.ACompany.DB_Helper.MBranchUI_Helper
{
    public class CUD_Operation
    {
        #region HELPER FOR :: INSERT/UPDATE DATA USING STORED PROCEDURE (GENERAL BRANCH & GENERAL BRANCH SETTING) ::-- MAIN DB
        public static int? Update_Insert_GeneralBranch(_SqlParameters PostedData)
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
                        #region OUTPUT VARAIBLE
                        var ResponseParameter = new ObjectParameter("Response", typeof(int));
                        var CampusIdParameter = new ObjectParameter("CampusId", typeof(int));
                        #endregion
                        #region EXECUTE STORE PROCEDURE
                        var GeneralBranch = db.GeneralBranch_UpSert(
                                                             PostedData.OperationType,
                                                             PostedData.GuID,
                                                             PostedData.Description,
                                                             PostedData.CampusTypeId,
                                                             PostedData.OrganizationTypeId,
                                                             PostedData.CountryId,
                                                             PostedData.CityId,
                                                             PostedData.Address.ToSafeString(),
                                                             PostedData.ContactNo.ToSafeString(),
                                                             PostedData.EmailAddress.ToSafeString(),
                                                             PostedData.NTNNo.ToSafeString(),
                                                             PostedData.Remarks.ToSafeString(),
                                                             DateTime.Now,
                                                             Session_Manager.UserId,
                                                             DateTime.Now,
                                                             Session_Manager.UserId,
                                                             (int?)DocumentStatus.DocStatus.Working_BRANCHES,
                                                             (int?)DocumentStatus.DocType.BRANCHES,
                                                             true,
                                                             Session_Manager.BranchId,
                                                             Session_Manager.CompanyId,
                                                             ResponseParameter,
                                                             CampusIdParameter
                            );
                        int? CampusId = (int)CampusIdParameter.Value;

                        var GeneralBranchSetting = db.GeneralBranchSetting_UpSert(
                                                                                    PostedData.OperationType,
                                                                                    Uttility.fn_GetHashGuid(),
                                                                                    CampusId,
                                                                                    PostedData.RollCallSystemId,
                                                                                    PostedData.BillingMethodId,
                                                                                    PostedData.StudyLevelIds,
                                                                                    PostedData.StudyGroupIds,
                                                                                    PostedData.PolicyPeriodId,
                                                                                    PostedData.ChallanMethodId,
                                                                                    DateTime.Now,
                                                                                    Session_Manager.UserId,
                                                                                    DateTime.Now,
                                                                                    Session_Manager.UserId,
                                                                                    (int?)DocumentStatus.DocType.BRANCH_SETTING,
                                                                                    (int?)DocumentStatus.DocStatus.Active_BranchSetting,
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
                    catch (Exception Ex)
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