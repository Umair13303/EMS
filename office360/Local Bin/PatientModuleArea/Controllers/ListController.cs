using office360.Models.EDMX;
using office360.Areas.PatientModuleArea.Data;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static office360.Areas.PatientModuleArea.Data.Patients;

namespace office360.Areas.PatientModuleArea.Controllers
{
    public class ListController : Controller
    {
        HMSEntities db = new HMSEntities();

        // GET: PatientModuleArea/List
        [HttpGet]
        public JsonResult ViewPatientList(PatientsSearchParameters PatientsSearchParameters)
        {
            #region CONVERION OF DATES
            var SDate = PatientsSearchParameters.StartDate;
            var EDate = PatientsSearchParameters.EndDate;
            DateTime StartDate = DateTime.Parse(SDate);
            DateTime EndDate = DateTime.Parse(EDate);
            #endregion
            var @Param1 = new SqlParameter("@StartDate", StartDate);
            var @Param2 = new SqlParameter("@EndDate", EndDate );

            var data = db.Database.SqlQuery<Patients_GetListByDateRange_Result>("Patients_GetListByDateRange @StartDate,@EndDate",@Param1,@Param2).ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ViewPatientAppointmentList(PatientsSearchParameters PatientsSearchParameters)
        {
            var CoyId = Session["CompanyId"];
            var @Param1 = new SqlParameter("@CompanyId", CoyId);
            var SDate = PatientsSearchParameters.StartDate;
            var EDate = PatientsSearchParameters.EndDate;
            DateTime StartDate = DateTime.Parse(SDate);
            DateTime EndDate = DateTime.Parse(EDate);
            var @Param2 = new SqlParameter("@StartDate", StartDate);
            var @Param3 = new SqlParameter("@EndDate", EndDate);

            var data = db.Database.SqlQuery<PatientAppointment_GetListByDOAAndStatus_Result>("PatientAppointment_GetListByDOAAndStatus @CompanyId,@StartDate,@EndDate", @Param1,@Param2, @Param3).ToList();
            if (data.Count > 0)
            {
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string message = "NO RECORD FOUND";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });

            }
        }
    }
}