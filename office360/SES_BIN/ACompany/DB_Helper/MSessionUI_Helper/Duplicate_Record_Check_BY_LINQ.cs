using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static office360.Models.General.DBListCondition;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.Http_Server_Status;

namespace office360.Areas.ACompany.DB_Helper.MSessionUI_Helper
{
    public class Duplicate_Record_Check_BY_LINQ
    {
        #region HELPER FOR :: CHECK IF APP_SESSION(Open_ADMISSION) ALREADY EXIST FOR CLASS 
        public static int? IS_EXIST_APPSESSION_BYCLASSIDS(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                int? Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
                try
                {
                    switch (PostedData.OperationType)
                    {

                        case nameof(DB_OperationType.INSERT_DATA_INTO_DB):
                            var ClassIdsList = PostedData.ClassIds?.Split(',').ToList() ?? new List<string> { "0" };

                            var DATA = db.AppSession
                                .Where(x =>
                                    x.CompanyId == Session_Manager.CompanyId &&
                                    x.DocumentStatus == (int?)DocStatus.Open_ADMISSION &&
                                    x.Status == true
                                )
                                .ToList() // Pull data into memory
                                .Where(x => x.ClassIds.Split(',') // Split ClassIds in memory
                                    .Any(classId => ClassIdsList.Contains(classId))) // Match against PostedData.ClassIds
                                .Select(x => new _SqlParameters { Id = x.Id })
                                .ToList();

                            if (DATA.Count == 0)
                            {
                                Response=(int?)Http_DB_Response.CODE_AUTHORIZED;

                            }
                            else
                            {
                                Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
                            }
                            break;

                        case nameof(DB_OperationType.UPDATE_DATA_INTO_DB):
                            Response = (int?)Http_DB_Response.CODE_AUTHORIZED;
                            break;

                        default:
                            Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
                            break;


                    }
                    return Response;
                }
                catch (Exception Ex)
                {
                    return Http_Server_Status.Http_DB_Response.CODE_UN_KNOWN_ACTIVITY.ToInt();

                }

            }
        }
        #endregion
    }
}