using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using office360.Areas.CrystalReports.Data;
using office360.Models.EDMX;

namespace office360.Areas.CrystalReports.Helpers
{
    public class StudentHelper
    {
        public static List<Employees_GetByParameter_Result> GetInfoByStudentId(Filters _filters)
        {
            List<Employees_GetByParameter_Result> lst = new List<Employees_GetByParameter_Result>();
            using (var db = new HMSEntities())
            {
                lst = db.Employees_GetByParameter(1,null).ToList();
            }
            return lst;
        }

    }
}