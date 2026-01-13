using office360.Common.CommonHelper;
using office360.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static office360.Models.General.HttpStatus;

namespace office360.Areas.AAccounts.Controllers
{
    public class MChallanUIController : Controller
    {
        SESEntities db = new SESEntities();
        int? StatusCode = 0;
        int? _Exe = 0;

        #region RENDER VIEW 
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewAccFeeChallan(_SqlParameters PostedData)
        {
            #region PASS VIEW
            _Exe = GetAllListFromDB.GetAllowedUsersRightsByParameter(PostedData.RightId);
            #endregion
            if (_Exe == (int?)HttpResponses.CODE_SUCCESS)
            {
                ViewBag.Title = PostedData.DisplayName.ToSafeString();
                ViewBag.DB_OperationType = PostedData.OperationType.ToSafeString();
                return View();
            }
            else
            {
                return RedirectToAction(_ActionsURL.LogIn, _Controller.Home, new { area = "" });
            }
        }
        #endregion

        #region DROP DOWN LIST HELPER
        public ActionResult GET_MT_GENERALBRANCH_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_APPSESSION_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_APPSESSION_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_APPSESSIONDETAIL_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_APPSESSIONDETAIL_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_APPCLASS_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_APPCLASS_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_CHALLANTYPE(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_ChallanType(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CHECK :: IF DATA ALREADY EXIST
        public ActionResult CHECK_FEESTRUCTURE_FOR_CLASS(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.CheckDuplicateRecord.ISEXIST_FEESTRUCTURE_FOR_CLASS(PostedData);
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POPULATE DATATABLE LIST

        #region POPULATE DATATABLE LIST FOR FEE STRUCTURE/CHALLAN DETAIL
        public ActionResult GET_MT_ACCFEESTRUCTUREDETAIL_BYPARAM_FORDT(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_ACCFEESTRUCTUREDETAIL_LIST_BYPARAM(PostedData).ToList();
            return Json(new { success = true, data = DATA }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region POPULATE DATATABLE LIST FOR ENROLLED STUDENTS/REGISTRATION
        public ActionResult GET_MT_ENRREGISTRATION_BYPARAM_FORDT(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AStudent.GetDataFromSP.GET_MT_ENRSTUDENT_LIST_SEARCHPARAM(PostedData).ToList();
            return Json(new { success = true, data = DATA }, JsonRequestBehavior.AllowGet);
        }

        #endregion



        #endregion

        #region INSERT / UPDATE DATA INTO DATABASE FOR DBO AccFeeChallan



        #endregion

    }
}