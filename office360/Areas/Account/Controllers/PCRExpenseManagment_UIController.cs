using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Areas.Account.Controllers
{
    public class PCRExpenseManagment_UIController : Controller
    {
        #region REGION FOR :: RENDER VIEWS
        public ActionResult CreateUpdate_PCRExpense_UI()
        {
            ViewBag.Title = "Add / Edit Expense Record";
            return View();
        }
        #endregion
    }
}