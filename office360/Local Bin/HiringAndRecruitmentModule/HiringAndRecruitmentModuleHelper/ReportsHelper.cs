using Microsoft.Reporting.WebForms;
using office360.Models.EDMX;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using Grpc.Core;
using office360.Areas.HiringAndRecruitmentModule.Data;
using office360.Models.General;

namespace office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper
{
    public class ReportsHelper
    {
        HMSEntities db = new HMSEntities();

        public byte[] GenerateEmployeeReport(HtmlViewerFilters Filters, out byte[] ReportFile)
        {
            string Type = "PDF";
            LocalReport lr = new LocalReport();
            string reportPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Areas/HiringAndRecruitmentModule/Reports/RDLC_Reports"), "EmployeeRegistrationReport.rdlc");
            lr.ReportPath = reportPath;
            SqlParameter param1 = new SqlParameter("@CompanyId", Sessions.CompanyId);
            SqlParameter param2 = new SqlParameter("@Code", Filters.EmployeeCode);
            List<Employees_GetByParameter_Result> data = db.Database.SqlQuery<Employees_GetByParameter_Result>("Employees_GetByParameter @CompanyId, @Code", param1, param2).ToList();
            ReportDataSource rd = new ReportDataSource("EmployeeRegistrationReportDataSet", data);
            lr.DataSources.Add(rd);
            byte[] ReportBytes = lr.Render(Type);
            ReportFile = ReportBytes;
            return ReportBytes;
        }

        public byte[] GenerateAdvertismentReport(HtmlViewerFilters Filters , out byte[] ReportFile)
        {
            string Type = "PDF";
            LocalReport lr = new LocalReport();
            string reportPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Areas/HiringAndRecruitmentModule/Reports/RDLC_Reports"), "AdvertismentReport.rdlc");
            lr.ReportPath = reportPath;
            SqlParameter param1 = new SqlParameter("@CompanyId", Sessions.CompanyId);
            SqlParameter param2 = new SqlParameter("@AdvertismentCode", Filters.AdvertismentCode);
            List<HRAdvertisment_GetDetailByParameter_Result> data = db.Database.SqlQuery<HRAdvertisment_GetDetailByParameter_Result>("HRAdvertisment_GetDetailByParameter @CompanyId, @AdvertismentCode", param1, param2).ToList();
            ReportDataSource rd = new ReportDataSource("AdvertismentReportDataSet", data);
            lr.DataSources.Add(rd);
            byte[] ReportBytes = lr.Render(Type);
            ReportFile = ReportBytes;
            return ReportBytes;
        }
        public byte[] GenerateApplicantCVReport(HtmlViewerFilters Filters, out byte[] ReportFile)
        {
            string Type = "PDF";
            LocalReport lr = new LocalReport();
            string reportPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Areas/HiringAndRecruitmentModule/Reports/RDLC_Reports"), "ApplicantCVReport.rdlc");
            lr.ReportPath = reportPath;
            SqlParameter param1 = new SqlParameter("@CaseType", 2);
            SqlParameter param2 = new SqlParameter("@CompanyId", Sessions.CompanyId);
            SqlParameter param3 = new SqlParameter("@AdvertismentCode", Filters.AdvertismentCode);
            SqlParameter param4 = new SqlParameter("@ApplicantCode", Filters.ApplicantCode);
            var data = db.Database.SqlQuery<HRApplicants_GetListByParameter_Result>("HRAdvertismentApplicants_GetListByParameter  @CaseType, @CompanyId,@AdvertismentCode,@ApplicantCode", param1, param2, param3, param4);
            ReportDataSource rd = new ReportDataSource("ApplicantCVDataSet", data);
            lr.DataSources.Add(rd);
            string mt, enc, f;
            string[] s;
            Warning[] w;
            byte[] ReportBytes = lr.Render(Type, null, out mt, out enc, out f, out s, out w);
            ReportFile = ReportBytes;
            return ReportBytes;
        }
    }
}