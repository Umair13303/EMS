using office360.Models.EDMX;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace office360.Areas.CRMModuleAEO.Controllers
{
    public class SaveDataIntoDbController : Controller
    {
        HMSEntities db = new HMSEntities();
        // GET: CRMModuleAEO/SaveDataIntoDb
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


    }
}