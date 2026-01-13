using office360.Common.CommonHelper;
using office360.Common.DataBaseProcedures;
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
using static office360.Models.General.DBListCondition;

namespace office360.Areas.ACompany.Controllers
{
    public class MBranchUIController : Controller
    {
        SESEntities db = new SESEntities();
        int? StatusCode = 0;
        int? _Exe = 0;

        #region RENDER VIEWS
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewGeneralBranch(_SqlParameters PostedData)
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

        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult GeneralBranchList(_SqlParameters PostedData)
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

        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewGeneralBranchEmailSetting(_SqlParameters PostedData)
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
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult GeneralBranchEmailSettingList(_SqlParameters PostedData)
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

        public ActionResult GET_MT_GENERALCOMPANY_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALCOMPANY_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_CAMPUSTYPE(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_CampusType(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_ORGANIZATIONTYPE(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_OrganizationType(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_COUNTRY(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_Country(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_CITY_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_City(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_POLICYPERIOD(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_PolicyPeriod(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_CHALLANMETHOD_BYPARAMTER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_ChallanMethod(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_ROLLCALLSYSTEM(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_RollCallSystem(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_BILLINGMETHOD(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_BillingMethod(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_STUDYGROUP_BYPARAMTER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_StudyGroup(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_STUDYLEVEL_BYPARAMTER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_StudyLevel(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_GENERALBRANCH_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_GENERALBRANCH_DETAILBYID(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_INFO_BY_GUID(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region POPULATE DATATABLE LIST

        #region POPULATE DATATABLE LIST FOR DBO GENERAL BRANCH
        public ActionResult GET_MT_GENERALBRANCH_LIST_BYPARAM_FORDT(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_LIST_SEARCHPARAM(PostedData).ToList();
            return Json(new { success = true, data = DATA }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region INSERT / UPDATE DATA INTO DATABASE FOR DBO GeneralBranch
        [HttpPost]
        public ActionResult UpSert_Into_GeneralBranch(_SqlParameters PostedData)
        {
            _Exe = Common.DataBaseProcedures.ACompany.InsertIntoDB.Update_Insert_GeneralBranch(PostedData);
            var data = new { Message = HttpStatus.HTTPTransactionMessagByStatusCode(_Exe), StatusCode = _Exe };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}