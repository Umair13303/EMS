using ClosedXML.Excel;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices;
using office360.Extensions;
using office360.Models.General;
using office360.Models.ViewComponents;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static office360.Models.General.HttpServerStatus;
using static office360.Models.General.UserRightMenu;

namespace REP.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult About()
        {
            return View();
        }
    }
}


