using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.DAL.DTO
{
    public class User
    {
        public int Id { get; set; }
        public Nullable<System.Guid> GuID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string AllowedCampusIds { get; set; }
        public Nullable<bool> IsLogIn { get; set; }
        public Nullable<bool> IsDeveloper { get; set; }
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