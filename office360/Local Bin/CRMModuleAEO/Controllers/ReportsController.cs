using Microsoft.Reporting.WebForms;
using office360.Models.EDMX;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Areas.CRMModuleAEO.Controllers
{
    public class ReportsController : Controller
    {
        HMSEntities db = new HMSEntities();

        // GET: CRMModuleAEO/Reports
        public ActionResult EmployeeReport(string Code)
        {
            var Type = "PDF";
            LocalReport lr = new LocalReport();
            string reportPath = Path.Combine(Server.MapPath("~/Areas/CRMModuleAEO/Reports/RDLC_Reports"), "EmployeeRegistrationReport.rdlc");
            lr.ReportPath = reportPath;
            var companyId = (int)Session["CompanyId"];
            var param1 = new SqlParameter("@CompanyId", companyId);
            var param2 = new SqlParameter("@Code", Code);
            var data = db.Database.SqlQuery<Employees_GetByParameter_Result>("Employees_GetByParameter @CompanyId, @Code", param1, param2).ToList();
            ReportDataSource rd = new ReportDataSource("EmployeeRegistrationReportDataSet", data);
            lr.DataSources.Add(rd);
            byte[] reportBytes = lr.Render(Type);
            return File(reportBytes, "application/pdf", "SalarySlips.pdf");
        }

    }
}