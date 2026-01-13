using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.DAL.DTO
{
    public class Company
    {
        public int Id { get; set; }
        public Nullable<System.Guid> GuID { get; set; }
        public string Code { get; set; }
        public string CompanyName { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> CityId { get; set; }
        public string AddressLine { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CompanyWebsite { get; set; }
        public string UploadLogo { get; set; }
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