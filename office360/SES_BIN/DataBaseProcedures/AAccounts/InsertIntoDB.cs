using office360.Common.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.HttpStatus;
using static office360.Models.General.DBListCondition;
using System.Globalization;
using System.Data.Entity.Core.Objects;
using DocType = office360.Models.General.DocumentStatus.DocType;

namespace office360.Common.DataBaseProcedures.AAccounts
{
    public class InsertIntoDB
    {


        public static int? StructureFeeType_UPDATE_INSERT(_SqlParameters PostedData)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        #region DB SETTING
                        if(PostedData.OperationType == nameof(DB_OperationType.INSERT_DATA_INTO_DB))
                        {
                            PostedData.GuID = Uttility.fn_GetHashGuid();
                        }
                        #endregion
                        #region OUTPUT VARAIBLE
                        var ResponseParameter       = new ObjectParameter("Response", typeof(int));
                        #endregion
                        #region EXECUTE STORE PROCEDURE
                        var data = db.StructureFeeType_UpSert(
                                                             PostedData.OperationType,
                                                             PostedData.GuID,
                                                             PostedData.FeeCatagoryId,
                                                             PostedData.ChargingMethodId,
                                                             PostedData.Description,
                                                             PostedData.IsOnAdmission,
                                                             PostedData.IsSecurity,
                                                             PostedData.IsRefundable,
                                                             PostedData.IsDiscount,
                                                             PostedData.RevenueAccountId,
                                                             PostedData.AssetAccountId,
                                                             PostedData.LiabilityAccountId,
                                                             PostedData.CostOfSaleAccountId,
                                                             DateTime.Now,
                                                             Session_Manager.UserId,
                                                             DateTime.Now,
                                                             Session_Manager.UserId,
                                                             (int?)DocType.FEE_TYPE,
                                                             (int?)DocStatus.Active_FEE_TYPE,
                                                             true,
                                                             Session_Manager.BranchId,
                                                             Session_Manager.CompanyId,
                                                             ResponseParameter
                                                              );
                        #endregion
                        #region RESPONSE VALUES IN VARIABLE
                        int? Response               = (int)ResponseParameter.Value;
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
                            default:
                                dbTran.Rollback();
                                break;
                        }
                       
                        return HttpStatus.HttpResponseByReturnValue(Response);
                        #endregion

                    }
                    catch (Exception Ex)
                    {
                        dbTran.Rollback();
                        return HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
        public static int? AccFeeStructure_UPDATE_INSERT(_SqlParameters PostedData, List<_SqlParameters> PostedDataDetail)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        int? Response = HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();

                        var FeeStructureParentGuID = Uttility.fn_GetHashGuid();

                        #region DB SETTING
                        if (PostedData.OperationType == nameof(DB_OperationType.INSERT_DATA_INTO_DB))
                        {
                            PostedData.GuID = Uttility.fn_GetHashGuid();
                        }
                        #endregion


                        #region DESCRIPTIONAL VARIABLE
                        var Code = (DateTime.Now.ToString("ddMMyy") + "-000" + (db.AccFeeStructure.ToList().Count() + 1)).ToSafeString();
                        var SessionName = db.AppSession.Where(x => x.Id == PostedData.SessionId).Select(x => new _SqlParameters { Code = x.Code }).FirstOrDefault();
                        var ClassName = db.AppClass.Where(x => x.Id == PostedData.ClassId).Select(x => new _SqlParameters { Code = x.Code }).FirstOrDefault();
                        #endregion
                        #region COUNT CHECK FEE STRUCTURE CLASS WISE
                        var FeeStructureCount= AAccounts.CheckDuplicateRecord.ISEXIST_FEESTRUCTURE_FOR_CLASS(PostedData);
                        #endregion

                        switch (PostedData.OperationType)
                        {
                            #region INSERT DATA INTO DB:: ACCFEESTRUCTURE AND ACCFEESTRUCTUREDETAIL
                            case nameof(DB_OperationType.INSERT_DATA_INTO_DB):
                               

                                // DUPLICATE RECORD DOES NOT EXIST :: PROCEED INSERT DATA
                                if (FeeStructureCount.Count == 0)
                                {

                                  

                                    #region EXECUTE STORE PROCEDURE PARENT ::ACCFEESTRUCTURE
                                    var NewFeeStructureParent = new AccFeeStructure
                                    {
                                        GuID = PostedData.GuID,
                                        Code = Code,
                                        CampusId = PostedData.CampusId,
                                        Description = (("FS-" + ClassName.Code +"-"+SessionName.Code)+ (db.AccFeeStructure.ToList().Count() + 1)).ToSafeString(),
                                        SessionId = PostedData.SessionId,
                                        ClassId = PostedData.ClassId,
                                        WHTaxPolicyId = PostedData.WHTaxPolicyId,
                                        TotalFeeExclusive = PostedData.TotalFeeExclusive,
                                        WHTAmount = PostedData.WHTAmount,
                                        TotalFee = PostedData.TotalFee,
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = Session_Manager.UserId,
                                        DocType = (int?)DocType.FEE_STRUCTURE,
                                        DocumentStatus = (int?)DocStatus.Active_FEE_STRUCTURE,
                                        Status = true,
                                        BranchId = Session_Manager.BranchId,
                                        CompanyId = Session_Manager.CompanyId,
                                    };
                                    db.AccFeeStructure.Add(NewFeeStructureParent);
                                    db.SaveChanges();

                                    var InsertedFeeStructureId = db.AccFeeStructure.Where(x => x.GuID == PostedData.GuID).Select(x => new _SqlParameters { Id = x.Id }).ToList();

                                    #endregion


                                    #region EXECUTE STORE PROCEDURE DETAIL ::ACCFEESTRUCTUREDETAIL
                                    if (InsertedFeeStructureId.Count > 0)

                                    {
                                        PostedDataDetail.ToDataTable();
                                       
                                        var NewFeeStructureDetail = PostedDataDetail.Select(FeeStructureDetailList => new AccFeeStructureDetail
                                        {
                                            GuID = Uttility.fn_GetHashGuid(),
                                            FeeStructureId = InsertedFeeStructureId.FirstOrDefault().Id,
                                            FeeTypeId = FeeStructureDetailList.FeeTypeId,
                                            RevenueAccountId = FeeStructureDetailList.RevenueAccountId,
                                            AssetAccountId = FeeStructureDetailList.AssetAccountId,
                                            LiabilityAccountId = FeeStructureDetailList.LiabilityAccountId,
                                            CostOfSaleAccountId = FeeStructureDetailList.CostOfSaleAccountId,
                                            Amount = FeeStructureDetailList.FeeAmount,
                                            Status = true,
                                        }).ToList();
                                        db.AccFeeStructureDetail.AddRange(NewFeeStructureDetail);
                                        Response = (int?)HttpResponses.CODE_SUCCESS;
                                    }
                                    else
                                    {
                                        Response = HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                                    }
                                    #endregion


                                }
                                // DUPLICATE RECORD EXIST :: PROCEED DO NOT INSERT DATA
                                else
                                {
                                    Response = (int?)HttpResponses.CODE_DATA_ALREADY_EXIST;
                                }

                                break;

                            #endregion

                            #region UPDATE DATA INTO DB:: ACCFEESTRUCTURE AND ACCFEESTRUCTUREDETAIL
                            case nameof(DB_OperationType.UPDATE_DATA_INTO_DB):
                                try
                                {
                                    #region UPDATE THE FEE STRUCTURE BY GUID STATUS :: INACTIVE
                                    var ExistingFeeStructure = db.AccFeeStructure.Where(FS => FS.SessionId == PostedData.SessionId && FS.ClassId == PostedData.ClassId).ToList();
                                        ExistingFeeStructure.ForEach(FS => FS.DocumentStatus = (int?)DocStatus.InActive_FEE_STRUCTURE);
                                    db.SaveChanges();
                                    #endregion
                                    #region EXECUTE STORE PROCEDURE PARENT ::ACCFEESTRUCTURE
                                    var NewFeeStructureParent = new AccFeeStructure
                                    {
                                        GuID = FeeStructureParentGuID,
                                        Code = Code,
                                        CampusId = PostedData.CampusId,
                                        Description = (("FS-" + ClassName.Code + "-" + SessionName.Code) + (db.AccFeeStructure.ToList().Count() + 1)).ToSafeString(),
                                        SessionId = PostedData.SessionId,
                                        ClassId = PostedData.ClassId,
                                        WHTaxPolicyId = PostedData.WHTaxPolicyId,
                                        TotalFeeExclusive = PostedData.TotalFeeExclusive,
                                        WHTAmount = PostedData.WHTAmount,
                                        TotalFee = PostedData.TotalFee,
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = Session_Manager.UserId,
                                        DocType = (int?)DocType.FEE_STRUCTURE,
                                        DocumentStatus = (int?)DocStatus.Active_FEE_STRUCTURE,
                                        Status = true,
                                        BranchId = Session_Manager.BranchId,
                                        CompanyId = Session_Manager.CompanyId,
                                    };
                                    db.AccFeeStructure.Add(NewFeeStructureParent);
                                    db.SaveChanges();
                                    #endregion

                                    var InsertedFeeStructureId = db.AccFeeStructure.Where(x => x.GuID == FeeStructureParentGuID).Select(x => new _SqlParameters { Id = x.Id }).ToList();

                                    #region EXECUTE STORE PROCEDURE DETAIL ::ACCFEESTRUCTUREDETAIL
                                    if (InsertedFeeStructureId.Count > 0)
                                    {
                                        var NewFeeStructureDetail = PostedDataDetail.Select(FeeStructureDetailList => new AccFeeStructureDetail
                                        {
                                            GuID = Uttility.fn_GetHashGuid(),
                                            FeeStructureId = InsertedFeeStructureId.FirstOrDefault().Id,
                                            FeeTypeId = FeeStructureDetailList.FeeTypeId,
                                            RevenueAccountId = FeeStructureDetailList.RevenueAccountId,
                                            AssetAccountId = FeeStructureDetailList.AssetAccountId,
                                            LiabilityAccountId = FeeStructureDetailList.LiabilityAccountId,
                                            CostOfSaleAccountId = FeeStructureDetailList.CostOfSaleAccountId,
                                            Amount = FeeStructureDetailList.FeeAmount,
                                            Status = true,
                                        }).ToList();
                                        db.AccFeeStructureDetail.AddRange(NewFeeStructureDetail);
                                        Response = (int?)HttpResponses.CODE_SUCCESS;
                                    }
                                    else
                                    {
                                        Response = HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                                    }
                                    #endregion

                                    Response = (int?)HttpResponses.CODE_DATA_UPDATED;
                                }
                                catch
                                {
                                    Response = (int?)HttpResponses.CODE_UN_KNOWN_ACTIVITY;
                                }
                                break;
                            #endregion

                            #region DELETE DATA INTO DB:: ACCFEESTRUCTURE AND ACCFEESTRUCTUREDETAIL
                            case nameof(DB_OperationType.DELETE_DATA_INTO_DB):
                                try
                                {
                                    Response = (int?)HttpResponses.CODE_DATA_UPDATED;
                                }
                                catch
                                {
                                    Response = (int?)HttpResponses.CODE_UN_KNOWN_ACTIVITY;
                                }
                                break;
                            #endregion

                        }


                        #region TRANSACTION HANDLING DETAIL
                        switch (Response)
                        {
                            case (int?)HttpResponses.CODE_SUCCESS:
                            case (int?)HttpResponses.CODE_DATA_UPDATED:
                                db.SaveChanges();
                                dbTran.Commit();
                                break;
                            
                            case (int?)HttpResponses.CODE_BAD_REQUEST:
                                dbTran.Rollback();
                                break;
                            default:
                                dbTran.Rollback();
                                break;
                        }
                       
                        return HttpStatus.HttpResponseByReturnValue(Response);
                        #endregion

                    }
                    catch (Exception Ex)
                    {
                        dbTran.Rollback();
                        return HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
        public static int? StructureDiscountType_UPDATE_INSERT(_SqlParameters PostedData)
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
                        #endregion
                        #region EXECUTE STORE PROCEDURE
                        var data = db.StructureDiscountType_Upsert(
                                                             PostedData.OperationType,
                                                             PostedData.GuID,
                                                             PostedData.Code,
                                                             PostedData.Description,
                                                             PostedData.Remarks,
                                                             DateTime.Now,
                                                             Session_Manager.UserId,
                                                             DateTime.Now,
                                                             Session_Manager.UserId,
                                                             (int?)DocType.DISCOUNT_TYPE,
                                                             (int?)DocStatus.Active_DISCOUNT_TYPE,
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
                            default:
                                dbTran.Rollback();
                                break;
                        }

                        return HttpStatus.HttpResponseByReturnValue(Response);
                        #endregion

                    }
                    catch (Exception Ex)
                    {
                        dbTran.Rollback();
                        return HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
    }
}