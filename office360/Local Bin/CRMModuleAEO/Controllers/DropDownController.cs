using office360.Models.EDMX;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace office360.Areas.CRMModuleAEO.Controllers
{
    public class DropDownController : Controller
    {
        HMSEntities db = new HMSEntities();

        // GET: CRMModuleAEO/DropDown
        [HttpPost]
        public ActionResult PopulateList()
        {
            return Json(db.AEOEvents.Select
           (x => new {
               Id = x.Id,
               CityName = x.Id,
               CountryId = x.Id
           }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateEmployeeTypes()
        {
            var Session_CompanyId = new SqlParameter("@CompanyId", Session["CompanyId"]);
            var data = db.Database.SqlQuery<LK_EmployeeTypes_GetListById_Result>("LK_EmployeeTypes_GetListById @CompanyId", Session_CompanyId).ToList();
            
            return Json(data, JsonRequestBehavior.AllowGet);
        } 
        public ActionResult PopulateBranches()
        {
            var Session_CompanyId = new SqlParameter("@CompanyId", Session["CompanyId"]);
            var Status = new SqlParameter("@Status", 1);
            var data = db.Database.SqlQuery<Branches_GetListByCoyId_Result>("Branches_GetListByCoyId @CompanyId,@Status", Session_CompanyId, Status).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        } 
        public ActionResult PopulateDesignations()
        {
            var Session_CompanyId = new SqlParameter("@CompanyId", Session["CompanyId"]);
            var data = db.Database.SqlQuery<Designation_GetListByCoyId_Result>("Designation_GetListByCoyId @CompanyId", Session_CompanyId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        } 
        public ActionResult PopulateDepartmentsByBranchId(int? BranchId)
        {
            var Session_CompanyId = new SqlParameter("@CompanyId", Session["CompanyId"]);
            var Param2 = new SqlParameter("@BranchId", BranchId);
            var data = db.Database.SqlQuery<LK_Departments_GetListByBranchId_Result>("LK_Departments_GetListByBranchId @CompanyId,@BranchId", Session_CompanyId, Param2).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulateBanksList()
        {
            return Json(db.LK_Banks.Select
           (x => new {
               Id = x.Id,
               BankName = x.BankName,
           }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulateBankBranchesByBankId(int? BankId)
        {
            var @Param1 = new SqlParameter("@BankId", BankId);
            var data = db.Database.SqlQuery<LK_BankBranches_GetListByBankId_Result>("LK_BankBranches_GetListByBankId @BankId", @Param1).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}

