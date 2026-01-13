using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using office360.Models.General;

namespace office360.Areas.Account.Controllers
{
    public class PCRIncomeManagment_UIController : Controller
    {
        #region REGION FOR :: RENDER VIEWS
        public ActionResult CreateUpdate_PCRIncome_UI()
        {
            return View();
        }
        #endregion

    }
}