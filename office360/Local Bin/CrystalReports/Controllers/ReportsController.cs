//using CrystalDecisions.CrystalReports.Engine;
//using office360.Models.EDMX;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Web.Mvc;

//namespace office360.Areas.CrystalReports.Controllers
//{
//    public static class ListExtensions
//    {
//        public static DataTable ToDataTable<T>(this List<T> list, string tableName)
//        {
//            DataTable dataTable = new DataTable(tableName);
//            var properties = typeof(T).GetProperties();

//            foreach (var prop in properties)
//            {
//                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
//            }

//            foreach (var item in list)
//            {
//                var row = dataTable.NewRow();
//                foreach (var prop in properties)
//                {
//                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
//                }
//                dataTable.Rows.Add(row);
//            }

//            return dataTable;
//        }
//    }

//    public class ReportsController : Controller
//    {
//        HMSEntities db = new HMSEntities();

//        //public ActionResult ExportCustomers()
//        //{
//        //    List<Employees_GetByParameter_Result> employeeList = new List<Employees_GetByParameter_Result>();
//        //    employeeList = db.Employees_GetByParameter(1, null).ToList();

//        //    DataTable dataTable = employeeList.ToDataTable("Employees");

//        //    DataSet dataSet = new DataSet("EmployeeDataSet");
//        //    dataSet.Tables.Add(dataTable);

//        //    ReportDocument report = new ReportDocument();
//        //    report.Load(Path.Combine(Server.MapPath("~/Areas/CrystalReports/Reports"), "EmployeeReports.rpt"));
//        //    report.SetDataSource(dataSet);

//        //    byte[] reportBytes = ExportReportToPDF(report);

//        //    return File(reportBytes, "application/pdf", "EmployeeReports.pdf");
//        //}

//        //private byte[] ExportReportToPDF(ReportDocument report)
//        //{
//        //    Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

//        //    byte[] bytes;
//        //    using (var memoryStream = new MemoryStream())
//        //    {
//        //        stream.CopyTo(memoryStream);
//        //        bytes = memoryStream.ToArray();
//        //    }

//        //    return bytes;
//        //}
//    }
//}
