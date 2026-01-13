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
    public class BulkDataOperation
    {

        public static int? Update_Insert_AppSession_BulkOperation(_SqlParameters PostedData, List<_SqlParameters> PostedDataDetail)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        int? Response = (int?)HttpResponses.CODE_DATA_ALREADY_EXIST;
                        var IsAlreadyActiveSession = office360.Common.DataBaseProcedures.ACompany.CheckDuplicateRecord.ISEXIST_APPSESSION_FOR_CLASS(PostedData);
                        if (IsAlreadyActiveSession == 0)
                        {

                            #region DB SETTING
                            if (PostedData.OperationType == nameof(DB_OperationType.INSERT_DATA_INTO_DB))
                            {
                                PostedData.GuID = Uttility.fn_GetHashGuid();
                            }
                            #endregion

                            #region OUTPUT VARIABLES
                            var ResponseParameter = new ObjectParameter("Response", typeof(int));
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
                                                (int?)DocumentStatus.DocType.ADMISSION,
                                                (int?)DocStatus.Open_ADMISSION,
                                                true,
                                                Session_Manager.BranchId,
                                                Session_Manager.CompanyId,
                                                ResponseParameter
                                                );
                            #endregion
                            int? AppSessionId = db.AppSession.Where(x => x.GuID == PostedData.GuID).Select(x => x.Id).FirstOrDefault();
                            #region CONVERTING LIST TO DATA TABLE
                            List<AppSessionDetail_BULK_TT> PostedDataDetail_ = PostedDataDetail.Select(X => new AppSessionDetail_BULK_TT
                            {
                                Id = 0,
                                GuID = Uttility.fn_GetHashGuid(),
                                AppSessionId = AppSessionId,
                                Description = X.Description,
                                PeriodStartOn = X.PeriodStartOn,
                                PeriodEndOn = X.PeriodEndOn,
                                Status = true,
                            }).ToList();
                            DataTable AppSessionDetail_BULK_TT = PostedDataDetail_.ToDataTable();
                            #endregion
                            #region EXECUTION OF STORE PROCEDURE BULKOperation_AppSessionDetail
                            var operationTypeParam = new SqlParameter("@DB_OperationType", SqlDbType.NVarChar, -1)
                            {
                                Value = PostedData.OperationType
                            };

                            var responseParameter = new SqlParameter("@ResponseParameter", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            var appSessionDetailParam = new SqlParameter("@AppSessionDetail", SqlDbType.Structured)
                            {
                                TypeName = "dbo.AppSessionDetail_BULK_TT",
                                Value = AppSessionDetail_BULK_TT
                            };

                            db.Database.ExecuteSqlCommand(
                                "EXEC BULKOperation_AppSessionDetail @DB_OperationType, @ResponseParameter OUTPUT, @AppSessionDetail",
                                operationTypeParam,
                                responseParameter,
                                appSessionDetailParam
                            );
                            var responseValue = (int)responseParameter.Value;
                            #endregion
                            #region RESPONSE VALUES IN VARIABLE
                            Response = (int)ResponseParameter.Value;
                            Response = responseValue;
                            #endregion
                            #region TRANSACTION HANDLING DETAIL
                            switch (Response)
                            {
                                case (int?)HttpResponses.CODE_SUCCESS:
                                    dbTran.Commit();
                                    break;

                                case (int?)HttpResponses.CODE_BAD_REQUEST:
                                    dbTran.Rollback();
                                    break;
                            }
                            #endregion
                        }

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