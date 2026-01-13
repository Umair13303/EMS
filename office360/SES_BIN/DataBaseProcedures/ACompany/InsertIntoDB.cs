using office360.Common.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.HttpStatus;
using static office360.Models.General.DBListCondition;
using DocumentStatus = office360.Models.General.DocumentStatus;
using System.Data.Entity.Infrastructure;

namespace office360.Common.DataBaseProcedures.ACompany
{
    public class InsertIntoDB
    {
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
                            case (int?)HttpResponses.CODE_SUCCESS:
                            case (int?)HttpResponses.CODE_DATA_UPDATED:

                                dbTran.Commit();
                                break;
                           
                            case (int?)HttpResponses.CODE_BAD_REQUEST:
                                dbTran.Rollback();
                                break;
                        }
                        #endregion
                        return HttpStatus.HttpResponseByReturnValue(Response);

                    }
                    catch (Exception Ex)
                    {
                        dbTran.Rollback();
                        return HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
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
                                                    (int?)DocumentStatus.DocType.CLASSES,
                                                    (int?)DocumentStatus.DocStatus.Working_CLASSES,
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
                            case (int?)HttpResponses.CODE_SUCCESS:
                            case (int?)HttpResponses.CODE_DATA_UPDATED:

                                dbTran.Commit();
                                break;

                            case (int?)HttpResponses.CODE_BAD_REQUEST:
                                dbTran.Rollback();
                                break;
                        }
                        #endregion
                        return HttpStatus.HttpResponseByReturnValue(Response);
                        
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        return HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
    }
}