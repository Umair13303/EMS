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
    public class MFeeUIController : Controller
    {
        SESEntities db = new SESEntities();
        int? StatusCode = 0;
        int? _Exe = 0;


        #region RENDER VIEWS
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewAccFeeType(_SqlParameters PostedData)
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
        public ActionResult CreateNewAccFeeStructure(_SqlParameters PostedData)
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
        public ActionResult AccFeeTypeList(_SqlParameters PostedData)
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
        public ActionResult AccFeeStructureList(_SqlParameters PostedData)
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
        public ActionResult GET_LK1_FEECATAGORY(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_FeeCatagory(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_CHARGINGMETHOD(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_ChargingMethod(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_WHTAXPOLICY_BYPARAMTER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_WHTaxPolicy(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GET_MT_GENERALCOMPANY_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALCOMPANY_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_GENERALBRANCH_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA  = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_APPSESSION_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_APPSESSION_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_APPCLASS_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_APPCLASS_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_STRUCTURECOAACCOUNT_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_STRUCTUREFEETYPE_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_STRUCTUREFEETYPE_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_ACCFEESTRUCTURE_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_ACCFEESTRUCTURE_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_ACCFEESTRUCTUREDETAIL_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_ACCFEESTRUCTUREDETAIL_LIST_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GET_MT_STRUCTUREFEETYPE_DETAILBYID(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_STRUCTUREFEETYPE_INFO_BY_GUID(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_ACCFEESTRUCTURE_DETAILBYID(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_ACCFEESTRUCTURE_INFO_BY_GUID(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_ACCFEESTRUCTUREDETAIL_DETAILBYID(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_ACCFEESTRUCTUREDETAIL_INFO_BY_GUID(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CHECK DUPLICATE
        public ActionResult CHECK_FEESTRUCTURE_FOR_CLASS(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.CheckDuplicateRecord.ISEXIST_FEESTRUCTURE_FOR_CLASS(PostedData);
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region POPULATE DATATABLE LIST

        #region POPULATE DATATABLE LIST FOR STRUCTURE FEE TYPE
        public ActionResult GET_MT_STRUCTUREFEETYPE_LIST_BYPARAM_FORDT(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_STRUCTUREFEETYPE_LIST_BYSEARCHPARAM(PostedData).ToList();
            return Json(new { success = true, data = DATA }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POPULATE DATATABLE LIST FOR FEE STRUCTURE

        public ActionResult GET_MT_ACCFEESTRUCTURE_LIST_BYPARAM_FORDT(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_ACCFEESTRUCTURE_LIST_BYSEARCHPARAM(PostedData).ToList();
            return Json(new { success = true, data = DATA }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion

        #region INSERT / UPDATE DATA INTO DATABASE FOR DBO StructureFeeType
        [HttpPost]
        public ActionResult Update_Insert_StructureFeeType(_SqlParameters PostedData)
        {
            _Exe = Common.DataBaseProcedures.AAccounts.InsertIntoDB.StructureFeeType_UPDATE_INSERT(PostedData);
            var data = new { Message = HttpStatus.HTTPTransactionMessagByStatusCode(_Exe), StatusCode = _Exe };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region INSERT / UPDATE DATA INTO DATABASE FOR DBO AccFeeStructure
        [HttpPost]
        public ActionResult Update_Insert_AccFeeStructure(_SqlParameters PostedData,List<_SqlParameters> PostedDataDetail)
        {
            _Exe = Common.DataBaseProcedures.AAccounts.InsertIntoDB.AccFeeStructure_UPDATE_INSERT(PostedData, PostedDataDetail);
            var data = new { Message = HttpStatus.HTTPTransactionMessagByStatusCode(_Exe), StatusCode = _Exe };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        #endregion
    }
}