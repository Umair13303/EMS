using office360.Models.EDMX;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using office360.Areas.PatientModuleArea.Data;
using static office360.Areas.PatientModuleArea.Data.Patients;

namespace office360.Areas.PatientModuleArea.Controllers
{
    public class DropDownListController : Controller
    {
        // GET: PatientModuleArea/DropDownList
        HMSEntities db = new HMSEntities();
        [HttpPost]
        public ActionResult PopulateCity()
        {
            return Json(db.LK_Cities.Select
           (x => new {
               Id = x.Id,
               CityName = x.CityName,
               CountryId = x.CountryId
           }).ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PoplatePatientName()
        {
            return Json(db.Patients.Select
           (x => new {
               Name = x.Name,
               Code = x.Code
           }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateDepartment()
        {
            var Session_CompanyId = new SqlParameter("@CompanyId", Session["CompanyId"]);
            var data = db.Database.SqlQuery<LK_Departments_GetListByCompanyId_Result>("LK_Departments_GetListByCompanyId @CompanyId", Session_CompanyId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulateDoctorsByCompanyId(string Doc_Code)
        {
            Doc_Code = null;
            var Session_CompanyId = new SqlParameter("@CompanyId", Session["CompanyId"]);
            var Code = new SqlParameter("@Code", Doc_Code == null ? DBNull.Value : (object)Doc_Code);

            var data = db.Database.SqlQuery<Employees_GetByParameter_Result>("Employees_GetByParameter @CompanyId,@Code", Session_CompanyId, Code).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }  
        public ActionResult PopulatePatientDetailsByCode(string Code)
        {
            var SessionCoy = Session["CompanyId"];
            var SessionPRI = Code;
            var Session_CompanyId = new SqlParameter("@CompanyId", SessionCoy);
            var Session_Code = new SqlParameter("@Code", SessionPRI);
            var data = db.Database.SqlQuery<Patient_GetDetailByCode_Result>("Patient_GetDetailByCode @Code, @CompanyId", Session_Code, Session_CompanyId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulatePatientNameAndAppointmentCode(PatientsModuleSession PatientsModuleSession)
        {
            int SessionCompanyId = (int)Session["CompanyId"];
            var @Paramerter1 = new SqlParameter("@AppointmentCode", PatientsModuleSession.AppointmentCode);
            var @Paramerter2 = new SqlParameter("@CompanyId", SessionCompanyId);
            var data = db.Database.SqlQuery<PatientAppointment_GetDetailByAppointmentId_Result>("PatientAppointment_GetDetailByAppointmentId @AppointmentCode, @CompanyId", @Paramerter1, @Paramerter2).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}