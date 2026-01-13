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
using static office360.Models.General.DBListCondition;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.Http_Server_Status;
namespace office360.Areas.ACompany.DB_Helper.MSessionUI_Helper
{
    public class CUD_BULK_Operation
    {
        #region HELPER FOR :: INSERT/UPDATE DATA USING STORED PROCEDURE (APP SESSION & APP SESSION DETAIL) ::-- MAIN DB

        public static int? Update_Insert_AppSession_BulkOperation(_SqlParameters PostedData, List<_SqlParameters> PostedDataDetail)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        #region CHECK :: DUPLICATE APPSESSION BY CLASSIDS
                        int? DB_OPERATION_STATUS =DB_Helper.MSessionUI_Helper.Duplicate_Record_Check_BY_LINQ.IS_EXIST_APPSESSION_BYCLASSIDS(PostedData);
                        switch (DB_OPERATION_STATUS)
                        {
                            case (int?)Http_DB_Response.CODE_AUTHORIZED:
                                #region DB SETTING
                                if (PostedData.OperationType == nameof(DB_OperationType.INSERT_DATA_INTO_DB))
                                {
                                    PostedData.GuID = Uttility.fn_GetHashGuid();
                                }
                                #endregion
                                #region OUTPUT VARIABLES
                                var ResponseParameter = new ObjectParameter("Response", typeof(int));
                                var Response_SQL_PARAM = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
                                #endregion
                                #region EXECUTION OF STORE PROCEDURE AppSession_UpSert

                                var AppSession = db.AppSession_UpSert(
                                                    PostedData.OperationType,
                                                    PostedData.GuID,
                                                    PostedData.Description,
                                                    PostedData.CampusId,
                                                    PostedData.SessionStartDate,
                                                    PostedData.SessionEndDate,
                                                    PostedData.EnrollmentTypeId,
                                                    PostedData.ClassIds,
                                                    DateTime.Now,
                                                    Session_Manager.UserId,
                                                    DateTime.Now,
                                                    Session_Manager.UserId,
                                                    (int?)Models.General.DocumentStatus.DocType.ADMISSION,
                                                    (int?)DocStatus.Open_ADMISSION,
                                                    true,
                                                    Session_Manager.BranchId,
                                                    Session_Manager.CompanyId,
                                                    ResponseParameter
                                                    );
                                #endregion
                                int? SessionId = db.AppSession.Where(x => x.GuID == PostedData.GuID).Select(x => x.Id).FirstOrDefault();
                                #region CONVERTING LIST TO DATA TABLE
                                List<ACompany.Data.BULK_AM.AppSessionDetail_BULK_TT> PostedDataDetail_ = PostedDataDetail.Select(X => new ACompany.Data.BULK_AM.AppSessionDetail_BULK_TT
                                {
                                    Id = 0,
                                    GuID = PostedData.OperationType == nameof(DB_OperationType.UPDATE_DATA_INTO_DB) ? X.GuID : Uttility.fn_GetHashGuid(),
                                    SessionId = SessionId,
                                    Description = X.Description,
                                    PeriodStartOn = X.PeriodStartOn,
                                    PeriodEndOn = X.PeriodEndOn,
                                    Status = true,
                                }).ToList();
                                DataTable AppSessionDetail_BULK_TT = PostedDataDetail_.ToDataTable();
                                #endregion
                                #region EXECUTION OF STORE PROCEDURE BULKOperation_AppSessionDetail
                                var OperationType_SQL_PARAM = new SqlParameter("@DB_OperationType", SqlDbType.NVarChar, -1) { Value = PostedData.OperationType };
                                var AppSessionDetail_SQL_PARAM = new SqlParameter("@AppSessionDetail", SqlDbType.Structured)
                                {
                                    TypeName = "dbo.AppSessionDetail_BULK_TT",
                                    Value = AppSessionDetail_BULK_TT
                                };

                                db.Database.ExecuteSqlCommand(
                                    "EXEC BULKOperation_AppSessionDetail @DB_OperationType, @Response OUTPUT, @AppSessionDetail",
                                    OperationType_SQL_PARAM,
                                    Response_SQL_PARAM,
                                    AppSessionDetail_SQL_PARAM
                                );
                                #endregion
                                #region RESPONSE VALUES IN VARIABLE
                                int? Response;
                                Response = (int?)ResponseParameter.Value;
                                Response = (int?)Response_SQL_PARAM.Value;
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

                            default:
                                return Http_Server_Status.Http_DB_ResponseByReturnValue(DB_OPERATION_STATUS);
                        }

                        #endregion


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