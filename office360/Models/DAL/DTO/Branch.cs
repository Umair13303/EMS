using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.DAL.DTO
{
    public class Branch
    {
        public int Id { get; set; }
        public Nullable<System.Guid> GuID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<int> CampusTypeId { get; set; }
        public Nullable<int> OrganizationTypeId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> CityId { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string NTNNo { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> DocType { get; set; }
        public Nullable<int> DocumentStatus { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string Remarks { get; set; }
    }
}