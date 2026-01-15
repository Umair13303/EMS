using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Areas.Account.Controllers
{
    public class PCRReportManagment_UIController : Controller
    {
        public ActionResult View_PCRLedger_UI()
        {
            ViewBag.Title = "View Petty Cash Ledger";
            return View();
        }
        public ActionResult View_PCRCategorySummary_UI()
        {
            ViewBag.Title = "View Petty Cash Summary";
            return View();
        }
    }
}