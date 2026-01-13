using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.General
{
    public class Dictionary
    {
        public static readonly Dictionary<int?, string> RoleGuids = new Dictionary<int?, string>
        {
               { 1, "ROLE_ADMIN" },
               { 2, "ROLE_DEVELOPER" },
               { 3, "ROLE_MANAGER" },
               { 4, "ROLE_DEO" },
               { 5, "ROLE_TEACHER" },
               { 6, "ROLE_STUDENT" },
        };


        public static readonly int URLTYPEID_FORM = 1;
        public static readonly int URLTYPEID_REPORT_RDLC = 2;
    }
}