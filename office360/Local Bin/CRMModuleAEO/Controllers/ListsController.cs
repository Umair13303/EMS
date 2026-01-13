using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Areas.CRMModuleAEO.Controllers
{
    public class ListsController : Controller
    {
        HMSEntities db = new HMSEntities();
        // GET: CRMModuleAEO/Lists
        public ActionResult AEOEventsList()
        {
            var @Param1 = new SqlParameter("@CompanyId", Session["CompanyId"]);
            var data = db.Database.SqlQuery<AEOEvents_GetList_Result>("AEOEvents_GetList @CompanyId", @Param1).ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulateEmployeeList()
        {
            var @Param1 =  (int)Session["CompanyId"];
            List<Employees_GetByParameter_Result> data = new List<Employees_GetByParameter_Result>();
            data = db.Employees_GetByParameter(@Param1,null).ToList<Employees_GetByParameter_Result>();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
    }
}