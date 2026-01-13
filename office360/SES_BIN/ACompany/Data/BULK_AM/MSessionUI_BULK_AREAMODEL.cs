using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Areas.ACompany.Data.BULK_AM
{
    public class MSessionUI_BULK_AREAMODEL
    {
    }
    public partial class AppSessionDetail_BULK_TT
    {
        public int Id { get; set; }
        public Nullable<System.Guid> GuID { get; set; }
        public Nullable<int> SessionId { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> PeriodStartOn { get; set; }
        public Nullable<System.DateTime> PeriodEndOn { get; set; }
        public Nullable<bool> Status { get; set; }
    }

}