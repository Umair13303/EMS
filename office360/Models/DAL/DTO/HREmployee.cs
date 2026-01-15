using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace office360.Models.DAL.DTO
{
    public class HREmployee
    {
        public int Id { get; set; }

        public Guid? GuID { get; set; }

        public string Code { get; set; }

        public string EmployeeName { get; set; }

        public DateTime? DateofJoining { get; set; }

        public int? EmploymentTypeId { get; set; }

        public int? DesignationId { get; set; }

        public string CNICNumber { get; set; }

        public string MobileNumber { get; set; }

        public string EmailAddress { get; set; }

        public int? BankId { get; set; }

        public string AccountNumber { get; set; }

        public DateTime? DateofResign { get; set; }

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