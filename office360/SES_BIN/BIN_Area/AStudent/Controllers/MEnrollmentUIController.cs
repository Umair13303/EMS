using office360.Common.CommonHelper;
using office360.Common.DataBaseProcedures;
using office360.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.HttpStatus;
using static office360.Models.General.DBListCondition;
using OfficeOpenXml;

namespace office360.Areas.AStudent.Controllers
{
    public class MEnrollmentUIController : Controller
    {
        SESEntities db = new SESEntities();
        int? StatusCode = 0;
        int? _Exe = 0;
        #region RENDER VIEW
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewEnrStudent(_SqlParameters PostedData)
        {
            #region PASS VIEW
            _Exe = GetAllListFromDB.GetAllowedUsersRightsByParameter(PostedData.RightId);
            #endregion
            if (_Exe == (int?)HttpResponses.CODE_SUCCESS)
            {
                ViewBag.Title = PostedData.DisplayName.ToSafeString();
                ViewBag.DB_OperationType = PostedData.OperationType.ToSafeString();

                ViewBag.SessionDetail = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_ACTIVE_MT_APPSESSION_DETAIL(PostedData);
                return View();
            }
            else
            {
                return RedirectToAction(_ActionsURL.LogIn, _Controller.Home, new { area = "" });
            }

        }
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewEnrStudentBulkOperation(_SqlParameters PostedData)
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
        public ActionResult EnrStudentList(_SqlParameters PostedData)
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
        public ActionResult GET_LK1_RELIGION(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_Religion(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_COUNTRY(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_Country(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_OCCUPATION(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_Occupation(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_LK1_ADMISSIONCATAGORY(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_AdmissionCatagory(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GET_MT_ACCFEESTRUCTURE_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_ACCFEESTRUCTURE_BYPARAM(PostedData).ToList();
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        #region DROP DOWN LIST BY SEARCH PARAMETER
        public ActionResult GET_MT_ENRSTUDENT_BYPARAMETER(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AStudent.GetDataFromSP.GET_MT_ENRSTUDENT_BYPARAM(PostedData).ToList();
            return Json(new { data = DATA }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region GET DATA FOR DOCUMENT BY GUID
        public ActionResult GET_MT_ENRSTUDENT_DETAILBYID(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AStudent.GetDataFromSP.GET_MT_ENRSTUDENT_INFO_BY_GUID(PostedData).ToList();
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

        #region POPULATE DATATABLE LIST FOR ENROLLED STUDENTS
        public ActionResult GET_MT_ENRSTUDENT_LIST_BYPARAM_FORDT(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AStudent.GetDataFromSP.GET_MT_ENRSTUDENT_LIST_SEARCHPARAM(PostedData).ToList();
            return Json(new { success = true, data = DATA }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion  

        #region INSERT / UPDATE DATA INTO DATABASE FOR DBO EnrStudent
        public ActionResult UpSert_Into_EnrStudent(List<_SqlParameters> PostedDataDetail)
        {
            _Exe = Common.DataBaseProcedures.AStudent.BulkDataOperation.Update_Insert_EnrStudent_BULKOperation(PostedDataDetail);
            var data = new { Message = HttpStatus.HTTPTransactionMessagByStatusCode(_Exe), StatusCode = _Exe };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion  
    }
}