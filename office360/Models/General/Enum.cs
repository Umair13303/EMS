using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.General
{
    public enum USER_ROLE
    {
        ROLE_ADMIN = 1,
        ROLE_DEVELOPER = 2,
        ROLE_MANAGER = 3,
        ROLE_DEO = 4,
    }
    public enum DB_OperationType
    {
        INSERT_DATA_INTO_DB = 1,
        UPDATE_DATA_INTO_DB = 2,
        DELETE_DATA_INTO_DB = 3
    }
    public enum DOCUMENT_TYPE
    {
        COMPANY = 1,
        USER = 2,
        RIGHT_SETTING = 3,
        USER_RIGHT = 4,
        BRANCH = 5,
        PETTY_CASH_INCOME=6,
        PETTY_CASH_EXPENSE = 7,
        HR_Employee = 8,

    }
    public enum DOCUMENT_STATUS
    {
        /*
         *  1.  ACTIVE DOCUMENTS WILL BE DISPLAYED ACCURATELY
            2.  IN-ACTIVE DOCUMENTS WILL DISPLAYED IN HISTORY AND RECORDS BUT WILL NOT BE SHOWN IN REPORTS
            3.  DELETED DOCUMENTS WILL HAVE NO IMPACT.
         */
        ACTIVE_COMPANY = 1,
        INACTIVE_COMPANY = 2,
        DELETED_COMPANY = 3,

        ACTIVE_USER = 4,
        INACTIVE_USER = 5,
        DELETED_USER = 6,

        ACTIVE_RIGHT_SETTING = 7,
        INACTIVE_RIGHT_SETTING = 8,

        ACTIVE_USER_RIGHT = 9,
        INACTIVE_USER_RIGHT = 10,

        ACTIVE_BRANCH = 11,
        INACTIVE_BRANCH = 12,
        DELETED_BRANCH = 13,

        ACTIVE_PETTY_CASH_INCOME = 14,
        DELETED_PETTY_CASH_INCOME = 15,

        ACTIVE_PETTY_CASH_EXPENSE = 16,
        DELETED_PETTY_CASH_EXPENSE = 17,

        ACTIVE_HR_Employee = 18,
        INACTIVE_HR_Employee = 19,
        DELETED_HR_Employee = 20,



    }
    public enum DATEPICKER_INCREMENT
    {
        FOR_ADMISSION_SESSION_ROUTINE = 365,
        FOR_ADMISSION_OPENING_ROUTINE = 20,
    }
   
    public enum CHART_OF_ACCOUNT_TYPE
    {
        ASSETS=1,
        LIABILITIES=2,
        CAPITAL_EQUITY=3,
        REVENUE_SALE=4,
        COST_OF_SALES=5,
        EXPENSES=6
    }
    public enum FEE_CATEGORY
    {
        ACADEMIC_FEE=1,
        OTHER_FEE=2,
    }
}