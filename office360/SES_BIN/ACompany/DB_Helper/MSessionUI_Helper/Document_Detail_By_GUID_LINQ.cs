using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Areas.ACompany.DB_Helper.MSessionUI_Helper
{
    public class Document_Detail_By_GUID_LINQ
    {
        #region HELPER FOR :: GET DATA USING LINQ (APP_SESSION) ::-- MAIN DB
        public static List<_SqlParameters> GET_MT_APPSESSION_INFO_BY_GUID(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();

            using (SESEntities db = new SESEntities())
            {
                DATA = ((List<_SqlParameters>)
                       (from AS in db.AppSession
                        where AS.CompanyId == Session_Manager.CompanyId && AS.GuID == PostedData.GuID
                        select new _SqlParameters
                        {
                            Id = AS.Id,
                            GuID = AS.GuID,
                            Code = AS.Code,
                            CampusId = AS.CampusId,
                            EnrollmentTypeId = AS.EnrollmentTypeId,
                            Description = AS.Description,
                            SessionStartDate = AS.SessionStartDate,
                            SessionEndDate = AS.SessionEndDate,
                            ClassIds = AS.ClassIds,
                        }).ToList()) ;
                return DATA;

            }
        }
        
        public static List<_SqlParameters> GET_MT_APPSESSIONDETAIL_INFO_BY_ID(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();

            using (SESEntities db = new SESEntities())
            {
                 DATA = (from ASD in db.AppSessionDetail
                            join AS in db.AppSession on ASD.SessionId equals AS.Id
                            where AS.CompanyId == Session_Manager.CompanyId && AS.GuID == PostedData.GuID
                            select new _SqlParameters
                            {
                                Id = ASD.Id,
                                GuID = ASD.GuID,
                                Description = ASD.Description,
                                PeriodStartOn = ASD.PeriodStartOn,
                                PeriodEndOn = ASD.PeriodEndOn,
                            }).ToList();
                return DATA;

            }
        }
        #endregion

    }

}