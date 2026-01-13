using office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace office360.Areas.HiringAndRecruitmentModule.Controllers
{
    public class SaveDataIntoDbController : Controller
    {
        HMSEntities db = new HMSEntities();

       
        public ActionResult InsertHRApplicants(HRApplicants HRApplicants, List<HREducationDetails> HREducationDetails,List<HREmploymentHistory> HREmploymentHistory, HttpPostedFileBase ApplicantCVFile)
        {
            #region DOCUMENT CODE
            var fileName = "";
            var filePath = "";
            string DocumentCode = "APL" + '-' + DateTime.Now.ToShortDateString().Replace("/", "").Replace("0", "");
            #endregion DOCUMENT CODE
            string result = InsertIntoDB.InsertIntoHRApplicants(HRApplicants,HREducationDetails,HREmploymentHistory,DocumentCode, out int? StatusCode);
            #region STATUS CODE RESPONSE
            if (StatusCode == 200)
            {
                ViewBag.Message = "Applicants added successfully!";
            }
            if (StatusCode == 500)
            {
                ViewBag.Message = "Failed to add Applicants.";
            }
            #endregion


            return Json(new { Message = result, JsonRequestBehavior.AllowGet });

        }
        public ActionResult InsertHRInterviews(HRInterviews HRInterviews)
        {
            #region DOCUMENT CODE
            string DocumentCode = "INTE" + '-' + DateTime.Now.ToShortDateString().Replace("/", "").Replace("0", "");
            #endregion
            string result = InsertIntoDB.InsertIntoHRInterviews(HRInterviews,DocumentCode, out int? StatusCode);
            #region STATUS CODE RESPONSE
            if (StatusCode == 200)
            {
                ViewBag.Message = "Schedule InterView Successfully!";
            }
            if (StatusCode == 500)
            {
                ViewBag.Message = "Failed to Schedule InterView.";
            }
            #endregion

            return Json(new { Message = result, JsonRequestBehavior.AllowGet });
               
        }
        public ActionResult UpdateHRMApplicantInterviews(List<HRInterviews> HRInterviews)
        {
            string result = InsertIntoDB.ApproveOrRejectHRMApplicantInterviews(HRInterviews, out int? StatusCode);
            #region STATUS CODE RESPONSE

            if (StatusCode == 200)
            {
                ViewBag.Message = "Applicants Status Has Been successfully Updated!";
            }
            if (StatusCode == 500)
            {
                ViewBag.Message = "Failed to Update Applicants Status";
            }
            #endregion
            return Json(new { Message = result, JsonRequestBehavior.AllowGet });
        }



        public ActionResult SaveNewEvent(AEOEvents AEO_Events)
        {
            #region FUNCTION VARIABLE
            string HASH_Guid = Guid.NewGuid().ToString();
            var ShortGUID = Guid.NewGuid().ToString("N").ToLower().Replace("1", "").Replace("o", "").Replace("0", "").Substring(0, 6);
            int SessionUsersId = (int)Session["UsersId"];
            int SessionCompanyId = (int)Session["CompanyId"];
            var Time = DateTime.Now.ToLongTimeString();
            #endregion
            using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction()) //transaction
            {
                try
                {
                    var Code =  "AEID" + '-' + DateTime.Now.ToShortDateString().Replace("/", "").Replace("0", "");
                    db.AEOEvents_Insert(
                                           Code
                                         , AEO_Events.EventName
                                         , AEO_Events.StartDate
                                         , AEO_Events.EndDate
                                         , SessionCompanyId
                                         , true
                                         , SessionUsersId
                                         , Time
                                         , ShortGUID
                                         , HASH_Guid
                                         , AEO_Events.Venue

                                         );
                    dbTran.Commit();
                    string message = "SUCCESS";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    throw ex;
                }
            }
               
        }
        public ActionResult SaveNewEmployee(Employees Employees)
        {
            #region FUNCTION VARIABLE
            string HASH_Guid = Guid.NewGuid().ToString();
            var ShortGUID = Guid.NewGuid().ToString("N").ToLower().Replace("1", "").Replace("o", "").Replace("0", "").Substring(0, 6);
            var CompanyId = Convert.ToInt32(Session["UsersCompanyId"]);
            var BranchId = Convert.ToInt32(Session["UsersBranchId"]);
            var UsersId = Convert.ToInt32(Session["UsersId"]);

            var Time = DateTime.Now.ToLongTimeString();
            #endregion
            using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction()) //transaction
            {
                try
                {
                    var code = "EMRC" + '-' + DateTime.Now.ToShortDateString().Replace("/", "").Replace("0", "");
                    var emp =db.Employees_Insert(
                        code,
                        Employees.EmployeeName,
                        Employees.FatherHusbandName,
                        Employees.Cnic,
                        Employees.Nationality,
                        Employees.MartialStatus,
                        Employees.Gender,
                        Employees.DOB,
                        Employees.Age,
                        Employees.Email,
                        Employees.Phone,
                        Employees.Home,
                        Employees.CountryId,
                        Employees.CityId,
                        Employees.Address,
                        Employees.IncomeTaxStatus,
                        Employees.NTNNumber,
                        Employees.BankNameId,
                        Employees.BankBranchId,
                        Employees.AccountTitle,
                        Employees.AccountNumber,
                        Employees.BranchId,
                        Employees.DepartmentId,
                        Employees.DesignationId,
                        Employees.EmployeeTypeId,
                        Employees.JoiningDate,
                        HASH_Guid,
                        UsersId,
                        DateTime.Now,
                        CompanyId,
                        4// FOR SERVING
                    );
                    var AI = emp.FirstOrDefault();
                    dbTran.Commit();
                    string message = "SUCCESS";
                    return Json(new { Message = message,data=AI }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
        }
        public ActionResult SaveNewHRMAllowance(HRMAllowances HRMAllowances)
        {
            #region FUNCTION VARIABLE

            var Short_GUID = Guid.NewGuid().ToString("N").ToLower().Replace("1", "").Replace("o", "").Replace("0", "").Substring(0, 6);
            string HasH_GUID = Guid.NewGuid().ToString();
            var CompanyId = Convert.ToInt32(Session["UsersCompanyId"]);
            var BranchId = Convert.ToInt32(Session["UsersBranchId"]);
            var UsersId = Convert.ToInt32(Session["UsersId"]);

            var Time = DateTime.Now.ToLongTimeString();
            #endregion
            using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction()) //transaction
            {
                try
                {
                    var Code = "HRM-SA" + '-' + DateTime.Now.ToShortDateString().Replace("/", "").Replace("0", "");
                    db.HRMAllowances_Insert(
                                             Code
                                            , HRMAllowances.Description
                                            , HRMAllowances.AllowanceTypeId
                                            , HRMAllowances.Amount
                                            , HRMAllowances.Percentage
                                            , HRMAllowances.EffectiveFrom
                                            , HRMAllowances.ExpiredOn
                                            , Short_GUID
                                            , HasH_GUID
                                            , UsersId
                                            , DateTime.Now
                                            , CompanyId
                                            , 1// Active
                                            );
                  
                    dbTran.Commit();
                    string message = "SUCCESS";
                    return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
        }
        public ActionResult SaveNewSalaryStructure(HRMSalaryStructure HRMSalaryStructure)
        {
            #region FUNCTION VARIABLE
            var Short_GUID = Guid.NewGuid().ToString("N").ToLower().Replace("1", "").Replace("o", "").Replace("0", "").Substring(0, 6);
            string HasH_GUID = Guid.NewGuid().ToString();
            var CompanyId = Convert.ToInt32(Session["UsersCompanyId"]);
            var BranchId = Convert.ToInt32(Session["UsersBranchId"]);
            var UsersId = Convert.ToInt32(Session["UsersId"]);
            #endregion
            using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction()) //transaction
            {
                try
                {
                    var Code = "HSS" + '-' + DateTime.Now.ToShortDateString().Replace("/", "").Replace("0", "");
                   var Data = db.HRMSalaryStructure_Insert(
                                                     HRMSalaryStructure.EmployeeCode
                                                    ,Code
                                                    ,HRMSalaryStructure.BasicSalary
                                                    ,HRMSalaryStructure.EffectiveFrom
                                                    ,HRMSalaryStructure.ExpiryDate
                                                    ,Short_GUID
                                                    ,HasH_GUID
                                                    ,CompanyId
                                                    ,BranchId
                                                    ,1
                                                );
                    var AI = Data.FirstOrDefault();




                    dbTran.Commit();
                    string message = "SUCCESS";
                    return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
        }


    }
}