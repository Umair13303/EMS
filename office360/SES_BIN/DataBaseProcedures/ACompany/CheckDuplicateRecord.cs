using office360.Common.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.DBListCondition;
using static office360.Models.General.HttpStatus;

namespace office360.Common.DataBaseProcedures.ACompany
{
    public class CheckDuplicateRecord
    {
        #region FUNCTION FOR :: COUNT CHECK::IF DATA ALREAD EXIST FROM MAIN DB/BY PARAMETER
        //public static List<_SqlParameters> CountCheck_AccFeeChallan(_SqlParameters PostedData)
        //{
        //    using (var db = new SESEntities())
        //    {
        //        using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
        //        {
        //            var DATA = 
        //               // db.AccFeeChallan
        //               //  .Where(AFC =>
        //               //   AFC.SessionId == PostedData.SessionId && AFC.ClassId == PostedData.ClassId
        //               //&& AFC.RegistrationTypeId == PostedData.RegistrationTypeId && AFC.ClassRegistrationId == PostedData.ClassRegistrationId
        //               //&& AFC.FeeStructureId == PostedData.FeeStructureId && AFC.BranchId == Session_Manager.BranchId
        //               //&& AFC.CompanyId == Session_Manager.CompanyId
        //               //        )
        //               //  .Select(x => new _SqlParameters { Id = x.Id, Description = x.Description }).ToList();
        //            return DATA;
        //        }
        //    }
        //}
        public static int? ISEXIST_APPSESSION_FOR_CLASS(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                int? Response = (int?)HttpResponses.CODE_SUCCESS;
                #region CHECK :: IF SESSION WITH FileStatus.Open_ADMISSION EXIST FOR A CLASS
                var ClassIdsList = PostedData.ClassIds?.Split(',').ToList() ?? new List<string> { "0" };
                var DATA = db.AppSession
                    .Where(x =>
                             x.Status == true
                          && x.DocumentStatus == (int?)Models.General.DocumentStatus.DocStatus.Open_ADMISSION
                          && x.CompanyId == Session_Manager.CompanyId
                          ).ToList()
                          .Where(x => x.ClassIds.Split(',').Any(classId => ClassIdsList.Contains(classId))).Select(x => new _SqlParameters { Id = x.Id })
                          .ToList();
                #endregion

                return DATA.Count;
            }
        }
        #endregion

    }
}