using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Reporting.WinForms;
using office360.Common.CommonHelper;
using office360.Common.DataBaseProcedures;
using office360.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using static office360.Models.General.HttpStatus;

namespace office360.Common.DataBaseProcedures.Reports
{
    public class Reports
    {
        DataSet ds = null;
        private static SESEntities db = new SESEntities();
    }

}