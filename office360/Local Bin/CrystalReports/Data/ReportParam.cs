using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Areas.CrystalReports.Data
{
    public class ReportParam
    {

    }

    public static class ReportPath
    {
        private static string BasePhisicalReportPath = HttpContext.Current.Server.MapPath("~/Areas/Reporting/Reports/");
        private static string AccountsViewerReportsPath = HttpContext.Current.Server.MapPath("~/Areas/Reporting/Reports/Students/");
        private static string FeeReceiptsViewerReportsPath = HttpContext.Current.Server.MapPath("~/Areas/Reporting/Reports/Accounts/");
        private static string University = HttpContext.Current.Server.MapPath("~/Areas/Reporting/Reports/University/");
        private static string UniversityAdmissionAnnouncement = HttpContext.Current.Server.MapPath("~/Areas/Reporting/Reports/UniAdmissionAnnouncementReport/");


        /*Testing Reports start*/
        public static string EmptyReport = BasePhisicalReportPath + "EmptyReport.rpt";
        /*Testing Report close*/
        public static string BankReceipt = AccountsViewerReportsPath + "BankReceipt.rpt";
        public static string StudentLedger = AccountsViewerReportsPath + "StudentLedgerReport.rpt";
        public static string StudentChallan = AccountsViewerReportsPath + "StudentChallan.rpt";
        public static string Student = AccountsViewerReportsPath + "StudentReport.rpt";
    }
        public class Filters
    {
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public DateTime StartDateTimeRpt { get; set; }
        public DateTime EndDateTimeRpt { get; set; }
        public int? ExportType { get; set; }
        public Guid? Id { get; set; } //Id could be used for multi purpose i.e bill id, report type id, account id etc.       
        public string ReportTitle { get; set; }
        public string MonthIds { get; set; }
        public int? BranchId { get; set; }
        public int? ClassId { get; set; }
        public int? Month { get; set; }
        public string ChallanNos { get; set; }
        public string ChallanIds { get; set; }
        public Guid? ChallanId { get; set; }
        public string MonthInstallment { get; set; }

        public string StudentId { get; set; }
        public string DegreeId { get; set; }
        public Guid? SemesterId { get; set; }
        public string MonthAndYear { get; set; }
        public string CalenderId { get; set; }
        /*
        string StudentId,
        string DegreeId,
        Guid? SemesterId,
        string MonthAndYear
            */
    }

}