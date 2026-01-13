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
using static office360.Models.General.Http_Server_Status;
using static office360.Models.General.DBListCondition;
using office360.GlobalHelper.LookUp;


namespace office360.Areas.ACompany.Controllers
{
    public class MSessionUIController : Controller
    {
        SESEntities db = new SESEntities();
        int? StatusCode = 0;
        int? _Exe = 0;


        /*---------------------- ** ACTION RESULTS FOR :: RENDER OF VIEW ** ------------------------------------------------------------------- */

        #region ACTION RESULT FOR :: RENDER VIEW
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateUpdateAppSession(_SqlParameters PostedData)
        {
            #region PASS VIEW
            _Exe = GetAllListFromDB.GetAllowedUsersRightsByParameter(PostedData.RightId);
            #endregion
            if (_Exe == (int?)Http_DB_Response.CODE_SUCCESS)
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

        /*---------------------- ** ACTION RESULTS FOR :: RENDER OF DROP DOWN LIST FROM DB_LOOKUP USING LINQUERY ** --------------------------- */

        #region ACTION RESULT FOR :: RENDER DROP DOWN FROM DB_LOOKUP -- LINQ-QUERY
        public ActionResult GET_LK1_ENROLLMENTTYPES(_SqlParameters PostedData)
        {
            var DATA = LookUp_GetDataFromDB_LINQ.GET_LK1_EnrollmentType(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /*---------------------- ** ACTION RESULTS FOR :: RENDER OF DROP DOWN LIST FROM DB_MAIN USING STOREDPROCEDURE ** ---------------------- */

        #region ACTION RESULT FOR :: RENDER DROP DOWN FROM DB_MAIN -- STORED PROCEDURE
        public ActionResult GET_MT_GENERALBRANCH_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = DB_Helper.MBranchUI_Helper.DATA_FROM_SP.GET_MT_GENERALBRANCH_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_APPCLASS_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = DB_Helper.MClassUI_Helper.DATA_FROM_SP.GET_MT_APPCLASS_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        #endregion

        /*---------------------- ** ACTION RESULTS FOR :: DOCUMENT SETTING FROM DB_MAIN USING DBO_FUNCTION** --------------------------------- */

        #region ACTION RESULT FOR :: GET DOCUMENT DETAIL (DBO.GENERAL_BRANCH_SETTING) -- DBO_FUNCTION

        public ActionResult FN_GENERALBRANCHSETTING_INFO_BY_CAMPUSID(_SqlParameters PostedData)
        {
            var DATA = DB_Helper.MBranchUI_Helper.Document_Detail_By_Id_DBO_FN.FN_GENERALBRANCHSETTING_INFO_BY_CAMPUSID(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /*---------------------- ** ACTION RESULTS FOR :: DATABASE OPERATION BY USER (VERIFY DUPLICATE RECORDS) ** ---------------------- */
        #region ACTION RESULT FOR :: VERIFY DUPLICATE (DBO.APP_SESSION) -- LINQUERY

        public ActionResult GET_MT_APPSESSION_ISNOTEXIST(_SqlParameters PostedData)
        {
            _Exe = DB_Helper.MSessionUI_Helper.Duplicate_Record_Check_BY_LINQ.IS_EXIST_APPSESSION_BYCLASSIDS(PostedData);
            var DATA = new { Message = Http_Server_Status.HTTP_DB_TransactionMessagByStatusCode(_Exe), StatusCode = _Exe };

            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        #endregion


        /*---------------------- ** ACTION RESULTS FOR :: DATABASE OPERATION BY USER (INSERT/UPDATE/DELETE OF SESSION) ** ---------------------- */

        #region ACTION RESULT FOR :: INSERT/UPDATE/DELETE INTO DBO_APPSESSION & DBO_APPSESSIONDETAIL
        [HttpPost]
        public ActionResult UpSert_Into_AppSession(_SqlParameters PostedData, List<_SqlParameters> PostedDataDetail)
        {
            _Exe = DB_Helper.MSessionUI_Helper.CUD_BULK_Operation.Update_Insert_AppSession_BulkOperation(PostedData, PostedDataDetail);
            var data = new { Message = Http_Server_Status.HTTP_DB_TransactionMessagByStatusCode(_Exe), StatusCode = _Exe };
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion

        /*---------------------- ** ACTION RESULTS FOR :: EDIT (LOAD DOCUMENT OF SESSION & GET DETAIL BY SESSION_GUID) ** ------------------------ */

        #region ACTION RESULT FOR :: SEARCH DROP DOWN FROM DB_MAIN -- STORED PROCEDURE
        public ActionResult GET_MT_APPSESSION_BYPARAMETER_SEARCH(_SqlParameters PostedData)
        {
            var DATA = DB_Helper.MSessionUI_Helper.DATA_FROM_SP.GET_MT_APPSESSION_BYPARAM(PostedData).ToList();
            return Json(new { data = DATA }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ACTION RESULT FOR :: GET DOCUMENT DETAIL (DBO.APP_SESSION & DBO.APP_SESSION_DETAIL) -- LINQ-QUERY
        public ActionResult GET_MT_APPSESSION_INFOBYGUID(_SqlParameters PostedData)
        {
            var DATA = DB_Helper.MSessionUI_Helper.Document_Detail_By_GUID_LINQ.GET_MT_APPSESSION_INFO_BY_GUID(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_APPSESSIONDETAIL_INFOBYGUID(_SqlParameters PostedData)
        {
            var DATA = DB_Helper.MSessionUI_Helper.Document_Detail_By_GUID_LINQ.GET_MT_APPSESSIONDETAIL_INFO_BY_ID(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}