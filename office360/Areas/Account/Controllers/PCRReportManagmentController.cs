
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
using office360.Models.DAL;
using office360.Models.DAL.DTO;

namespace office360.Areas.Account.Controllers
{
    public class PCRReportManagmentController : ApiController
    {

        #region REGION FOR :: GET DATA FROM DBO.PCRLedger_ByDateRange FOR LIST
        [HttpGet]
        public IHttpActionResult GET_PRCLedger_ByDateRange(string StartDate, string EndDate, int PageNumber = 0, string Search = "", int PageSize = 50)
        {
            try
            {
                int CompanyId = 1;
                using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
                {
                    dal.Configuration.LazyLoadingEnabled = false;
                    dal.Configuration.ProxyCreationEnabled = false;
                    #region PCR LEDGER LIST
                    var ParamStartDate = new System.Data.SqlClient.SqlParameter("@StartDate", DateTime.Parse(StartDate));
                    var ParamEndDate = new System.Data.SqlClient.SqlParameter("@EndDate", DateTime.Parse(EndDate));
                    var ParamCompanyId = new System.Data.SqlClient.SqlParameter("@CompanyId", CompanyId);

                    var PCRLedgerList = dal.Database.SqlQuery<PCRLedger_ByDateRange_VM>("EXEC PCRLedger_ByDateRange @StartDate, @EndDate, @CompanyId", ParamStartDate, ParamEndDate, ParamCompanyId).ToList();
                    #endregion

                    #region PCR LEDGER LIST FILTER BY SEARCH IF APPLIED

                    if (!string.IsNullOrEmpty(Search))
                    {
                        Search = Search.ToLower();
                        PCRLedgerList = PCRLedgerList.Where(x =>
                            (x.Description != null && x.Description.ToLower().Contains(Search)) ||
                            (x.Code != null && x.Code.ToLower().Contains(Search))
                        ).ToList();
                    }
                    #endregion

                    #region PCR LEDGER KPI SUMMARY
                    decimal Credit = PCRLedgerList.Sum(x => x.Credit);
                    decimal Debit = PCRLedgerList.Sum(x => x.Debit);
                    decimal Opening = PCRLedgerList.FirstOrDefault()?.Balance + (PCRLedgerList.FirstOrDefault()?.Debit ?? 0) - (PCRLedgerList.FirstOrDefault()?.Credit ?? 0) ?? 0;
                    decimal Closing = PCRLedgerList.LastOrDefault()?.Balance ?? 0;
                    var KPISummary = new
                    {
                        Opening = Opening,
                        Credit = Credit,
                        Debit = Debit,
                        Closing = Closing
                    };
                    #endregion

                    #region PCR LEDGER LIST PAGINATION
                    int TotalCount = PCRLedgerList.Count;
                    var pagedList = PCRLedgerList.Skip(PageNumber * PageSize).Take(PageSize).ToList();
                    #endregion

                    return Ok(new
                    {
                        Data = pagedList,
                        TotalCount = TotalCount,
                        KPISummary = KPISummary
                    });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion  

        #region REGION FOR :: GET DATA FROM DBO.PCRExpense MONTHLY SUMMARY

        [HttpGet]
        public IHttpActionResult GET_PRCSummary_ByDateRange(string StartMonth, string EndMonth)
        {
            int CompanyId = 1;
            DateTime StartDate = DateTime.Parse(StartMonth);
            DateTime EndDate = DateTime.Parse(EndMonth);
            try
            {
                using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
                {
                    dal.Configuration.LazyLoadingEnabled = false;
                    dal.Configuration.ProxyCreationEnabled = false;
                    var PCRExpenseList = (from e in dal.PCRExpense
                                          join ec in dal.ExpenseCategory
                                              on e.ExpenseCategoryId equals ec.Id
                                          where e.TransactionDate >= StartDate && e.TransactionDate < EndDate
                                          select new
                                          {
                                              e.Id,
                                              e.TransactionDate,
                                              e.NetAmount,
                                              e.Description,
                                              ExpenseCategoryId = e.ExpenseCategoryId,
                                              ExpenseCategory = ec.Description
                                          }).ToList();

                    List<DateTime> MonthList = new List<DateTime>();
                    DateTime CurrentMonth = new DateTime(StartDate.Year, StartDate.Month, 1);
                    while (CurrentMonth < EndDate)
                    {
                        MonthList.Add(CurrentMonth);
                        CurrentMonth = CurrentMonth.AddMonths(1);
                    }

                    var ExpenseSummary = PCRExpenseList.GroupBy(e => new { e.ExpenseCategoryId, e.ExpenseCategory })
                        .Select(Group =>
                        {
                            var row = new Dictionary<string, object>();
                            row["ExpenseCategoryId"] = Group.Key;
                            row["ExpenseCategory"] = Group.Key.ExpenseCategory;

                            decimal? SummaryTotal = 0;
                            foreach (var month in MonthList)
                            {
                                decimal? MonthlySum = Group
                                .Where(x => x.TransactionDate >= month && x.TransactionDate < month.AddMonths(1))
                                .Sum(x => x.NetAmount);

                                row[month.ToString("MMM-yyyy")] = MonthlySum;
                                SummaryTotal += MonthlySum;
                            }

                            row["SummaryTotal"] = SummaryTotal;
                            return row;
                        }).ToList();
                    return Ok(ExpenseSummary);
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
