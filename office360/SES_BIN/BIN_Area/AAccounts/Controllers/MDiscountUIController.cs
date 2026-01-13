using Newtonsoft.Json;
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
using static office360.Models.General.DBListCondition;
using static office360.Models.General.HttpStatus;

namespace office360.Areas.AAccounts.Controllers
{
    public class MDiscountUIController : Controller
    {
        SESEntities db = new SESEntities();
        int? StatusCode = 0;
        int? _Exe = 0;


        #region RENDER VIEWS
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewAccDiscountType(_SqlParameters PostedData)
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
        public ActionResult AccDiscountTypeList(_SqlParameters PostedData)
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

        #region POPULATE DROP DOWN LIST

        public ActionResult GET_MT_STRUCTUREDISCOUNTTYPE_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_STRUCTUREDISCOUNTTYPE_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_STRUCTUREDISCOUNTTYPE_DETAILBYID(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_STRUCTUREDISCOUNTTYPE_INFO_BY_GUID(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region POPULATE DATATABLE LIST

        #region POPULATE DATATABLE LIST FOR STRUCTURE FEE TYPE
        public ActionResult GET_MT_STRUCTUREDISCOUNTTYPE_LIST_BYPARAM_FORDT(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_STRUCTUREFEETYPE_LIST_BYSEARCHPARAM(PostedData).ToList();
            return Json(new { success = true, data = DATA }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region INSERT / UPDATE DATA INTO DATABASE FOR DBO AccDiscountType
        [HttpPost]
        public ActionResult Update_Insert_AccDiscountType(_SqlParameters PostedData)
        { 
            _Exe = Common.DataBaseProcedures.AAccounts.InsertIntoDB.StructureDiscountType_UPDATE_INSERT(PostedData);
            var data = new { Message = HttpStatus.HTTPTransactionMessagByStatusCode(_Exe), StatusCode = _Exe };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}