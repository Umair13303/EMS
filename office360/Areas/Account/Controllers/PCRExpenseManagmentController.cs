using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using office360.Areas.Account.DALHelper;
using office360.Models.DAL;
using office360.Models.DAL.DTO;
using office360.Models.General;
using static office360.Models.General.HttpServerStatus;

namespace office360.Areas.Account.Controllers
{
    public class PCRExpenseManagmentController : ApiController
    {
        #region REGION FOR :: LOOKUP APIS
        [HttpGet]
        public IHttpActionResult GET_ExpenseCategoryList()
        {
            using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
            {
                var ExpenseCategoryList = dal.LK_ExpenseCategory.AsNoTracking().Where(x => x.Status == true)
                     .Select(x => new
                     {
                         x.Id,
                         x.Description,
                     }).ToList();
                return Ok(ExpenseCategoryList);
            }
        }
        #endregion

        #region REGION FOR :: UPSERT DATA INTO DBO.PCRExpense
        [HttpPost]
        public IHttpActionResult UpsetDataIntoDB([FromUri] string OperationType, [FromBody] PCRExpense postedData)
        {
            int? UserId = 1;
            int? BranchId = 1;
            int? CompanyId = 1;
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
                    int? OperationStatus = Record_Validator.Is_Exist_PCRExpense(OperationType, postedData.GuID, postedData.Description, postedData.InvoiceNumber, postedData.NetAmount);
                    #endregion
                    if (OperationType == nameof(DB_OperationType.INSERT_DATA_INTO_DB))
                    {
                        if (OperationStatus == (int?)Http_DB_Response.CODE_AUTHORIZED)
                        {
                            using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
                            {
                                postedData.GuID = Guid.NewGuid();
                                postedData.CreatedOn = DateTime.Now;
                                postedData.CreatedBy = UserId;
                                postedData.DocType = (int?)DOCUMENT_TYPE.PETTY_CASH_EXPENSE;
                                postedData.DocumentStatus = (int?)DOCUMENT_STATUS.ACTIVE_PETTY_CASH_EXPENSE;
                                postedData.Status = true;
                                postedData.BranchId = BranchId;
                                postedData.CompanyId = CompanyId;
                                dal.PCRExpense.Add(postedData);
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
                                var ExistingPCRExpense = dal.PCRExpense.FirstOrDefault(x => x.GuID == postedData.GuID && x.Status == true);
                                if (ExistingPCRExpense == null)
                                {
                                    Response = (int?)Http_DB_Response.CODE_DATA_DOES_NOT_EXIST;
                                }
                                else
                                {
                                    ExistingPCRExpense.TransactionDate = postedData.TransactionDate;
                                    ExistingPCRExpense.InvoiceNumber = postedData.InvoiceNumber;
                                    ExistingPCRExpense.Description = postedData.Description;
                                    ExistingPCRExpense.ExpenseCategoryId = postedData.ExpenseCategoryId;
                                    ExistingPCRExpense.Quantity = postedData.Quantity;
                                    ExistingPCRExpense.UnitPrice = postedData.UnitPrice;
                                    ExistingPCRExpense.NetAmount = postedData.NetAmount;
                                    ExistingPCRExpense.Remarks = postedData.Remarks;
                                    ExistingPCRExpense.UpdatedOn = DateTime.Now;
                                    ExistingPCRExpense.UpdatedBy = UserId;
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

        #region REGION FOR :: DELETE DATA FROM DBO.PCRExpense FOR LIST
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
                    List<PCRExpense> PCRExpenseDetail = dal.PCRExpense.Where(x => x.GuID == GuID).ToList();
                    if (PCRExpenseDetail.Count > 0)
                    {
                        foreach (var PCRExpense in PCRExpenseDetail)
                        {
                            PCRExpense.Status = false;
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

        #region REGION FOR :: GET DATA FROM DBO.PCRExpense FOR LIST
        [HttpGet]
        public IHttpActionResult GET_PCRExpenseList()
        {
            try
            {
                using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
                {
                    dal.Configuration.LazyLoadingEnabled = false;
                    dal.Configuration.ProxyCreationEnabled = false;


                    int CompanyId = 1;
                    DateTime Today = DateTime.Today;
                    DateTime MonthStart = new DateTime(Today.Year, Today.Month, 1);
                    DateTime MonthEnd = MonthStart.AddMonths(1);

                    #region PCR EXPENSE LIST
                    var PCRExpenseList =(from e in dal.PCRExpense.AsNoTracking()
                                         join ec in dal.LK_ExpenseCategory on e.ExpenseCategoryId equals ec.Id
                                         where e.CompanyId == CompanyId && e.Status == true
                                         orderby e.TransactionDate descending
                                         select new
                                         {
                                             e.GuID,
                                             e.TransactionDate,
                                             e.InvoiceNumber,
                                             e.Description,
                                             e.ExpenseCategoryId,
                                             ExpenseCategory = ec.Description,
                                             e.Quantity,
                                             e.UnitPrice,
                                             e.NetAmount,
                                             e.Remarks
                                         }).ToList();

                    #endregion

                    #region PCR EXPENSE KPI SUMMARY

                    decimal SummaryPCRExpense = dal.PCRExpense.AsNoTracking().Where(x => x.CompanyId == CompanyId && x.Status == true).Sum(x => (decimal?)x.NetAmount) ?? 0;
                    decimal SummaryPCRIncome = dal.PCRIncome.AsNoTracking().Where(x => x.CompanyId == CompanyId && x.Status == true).Sum(x => (decimal?)x.Amount) ?? 0;
                    decimal MonthlyPCRExpense = dal.PCRExpense.AsNoTracking().Where(x => x.CompanyId == CompanyId && x.Status == true && x.TransactionDate >= MonthStart && x.TransactionDate < MonthEnd).Sum(x => (decimal?)x.NetAmount) ?? 0;
                    decimal MonthlyPCRIncome = dal.PCRIncome.AsNoTracking().Where(x => x.CompanyId == CompanyId && x.Status == true && x.TransactionDate >= MonthStart && x.TransactionDate < MonthEnd).Sum(x => (decimal?)x.Amount) ?? 0;
                    #endregion
                    var response = new
                    {
                        List = PCRExpenseList,
                        KPI = new
                        {
                            SummaryPCRExpense = SummaryPCRExpense,
                            SummaryPCRIncome = SummaryPCRIncome,
                            MonthlyPCRExpense = MonthlyPCRExpense,
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
    }
}
