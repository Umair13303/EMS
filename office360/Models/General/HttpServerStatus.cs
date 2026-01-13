using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.General
{
    public class HttpServerStatus
    {

        public enum Http_DB_Response
        {
            CODE_DATA_ALREADY_EXIST = 107,
            CODE_SUCCESS = 200,
            CODE_DATA_DELETED = 250,

            CODE_DATA_UPDATED = 300,
            CODE_BAD_REQUEST = 400,
            CODE_UNAUTHORIZED = 401,
            CODE_AUTHORIZED = 402,
            CODE_DATA_DOES_NOT_EXIST = 404,
            CODE_INTERNAL_SERVER_ERROR = 500,
            CODE_UN_KNOWN_ACTIVITY = 510,
        }


        public static int Http_DB_ResponseByReturnValue(int? Response)
        {
            switch (Response)
            {
                case 107:
                    return (int)Http_DB_Response.CODE_DATA_ALREADY_EXIST;
                case 200:
                    return (int)Http_DB_Response.CODE_SUCCESS;
                case 250:
                    return (int)Http_DB_Response.CODE_DATA_DELETED;
                case 300:
                    return (int)Http_DB_Response.CODE_DATA_UPDATED;
                case 400:
                    return (int)Http_DB_Response.CODE_BAD_REQUEST;
                case 401:
                    return (int)Http_DB_Response.CODE_UNAUTHORIZED;
                case 402:
                    return (int)Http_DB_Response.CODE_AUTHORIZED;
                case 404:
                    return (int)Http_DB_Response.CODE_DATA_DOES_NOT_EXIST;
                default:
                    return (int)Http_DB_Response.CODE_UN_KNOWN_ACTIVITY;
            }
        }

        public static string HTTP_DB_TransactionMessagByStatusCode(int? StatusCode)
        {
            switch (StatusCode)
            {
                case (int)Http_DB_Response.CODE_DATA_ALREADY_EXIST:
                    return "THE DATA ALREADY EXIST.";
                case (int)Http_DB_Response.CODE_SUCCESS:
                    return "TRANSACTION HAS BEEN PERFORMED SUCCESSFULLY.";
                case (int)Http_DB_Response.CODE_DATA_DELETED:
                    return "TRANSACTION HAS BEEN DELETED SUCCESSFULLY.";
                case (int)Http_DB_Response.CODE_DATA_UPDATED:
                    return "TRANSACTION HAS BEEN UPDATED SUCCESSFULLY";
                case (int)Http_DB_Response.CODE_BAD_REQUEST:
                    return "BAD REQUEST PLEASE CHECK YOUR TRANSACTION.";
                case (int)Http_DB_Response.CODE_UNAUTHORIZED:
                    return "ACCESS DENIED.";
                case (int)Http_DB_Response.CODE_AUTHORIZED:
                    return "ACCESS PROVIDED.";
                case (int)Http_DB_Response.CODE_DATA_DOES_NOT_EXIST:
                    return "THE DATA DOES NOT EXIST IN SERVER SIDE.";
                case (int)Http_DB_Response.CODE_INTERNAL_SERVER_ERROR:
                    return "TRANSACTION ABORTED DUE TO INTERNAL SERVER.";
                default:
                    return "An unknown status code was returned.";
            }
        }
    }
}