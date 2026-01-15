using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using office360.Models.DAL;
using office360.Models.General;
using static office360.Models.General.HttpServerStatus;

namespace office360.Areas.Account.DALHelper
{
    public class Record_Validator
    {

        #region DUPLICATE CHECK METHOD FOR dbo.PCRExpense
        public static int? Is_Exist_PCRExpense(string OperationType, Guid? GuID, string Description, string InvoiceNumber, decimal? NetAmount)
        {
            bool IsRecordExist = false;
            int? Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
            using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
            {
                switch (OperationType)
                {
                    case nameof(DB_OperationType.INSERT_DATA_INTO_DB):
                        #region IN CASE OF INSERT :: CHECK IF ENTERY RECORD EXIST , BASED ON DATA ENTERED
                        IsRecordExist = dal.PCRExpense
                            .Any(x =>
                                 x.Description == Description || x.InvoiceNumber == InvoiceNumber || x.NetAmount == NetAmount
                                && x.Status == true
                            );
                        #endregion
                        if (!IsRecordExist)
                            Response = (int?)Http_DB_Response.CODE_AUTHORIZED;
                        else
                            Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
                        break;

                    case nameof(DB_OperationType.UPDATE_DATA_INTO_DB):
                        IsRecordExist = dal.PCRExpense
                            .Any(x =>
                                 x.GuID == GuID
                                && x.Status == true
                            );
                        if (!IsRecordExist)
                            Response = (int?)Http_DB_Response.CODE_DATA_DOES_NOT_EXIST;
                        else
                            Response = (int?)Http_DB_Response.CODE_AUTHORIZED;
                        break;
                    default:
                        Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
                        break;
                }


            }

            return Response;

        }
        #endregion
        #region DUPLICATE CHECK METHOD FOR dbo.PCRIncome
        public static int? Is_Exist_PCRIncome(string OperationType, Guid? GuID, string Description)
        {
            bool IsRecordExist = false;
            int? Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
            using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
            {
                switch (OperationType)
                {
                    case nameof(DB_OperationType.INSERT_DATA_INTO_DB):
                        #region IN CASE OF INSERT :: CHECK IF ENTERY RECORD EXIST , BASED ON DATA ENTERED
                        IsRecordExist = dal.PCRIncome
                            .Any(x =>
                                 x.Description == Description
                                && x.Status == true
                            );
                        #endregion
                        if (!IsRecordExist)
                            Response = (int?)Http_DB_Response.CODE_AUTHORIZED;
                        else
                            Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
                        break;

                    case nameof(DB_OperationType.UPDATE_DATA_INTO_DB):
                        IsRecordExist = dal.PCRIncome
                            .Any(x =>
                                 x.GuID == GuID
                                && x.Status == true
                            );
                        if (!IsRecordExist)
                            Response = (int?)Http_DB_Response.CODE_DATA_DOES_NOT_EXIST;
                        else
                            Response = (int?)Http_DB_Response.CODE_AUTHORIZED;
                        break;
                    default:
                        Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
                        break;
                }


            }

            return Response;

        }
        #endregion

        #region DUPLICATE CHECK METHOD FOR dbo.PCRIncome
        public static int? Is_Exist_HREmployee(string OperationType, Guid? GuID, string CNICNumber)
        {
            bool IsRecordExist = false;
            int? Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
            using (EMSIntegCubeEntities dal = new EMSIntegCubeEntities())
            {
                switch (OperationType)
                {
                    case nameof(DB_OperationType.INSERT_DATA_INTO_DB):
                        #region IN CASE OF INSERT :: CHECK IF ENTERY RECORD EXIST , BASED ON DATA ENTERED
                        IsRecordExist = dal.HREmployee
                            .Any(x =>
                                 x.CNICNumber == CNICNumber
                                && x.Status == true
                            );
                        #endregion
                        if (!IsRecordExist)
                            Response = (int?)Http_DB_Response.CODE_AUTHORIZED;
                        else
                            Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
                        break;

                    case nameof(DB_OperationType.UPDATE_DATA_INTO_DB):
                        IsRecordExist = dal.HREmployee
                            .Any(x =>
                                 x.GuID == GuID
                                && x.Status == true
                            );
                        if (!IsRecordExist)
                            Response = (int?)Http_DB_Response.CODE_DATA_DOES_NOT_EXIST;
                        else
                            Response = (int?)Http_DB_Response.CODE_AUTHORIZED;
                        break;
                    default:
                        Response = (int?)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
                        break;
                }


            }

            return Response;

        }
        #endregion
    }
}