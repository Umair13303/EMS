using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Presentation;
using office360.Models.DAL;
using office360.Models.DAL.DTO;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using static office360.Models.General.HttpServerStatus;

namespace office360.Areas.Account.Controllers
{
    public class PCRIncomeManagmentController : ApiController
    {

        #region REGION FOR :: UPSERT DATA INTO DBO.PCRIncome
        [HttpPost]
        public IHttpActionResult UpsetDataIntoDB([FromUri] string OperationType, [FromBody] PCRIncome postedData)
        {
            try
            {
                int? Response = (int?)Http_DB_Response.CODE_BAD_REQUEST;
                if (postedData == null)
                {
                    return BadRequest("Invalid data.");
                }
                else
                {
                    #region CHECK IF DUPLICATE RECORD EXIST
                    int? OperationStatus = Is_Exist_PCRIncome(OperationType, postedData.GuID, postedData.Description);
                    #endregion
                    if (OperationType == nameof(DB_OperationType.INSERT_DATA_INTO_DB))
                    {
                        if (OperationStatus == (int?)Http_DB_Response.CODE_AUTHORIZED)
                        {
                            using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
                            {
                                postedData.GuID = Guid.NewGuid();
                                postedData.CreatedOn = DateTime.Now;
                                postedData.CreatedBy = 1;
                                postedData.DocType = (int?)DOCUMENT_TYPE.PETTY_CASH_INCOME;
                                postedData.DocumentStatus = (int?)DOCUMENT_STATUS.ACTIVE_PETTY_CASH_INCOME;
                                postedData.Status = true;
                                postedData.BranchId = 1;
                                postedData.CompanyId = 1;
                                dal.PCRIncome.Add(postedData);
                                dal.SaveChanges();
                                Response = (int?)Http_DB_Response.CODE_SUCCESS;
                            }
                        }
                        else
                        {
                            Response = OperationStatus;
                        }
                    }
                    if (OperationType == nameof(DB_OperationType.UPDATE_DATA_INTO_DB))
                    {
                        if (OperationStatus == (int?)Http_DB_Response.CODE_AUTHORIZED)
                        {
                            using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
                            {
                                var ExistingPCRIncome = dal.PCRIncome.FirstOrDefault(x => x.GuID == postedData.GuID && x.Status == true);
                                if (ExistingPCRIncome == null)
                                {
                                    Response = (int?)Http_DB_Response.CODE_DATA_DOES_NOT_EXIST;
                                }
                                else
                                {
                                    ExistingPCRIncome.TransactionDate = postedData.TransactionDate;
                                    ExistingPCRIncome.TransactionID = postedData.TransactionID;
                                    ExistingPCRIncome.PaymentThroughId = postedData.PaymentThroughId;
                                    ExistingPCRIncome.Amount = postedData.Amount;
                                    ExistingPCRIncome.Description = postedData.Description;
                                    ExistingPCRIncome.Remarks = postedData.Remarks;
                                    ExistingPCRIncome.UpdatedOn = DateTime.Now;
                                    ExistingPCRIncome.UpdatedBy = 1;
                                    dal.SaveChanges();
                                    Response = (int?)Http_DB_Response.CODE_DATA_UPDATED;

                                }
                            }
                        }
                        else
                        {
                            Response = OperationStatus;
                        }

                    }

                    string Message = HttpServerStatus.HTTP_DB_TransactionMessagByStatusCode(Response);

                    return Ok(new { message = Message, status = Response });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region REGION FOR :: DELETE DATA FROM DBO.PCRIncome FOR LIST
        [HttpPost]
        public IHttpActionResult Delete_RecordByGuID(Guid? GuID)
        {
            int? Response = (int?)Http_DB_Response.CODE_BAD_REQUEST;
            try
            {
                if (GuID == null)
                {
                    return BadRequest("Invalid data.");
                }
                using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
                {
                    List<PCRIncome> PCRIncomeDetail = dal.PCRIncome.Where(x => x.GuID == GuID).ToList();
                    if (PCRIncomeDetail.Count > 0)
                    {
                        foreach (var PCRIncome in PCRIncomeDetail)
                        {
                            PCRIncome.Status = false;
                        }
                        Response = (int?)Http_DB_Response.CODE_DATA_DELETED;
                        dal.SaveChanges();
                    }
                    else
                    {
                        Response = (int?)Http_DB_Response.CODE_DATA_DOES_NOT_EXIST;
                    }

                }
                string Message = HttpServerStatus.HTTP_DB_TransactionMessagByStatusCode(Response);

                return Ok(new { message = Message, status = Response });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion
        #region REGION FOR :: UTTITLITY APIS
        [HttpGet]
        public IHttpActionResult GET_PaymentMethodList()
        {
            using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
            {
                var PaymentMethodList = dal.PaymentMethod.AsNoTracking().Where(x => x.Status == true)
                     .Select(x => new
                     {
                         x.Id,
                         x.Description,
                     }).ToList();
                return Ok(PaymentMethodList);
            }
        }
        #endregion
        #region REGION FOR :: GET DATA FROM DBO.PCRIncome FOR LIST
        [HttpGet]
        public IHttpActionResult GET_PCRIncomeList()
        {
            try
            {
                int CompanyId = 1;
                DateTime Today = DateTime.Today;
                DateTime MonthStart = new DateTime(Today.Year, Today.Month, 1);
                DateTime MonthEnd = MonthStart.AddMonths(1);

                using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
                {
                    dal.Configuration.LazyLoadingEnabled = false;
                    dal.Configuration.ProxyCreationEnabled = false;
                    #region PCR INCOME LIST
                    var PCRIncomeList = dal.PCRIncome.AsNoTracking().Where(x => x.CompanyId == CompanyId && x.Status == true).OrderByDescending(x => x.TransactionDate)
                        .Select(x => new
                        {
                            x.GuID,
                            x.TransactionDate,
                            x.TransactionID,
                            x.PaymentThroughId,
                            x.Amount,
                            x.Description,
                            x.Remarks
                        }).ToList();
                    #endregion

                    #region PCR EXPENSE KPI SUMMARY
                    decimal SummaryPCRIncome = dal.PCRIncome.AsNoTracking().Where(x => x.CompanyId == CompanyId && x.Status == true).Sum(x => (decimal?)x.Amount) ?? 0;
                    decimal MonthlyPCRIncome = dal.PCRIncome.AsNoTracking().Where(x => x.CompanyId == CompanyId && x.Status == true && x.TransactionDate >= MonthStart && x.TransactionDate < MonthEnd).Sum(x => (decimal?)x.Amount) ?? 0;
                    #endregion
                    var response = new
                    {
                        List = PCRIncomeList,
                        KPI = new
                        {
                            SummaryPCRIncome = SummaryPCRIncome,
                            MonthlyPCRIncome = MonthlyPCRIncome
                        }
                    };
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region DUPLICATE CHECK METHOD
        public static int? Is_Exist_PCRIncome(string OperationType,Guid? GuID, string Description)
        {
            bool IsRecordExist = false;
            int? Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
            using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
            {
                switch (OperationType)
                {
                    case nameof(DB_OperationType.INSERT_DATA_INTO_DB):
                        #region IN CASE OF INSERT :: CHECK IF ENTERY RECORD EXIST , BASED ON DATA ENTERED
                        IsRecordExist = dal.PCRIncome
                            .Any(x =>
                                 x.Description == Description
                                && x.Status == true
                            );
                        #endregion
                        if (!IsRecordExist)
                            Response = (int?)Http_DB_Response.CODE_AUTHORIZED;
                        else
                            Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
                        break;

                    case nameof(DB_OperationType.UPDATE_DATA_INTO_DB):
                        IsRecordExist = dal.PCRIncome
                            .Any(x =>
                                 x.GuID == GuID
                                && x.Status == true
                            );
                        if (!IsRecordExist)
                            Response = (int?)Http_DB_Response.CODE_DATA_DOES_NOT_EXIST;
                        else
                            Response = (int?)Http_DB_Response.CODE_AUTHORIZED;
                        break;
                    default:
                        Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
                        break;
                }


            }

            return Response;

        }
        #endregion
    }
}
