using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.DAL.DTO
{
    public class PCRIncome
    {
        public int Id { get; set; }
        public Guid? GuID { get; set; }
        public string Code { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string TransactionID { get; set; }
        public int? PaymentThroughId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DocType { get; set; }
        public int? DocumentStatus { get; set; }
        public bool? Status { get; set; }
        public int? BranchId { get; set; }
        public int? CompanyId { get; set; }
    }
}