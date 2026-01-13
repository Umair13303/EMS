using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.General
{
    public class AppConstant
    {
        public class Path
        {
            public static string BasePhisicalPath
            {
                get
                {
                    return HttpContext.Current.Server.MapPath("~/");
                }
            }
            public static string SitePath
            {
                get { return BasePhisicalPath; }
            }
            public static string SubdomainReports
            {
                get { return SitePath + "AReports\\"; }
            }
            public static string ReportLogo
            {
                get { return SitePathImages + "CompanyLogos\\"; }
            }
            public static string SitePathImages
            {
                get { return SitePath + "Uploads\\"; }
            }

        }

    }
}