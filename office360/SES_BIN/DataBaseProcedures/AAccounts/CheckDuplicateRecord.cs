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
using static office360.Models.General.HttpStatus;
using static office360.Models.General.DBListCondition;
using System.Globalization;
using System.Data.Entity.Core.Objects;

namespace office360.Common.DataBaseProcedures.AAccounts
{
    public class CheckDuplicateRecord
    {
        SESEntities db = new SESEntities();

        #region HELPER FOR :: DUPLICATE RECORD CHECK
        public static List<_SqlParameters> ISEXIST_FEESTRUCTURE_FOR_CLASS(_SqlParameters PostedData)
        {
            using (var db = new SESEntities())
            {
                var DATA = db.AccFeeStructure.Where(x =>

                                                 x.Status == true
                                                && x.DocumentStatus == (int?)DocStatus.Active_FEE_STRUCTURE
                                                && x.SessionId == PostedData.SessionId
                                                && x.ClassId == PostedData.ClassId

                                                && x.CompanyId == Session_Manager.CompanyId).Select(x => new _SqlParameters { Id = x.Id }).ToList();

                return DATA;
            }
        }

        #endregion

    }
}