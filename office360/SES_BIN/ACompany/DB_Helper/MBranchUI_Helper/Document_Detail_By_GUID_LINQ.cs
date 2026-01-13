using System.Linq;
using System.Web;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.Http_Server_Status;
using static office360.Models.General.DBListCondition;
using DocumentStatus = office360.Models.General.DocumentStatus;
using System.Data.Entity.Infrastructure;
using office360.Models.EDMX;
using office360.Models.General;
using office360.Common.CommonHelper;
using System.Data.Entity.Core.Objects;
using office360.Extensions;
using System;
using System.Collections.Generic;

namespace office360.Areas.ACompany.DB_Helper.MBranchUI_Helper
{
    public class Document_Detail_By_GUID_LINQ
    {
        #region HELPER FOR :: GET DATA USING LINQ (GENERAL_BRANCH) ::-- MAIN DB
        public static List<_SqlParameters> GET_MT_GENERALBRANCH_INFO_BY_GUID(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();

            using (SESEntities db = new SESEntities())
            {
                DATA = ((List<_SqlParameters>)
                       (from GB in db.GeneralBranch
                        where GB.CompanyId == Session_Manager.CompanyId && GB.GuID == PostedData.GuID
                        select new _SqlParameters
                        {
                            Id = GB.Id,
                            GuID = GB.GuID,
                            Code = GB.Code,
                            Description = GB.Description,
                            CampusTypeId = GB.CampusTypeId,
                            OrganizationTypeId = GB.OrganizationTypeId,
                            CountryId = GB.CountryId,
                            CityId = GB.CityId,
                            Address = GB.Address,
                            ContactNo = GB.ContactNo,
                            EmailAddress = GB.EmailAddress,
                            NTNNo = GB.NTNNo,
                            Remarks = GB.Remarks,
                            RollCallSystemId = db.GeneralBranchSetting.Where(GBS => GBS.CampusId == GB.Id).Select(GBS => GBS.RollCallSystemId).FirstOrDefault(),
                            BillingMethodId = db.GeneralBranchSetting.Where(GBS => GBS.CampusId == GB.Id).Select(GBS => GBS.BillingMethodId).FirstOrDefault(),
                            StudyLevelIds = db.GeneralBranchSetting.Where(GBS => GBS.CampusId == GB.Id).Select(GBS => GBS.StudyLevelIds).FirstOrDefault(),
                            StudyGroupIds = db.GeneralBranchSetting.Where(GBS => GBS.CampusId == GB.Id).Select(GBS => GBS.StudyGroupIds).FirstOrDefault(),
                            PolicyPeriodId = db.GeneralBranchSetting.Where(GBS => GBS.CampusId == GB.Id).Select(GBS => GBS.PolicyPeriodId).FirstOrDefault(),
                            ChallanMethodId = db.GeneralBranchSetting.Where(GBS => GBS.CampusId == GB.Id).Select(GBS => GBS.ChallanMethodId).FirstOrDefault()

                        }).ToList());

                return DATA;
            }
        }
        #endregion

    }
}