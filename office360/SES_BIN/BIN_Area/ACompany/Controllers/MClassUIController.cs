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
    public class MClassUIController : Controller
    {
        SESEntities db = new SESEntities();
        int? StatusCode = 0;
        int? _Exe = 0;

        #region RENDER VIEW 
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewAppClass(_SqlParameters PostedData)
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
        public ActionResult AppClassList(int? RightId)
        {
            #region PASS VIEW
            _Exe = GetAllListFromDB.GetAllowedUsersRightsByParameter(RightId);
            #endregion
            if (_Exe == (int?)HttpResponses.CODE_SUCCESS)
            {
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
        public ActionResult GET_MT_GENERALBRANCH_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_CampusType(_SqlParameters PostedData)
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
        public ActionResult GET_DATA_BY_PARAMETER(_SqlParameters PostedData)
        {

            List<_SqlParameters> DATA = null;

            switch (PostedData.ActionCondition)
            {
                //case nameof(SESActionCondition.GET_MT_GENERALCOMPANY_BYPARAMETER):
                //    DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALCOMPANY_BYPARAM(PostedData).ToList();
                //    break;

                //case nameof(SESActionCondition.GET_MT_GENERALBRANCH_BYPARAMETER):
                //    DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_BYPARAM(PostedData).ToList();
                //    break;
                //case nameof(LookUpActionCondition.GET_LK1_CAMPUSTYPE):
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_CampusType(PostedData).ToList();
                //    break;

                //case nameof(LookUpActionCondition.GET_LK1_ORGANIZATIONTYPE):
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_OrganizationType(PostedData).ToList();
                //    break;

                //case nameof(LookUpActionCondition.GET_LK1_COUNTRY):
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_Country(PostedData).ToList();
                //    break;

                //case nameof(LookUpActionCondition.GET_LK1_CITY_BYPARAMETER):
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_City(PostedData).ToList();
                //    break;

                //case nameof(LookUpActionCondition.GET_LK1_POLICYPERIOD):
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_PolicyPeriod(PostedData).ToList();
                //    break;
                //case nameof(LookUpActionCondition.GET_LK1_ROLLCALLSYSTEM):
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_RollCallSystem(PostedData).ToList();
                //    break;

                //case nameof(LookUpActionCondition.GET_LK1_BILLINGMETHOD):
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_BillingMethod(PostedData).ToList();
                //    break;

                //case nameof(LookUpActionCondition.GET_LK1_STUDYGROUP_BYPARAMTER):
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_StudyGroup(PostedData).ToList();
                //    break;

                //case nameof(LookUpActionCondition.GET_LK1_STUDYLEVEL_BYPARAMTER):
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_StudyLevel(PostedData).ToList();
                //    break;

            }
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region INSERT / UPDATE DATA INTO DATABASE FOR DBO AppClass
        public ActionResult UpSert_Into_AppClass(_SqlParameters PostedData)
        {
            _Exe = Common.DataBaseProcedures.ACompany.InsertIntoDB.Update_Insert_AppClass(PostedData);
            var data = new { Message = HTTPTransactionMessagByStatusCode(_Exe), StatusCode = _Exe };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}