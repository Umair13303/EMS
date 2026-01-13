using Microsoft.Reporting.WebForms;
using office360.Areas.PatientModuleArea.Data;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Areas.PatientModuleArea.Controllers
{
    public class ReportsController : Controller
    {
        // GET: PatientModuleArea/Reports
        public ActionResult PatientRegistrationReport(string Code)
        {
            HMSEntities db = new HMSEntities();
            var Type = "PDF";
            LocalReport lr = new LocalReport();
            string p = Path.Combine(Server.MapPath("~/Areas/PatientModuleArea/Reports/RDLC_Reports"), "PatientRegistrationReport.rdlc");
            lr.ReportPath = p;
            var Patient_Code = Code;
            var CompanyId = Session["CompanyId"];
            var @Param1 = new SqlParameter("@Code", Patient_Code);
            var @Param2 = new SqlParameter("@CompanyId", CompanyId);
            var data = db.Database.SqlQuery<Patient_GetDetailByCode_Result>("Patient_GetDetailByCode @Code, @CompanyId", new SqlParameter("@Code", string.IsNullOrEmpty(Code) ? DBNull.Value : (object)Code), new SqlParameter("@CompanyId", CompanyId)).ToList();
            ReportDataSource rd = new ReportDataSource("PatientRegistrationReportDataSet", data);
            lr.DataSources.Add(rd);
            string mt, enc, f;
            string[] s;
            Warning[] w;
            byte[] b = lr.Render(Type, null, out mt, out enc, out f, out s, out w);
            var Report = File(b, mt);

            return File(b, mt);
        }  
        public ActionResult MedicalPrescriptionReport(string AppointmentCode, string PatientCode)
        {
            HMSEntities db = new HMSEntities();
            var Type = "PDF";
            LocalReport lr = new LocalReport();
            string p = Path.Combine(Server.MapPath("~/Areas/PatientModuleArea/Reports/RDLC_Reports"), "MedicalPrescriptionReport.rdlc");
            lr.ReportPath = p;
            var CoyId = Session["CompanyId"];
            var @Param1 = new SqlParameter("@CompanyId", CoyId);
            var @Param2 = new SqlParameter("@AppointmentCode", AppointmentCode);
            var @Param3 = new SqlParameter("@PatientCode", PatientCode);
            var data = db.Database.SqlQuery<MedicalPrescriptions_GetDetailsByPrescriptionCode_Result>("MedicalPrescriptions_GetDetailsByPrescriptionCode @CompanyId,@AppointmentCode,@PatientCode", @Param1, @Param2, @Param3).ToList();
            ReportDataSource rd = new ReportDataSource("MedicalPrescriptionReportDataSet", data);
            lr.DataSources.Add(rd);
            string mt, enc, f;
            string[] s;
            Warning[] w;
            byte[] b = lr.Render(Type, null, out mt, out enc, out f, out s, out w);
            var Report = File(b, mt);

            return File(b, mt);
        }
        public ActionResult PatientAppointmentReport(string AppointmentCode, string PatientCode)
        {
            HMSEntities db = new HMSEntities();
            var Type = "PDF";
            LocalReport lr = new LocalReport();
            string p = Path.Combine(Server.MapPath("~/Areas/PatientModuleArea/Reports/RDLC_Reports"), "PatientAppointmentReport.rdlc");
            lr.ReportPath = p;
            var CoyId = Session["CompanyId"];
            var @Param1 = new SqlParameter("@CompanyId", CoyId);
            var @Param2 = new SqlParameter("@AppointmentCode", AppointmentCode);
            var @Param3 = new SqlParameter("@PatientCode", PatientCode);
            var data = db.Database.SqlQuery<MedicalPrescriptions_GetDetailsByPrescriptionCode_Result>("MedicalPrescriptions_GetDetailsByPrescriptionCode @CompanyId,@AppointmentCode,@PatientCode", @Param1, @Param2, @Param3).ToList();
            ReportDataSource rd = new ReportDataSource("PatientAppointmentReportDataSet", data);
            lr.DataSources.Add(rd);
            string mt, enc, f;
            string[] s;
            Warning[] w;
            byte[] b = lr.Render(Type, null, out mt, out enc, out f, out s, out w);
            var Report = File(b, mt);

            return File(b, mt);
        }

    }
}