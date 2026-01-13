using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.DAL.DTO
{
    public class PCRLedger_ByDateRange_VM
    {
        public Guid? GuID { get; set; }
        public string Code { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}