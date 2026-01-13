using office360.Common.CommonHelper;
using office360.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using static office360.Models.General.DBListCondition;
using static office360.Models.General.HttpStatus;

namespace office360.Areas.ACompany.Controllers
{
    public class MAcademicSessionUIController : Controller
    {
        SESEntities db = new SESEntities();
        int? StatusCode = 0;
        int? _Exe = 0;
        #region RENDER VIEW 

        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewAppSession(_SqlParameters PostedData)
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

        public ActionResult FN_GENERALBRANCHSETTING_INFO_BY_CAMPUSID(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.FN_GENERALBRANCHSETTING_INFO_BY_CAMPUSID(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_CHALLANMETHOD_BYPARAMTER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_ChallanMethod(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_APPCLASS_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_APPCLASS_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_ENROLLMENTTYPES(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_EnrollmentType(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GET_DATA_BY_PARAMETER(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = null;

            //switch (PostedData.ActionCondition)
            //{
            //    case nameof(SESActionCondition.GET_MT_GENERALCOMPANY_BYPARAMETER):
            //        DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALCOMPANY_BYPARAM(PostedData).ToList();
            //        break;

            //    case nameof(SESActionCondition.GET_MT_GENERALBRANCH_BYPARAMETER):
            //        DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_BYPARAM(PostedData).ToList();
            //        break;

            //    case nameof(SESActionCondition.GET_MT_GENERALBRANCH_DETAILBYID):
            //        DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_INFO_BY_GUID(PostedData).ToList();
            //        break;
                
            //    case nameof(LookUpActionCondition.GET_LK1_CHALLANMETHOD_BYPARAMTER):
            //        DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_ChallanMethod(PostedData).ToList();
            //        break;

            //    case nameof(SESActionCondition.GET_MT_APPCLASS_BYPARAMETER):
            //        DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_APPCLASS_BYPARAM(PostedData).ToList();
            //        break;

            //    case nameof(LookUpActionCondition.GET_LK1_ENROLLMENTTYPES):
            //        DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_EnrollmentType(PostedData).ToList();
            //        break;
            //}
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CHECK DUPLICATE
        public ActionResult CHECK_APPSESSION_FOR_CLASS(_SqlParameters PostedData)
        {
            var data = office360.Common.DataBaseProcedures.ACompany.CheckDuplicateRecord.ISEXIST_APPSESSION_FOR_CLASS(PostedData);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region INSERT / UPDATE DATA INTO DATABASE FOR DBO AppSession
        [HttpPost]
        public ActionResult UpSert_Into_AppSession(_SqlParameters PostedData, List<_SqlParameters> PostedDataDetail)
        {
            _Exe = Common.DataBaseProcedures.ACompany.BulkDataOperation.Update_Insert_AppSession_BulkOperation(PostedData, PostedDataDetail);
            var data = new { Message = HttpStatus.HTTPTransactionMessagByStatusCode(_Exe), StatusCode = _Exe };
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion

    }
}