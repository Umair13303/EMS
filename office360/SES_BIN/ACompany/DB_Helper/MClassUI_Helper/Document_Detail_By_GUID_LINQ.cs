using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Areas.ACompany.DB_Helper.MClassUI_Helper
{
    public class Document_Detail_By_GUID_LINQ
    {
        #region HELPER FOR :: GET DATA USING LINQ (APP_CLASS) ::-- MAIN DB
        public static List<_SqlParameters> GET_MT_APPCLASS_INFO_BY_GUID(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();

            using (SESEntities db = new SESEntities())
            {
                DATA = ((List<_SqlParameters>)
                       (from AC in db.AppClass
                        where AC.CompanyId == Session_Manager.CompanyId && AC.GuID == PostedData.GuID
                        select new _SqlParameters
                        {
                            Id = AC.Id,
                            GuID = AC.GuID,
                            CampusId = AC.CampusId,
                            Code = AC.Code,
                            Description = AC.Description,
                            StudyLevelId=AC.StudyLevelId,
                            StudyGroupId = AC.StudyGroupId,
                           
                        }).ToList());

                return DATA;
            }
        }

        #endregion
    }
}