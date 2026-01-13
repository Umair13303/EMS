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

namespace office360.Common.DataBaseProcedures.AStudent
{
    public class BulkDataOperation
    {


        public static int? Update_Insert_EnrStudent_BULKOperation(List<_SqlParameters> PostedDataDetail)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {

                        #region DB SETTING
                        if (PostedDataDetail.FirstOrDefault().OperationType == nameof(DB_OperationType.INSERT_DATA_INTO_DB))
                        {

                        }
                        #endregion
                        #region CONVERTING LIST TO DATA TABLE
                        List<EnrStudent_BULK_TT> PostedDataDetail_ = PostedDataDetail.Select(X => new EnrStudent_BULK_TT
                        {
                            GuID = !string.IsNullOrWhiteSpace(X.GuID.ToString()) ? X.GuID : Uttility.fn_GetHashGuid(),
                            CampusId = X.CampusId,
                            SessionId = X.SessionId,
                            RegisteredPeriodId = X.RegisteredPeriodId,
                            ClassId = X.ClassId,
                            AdmissionCatagoryId = X.AdmissionCatagoryId,
                            RegistrationNo = X.RegistrationNo,
                            StudentName = X.StudentName,
                            StudentCNIC = X.StudentCNIC,
                            BirthDate = X.BirthDate,
                            ReligionId = X.ReligionId,
                            CountryId = X.CountryId,
                            DomicileDistrict = X.DomicileDistrict,
                            FatherName = X.FatherName,
                            FatherCNIC = X.FatherCNIC,
                            OccupationId = X.OccupationId,
                            IsFatherAlive = X.IsFatherAlive,
                            GuardianName = X.GuardianName,
                            GuardianCNIC = X.GuardianCNIC,
                            StudentMobile = X.StudentMobile,
                            StudentEmail = X.StudentEmail,
                            ParentMobile = X.ParentMobile,
                            LandLine = X.LandLine,
                            EmergencyMobile = X.EmergencyMobile,
                            Address = X.Address,
                        }).ToList();
                        DataTable EnrStudent_BULK_TT = PostedDataDetail_.ToDataTable();
                        #endregion

                        #region EXECUTE STORE PROCEDURE BULKOperation_EnrStudent
                        var paramOperationType = new SqlParameter("@DB_OperationType", PostedDataDetail.FirstOrDefault().OperationType);
                        var paramSubOperationType = new SqlParameter("@DB_SubOperationType", PostedDataDetail.FirstOrDefault().SubOperationType);
                        var paramResponse = new SqlParameter("@ResponseParameter", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        var paramEnrStudentDetail = new SqlParameter("@EnrStudentDetail", SqlDbType.Structured)
                        {
                            TypeName = "dbo.EnrStudent_BULK_TT",
                            Value = EnrStudent_BULK_TT
                        };

                        var paramCreatedOn = new SqlParameter("@CreatedOn", DateTime.Now);
                        var paramCreatedBy = new SqlParameter("@CreatedBy", Session_Manager.UserId);
                        var paramUpdatedOn = new SqlParameter("@UpdatedOn", DateTime.Now);
                        var paramUpdatedBy = new SqlParameter("@UpdatedBy", Session_Manager.UserId);
                        var paramDocType = new SqlParameter("@DocType", (int?)DocumentStatus.DocType.STUDENT);
                        var paramDocumentStatus = new SqlParameter("@DocumentStatus", (int?)DocumentStatus.DocStatus.Active_STUDENT);
                        var paramStatus = new SqlParameter("@Status", true);
                        var paramBranchId = new SqlParameter("@BranchId", Session_Manager.BranchId);
                        var paramCompanyId = new SqlParameter("@CompanyId", Session_Manager.CompanyId);

                        db.Database.ExecuteSqlCommand(
                                      "EXEC dbo.BULKOperation_EnrStudent @DB_OperationType, @DB_SubOperationType, @ResponseParameter OUTPUT, @EnrStudentDetail, @CreatedOn, @CreatedBy, @UpdatedOn, @UpdatedBy, @DocType, @DocumentStatus, @Status, @BranchId, @CompanyId",
                                      paramOperationType,
                                      paramSubOperationType,
                                      paramResponse,
                                      paramEnrStudentDetail,
                                      paramCreatedOn,
                                      paramCreatedBy,
                                      paramUpdatedOn,
                                      paramUpdatedBy,
                                      paramDocType,
                                      paramDocumentStatus,
                                      paramStatus,
                                      paramBranchId,
                                      paramCompanyId
                        );
                        #endregion


                        #region RESPONSE VALUES IN VARIABLE
                        int? Response = (int)paramResponse.Value;
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