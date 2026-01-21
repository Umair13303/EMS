using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using office360.Areas.Account.DALHelper;
using office360.Models.DAL;
using office360.Models.DAL.DTO;
using office360.Models.General;
using static office360.Models.General.HttpServerStatus;

namespace office360.Areas.HumanResource.Controllers
{
    public class HREmployeeManagmentController : ApiController
    {
        #region REGION FOR :: LOOKUP APIS

        [HttpGet]
        public IHttpActionResult GET_EmploymentTypeList()
        {
            using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
            {
                var EmploymentTypeList = dal.LK_EmploymentType.AsNoTracking().Where(x => x.Status == true)
                     .Select(x => new
                     {
                         x.Id,
                         x.Description,
                     }).ToList();
                return Ok(EmploymentTypeList);
            }
        }

        [HttpGet]
        public IHttpActionResult GET_DesignationList()
        {
            using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
            {
                var DesignationList = dal.LK_Designation.AsNoTracking().Where(x => x.Status == true)
                     .Select(x => new
                     {
                         x.Id,
                         x.Description,
                     }).ToList();
                return Ok(DesignationList);
            }
        }
        [HttpGet]
        public IHttpActionResult GET_BankList()
        {
            using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
            {
                var BankList = dal.LK_Bank.AsNoTracking().Where(x => x.Status == true)
                     .Select(x => new
                     {
                         x.Id,
                         x.Description,
                     }).ToList();
                return Ok(BankList);
            }
        }
        #endregion

        #region REGION FOR :: UPSERT DATA INTO DBO.HREmployee
        [HttpPost]
        public IHttpActionResult UpsetDataIntoDB([FromUri] string OperationType, [FromBody] HREmployee postedData)
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
                    int? OperationStatus = Record_Validator.Is_Exist_HREmployee(OperationType, postedData.GuID, postedData.CNICNumber);
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
                                postedData.DocType = (int?)DOCUMENT_TYPE.HR_Employee;
                                postedData.DocumentStatus = (int?)DOCUMENT_STATUS.ACTIVE_HR_Employee;
                                postedData.Status = true;
                                postedData.BranchId = BranchId;
                                postedData.CompanyId = CompanyId;
                                dal.HREmployee.Add(postedData);
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
                                var ExistingHREmployee = dal.HREmployee.FirstOrDefault(x => x.GuID == postedData.GuID && x.Status == true);
                                if (ExistingHREmployee == null)
                                {
                                    Response = (int?)Http_DB_Response.CODE_DATA_DOES_NOT_EXIST;
                                }
                                else
                                {
                                    ExistingHREmployee.EmployeeName = postedData.EmployeeName;
                                    ExistingHREmployee.DateofJoining = postedData.DateofJoining;
                                    ExistingHREmployee.EmploymentTypeId = postedData.EmploymentTypeId;
                                    ExistingHREmployee.DesignationId = postedData.DesignationId;
                                    ExistingHREmployee.CNICNumber = postedData.CNICNumber;
                                    ExistingHREmployee.MobileNumber = postedData.MobileNumber;
                                    ExistingHREmployee.EmailAddress = postedData.EmailAddress;
                                    ExistingHREmployee.BankId = postedData.BankId;
                                    ExistingHREmployee.AccountNumber = postedData.AccountNumber;
                                    ExistingHREmployee.DateofResign = postedData.DateofResign;
                                    ExistingHREmployee.Remarks = postedData.Remarks;
                                    ExistingHREmployee.UpdatedOn = DateTime.Now;
                                    ExistingHREmployee.UpdatedBy = UserId;
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
                    List<HREmployee> HREmployeeDetail = dal.HREmployee.Where(x => x.GuID == GuID).ToList();
                    if (HREmployeeDetail.Count > 0)
                    {
                        foreach (var HREmployee in HREmployeeDetail)
                        {
                            HREmployee.Status = false;
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
        public IHttpActionResult GET_HREmployeeList()
        {
            try
            {
                using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
                {
                    dal.Configuration.LazyLoadingEnabled = false;
                    dal.Configuration.ProxyCreationEnabled = false;


                    int CompanyId = 1;

                    #region PCR EXPENSE LIST
                    var HREmployeeList = (from E in dal.HREmployee.AsNoTracking()
                                          join ET in dal.LK_EmploymentType on E.EmploymentTypeId equals ET.Id
                                          join D in dal.LK_Designation on E.DesignationId equals D.Id
                                          join B in dal.LK_Bank on E.BankId equals B.Id


                                          where E.CompanyId == CompanyId && E.Status == true
                                          orderby E.CreatedOn descending
                                          select new
                                          {
                                              E.GuID,
                                              E.EmployeeName,
                                              E.DateofJoining,
                                              E.CNICNumber,
                                              E.MobileNumber,
                                              E.EmailAddress,
                                              E.AccountNumber,
                                              E.DesignationId,
                                              E.EmploymentTypeId,
                                              E.BankId,
                                              Designation = D.Description,
                                              EmploymentType = ET.Description,
                                              Bank = B.Description,

                                          }).ToList();

                    #endregion

                    return Ok(HREmployeeList);
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
