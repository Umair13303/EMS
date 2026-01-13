using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.General
{
    public class SQLParamters
    {
        // Guid or Nullable<Guid>
        public bool? IsDiscount { get; set; }
        public bool? IsOnAdmission { get; set; }
        public bool? IsRefundable { get; set; }
        public bool? IsSecurity { get; set; }
        public bool? Recurring { get; set; }
        public bool? Status { get; set; }
        public bool? IsOnExceedingAmount { get; set; }
        public bool? IsLogIn { get; set; }
        public bool? IsDeveloper { get; set; }
        public bool? IsFatherAlive { get; set; }
        public bool? IsEnteryTestRequired { get; set; }
        public bool? IsInterviewRequired { get; set; }
        public bool? IsSemesterRequired { get; set; }
        public bool? IsByPercentage { get; set; }
        public bool? IsByAmount { get; set; }
        public bool? IsOtherFee { get; set; }
        public bool? IsRecurring { get; set; }

        public DateTime? AdmissionStartDate { get; set; }
        public DateTime? AdmissionEndDate { get; set; }
        public DateTime? DateofBirth { get; set; }
        public DateTime? ClassEndDate { get; set; }
        public DateTime? PeriodEndOn { get; set; }
        public DateTime? ClassStartDate { get; set; }
        public DateTime? PeriodStartOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? SessionEndDate { get; set; }
        public DateTime? SessionStartDate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public decimal? TotalFeeExclusive { get; set; }
        public decimal? WHTax { get; set; }
        public decimal? TotalFee { get; set; }
        public decimal? ActualFeeAmount { get; set; }
        public decimal? ChargedFeeAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? FeeAmount { get; set; }
        public decimal? FixedCharges { get; set; }
        public decimal? GrossRecievable { get; set; }
        public decimal? LateFee { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public decimal? NetRecievable { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? SlabAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? Amount { get; set; }

        public Guid? GuID { get; set; }

        public int? AcademicYearId { get; set; }
        public int? AdmissionId { get; set; }
        public int? AdmissionSessionId { get; set; }
        public int? AppSessionId { get; set; }
        public int? AllowedChallanNo { get; set; }
        public int? AssetAccountId { get; set; }
        public int? BillingMethodId { get; set; }
        public int? CampusId { get; set; }
        public int? CampusTypeId { get; set; }
        public int? ChallanMethodId { get; set; }
        public int? ChallanNo { get; set; }
        public int? ChargingMethodId { get; set; }
        public int? CityId { get; set; }
        public int? ClassId { get; set; }
        public int? ClassRegistrationId { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? CostOfSaleAccountId { get; set; }
        public int? CountryId { get; set; }
        public int? CreatedBy { get; set; }
        public int? DocumentStatus { get; set; }
        public int? DocumentStatusBranch { get; set; }
        public int? DocumentStatusSession { get; set; }
        public int? EducationLevelId { get; set; }
        public int? EnrollmentTypeId { get; set; }
        public int? ExportType { get; set; }
        public int? FeeCategoryId { get; set; }
        public int? FeeStructureId { get; set; }
        public int? FeeTypeId { get; set; }
        public int? GenderId { get; set; }
        public int? Id { get; set; }
        public int? LiabilityAccountId { get; set; }
        public int? MartialStatusId { get; set; }
        public int? MonthsNo { get; set; }
        public int? NationalityId { get; set; }
        public int? NumberOfChallan { get; set; }
        public int? OccupationId { get; set; }
        public int? OrganizationTypeId { get; set; }
        public int? ParentStudyLevelId { get; set; }
        public int? PolicyPeriodId { get; set; }
        public int? RegistrationTypeId { get; set; }
        public int? RelationshipId { get; set; }
        public int? ReligionId { get; set; }
        public int? UserId { get; set; }
        public int? RightId { get; set; }
        public int? RevenueAccountId { get; set; }
        public int? RollCallSystemId { get; set; }
        public int? SearchById { get; set; }
        public int? SessionId { get; set; }
        public int? StructureId { get; set; }
        public int? StudyGroupId { get; set; }
        public int? StudyLevelId { get; set; }
        public int? StudySchemeId { get; set; }
        public int? TransactionMonth { get; set; }
        public int? WHTaxPolicyId { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DocType { get; set; }
        public int? EmployeeId { get; set; }
        public int? RoleId { get; set; }
        public int? URLTypeId { get; set; }
        public int? RegisteredPeriodId { get; set; }
        public int? AdmissionCategoryId { get; set; }
        public int? SemesterId { get; set; }
        public int? SubjectId { get; set; }
        public int? AccountTypeId { get; set; }
        public int? AccountCategoryId { get; set; }
        public int? FinancialStatementId { get; set; }

        public string ActionCondition { get; set; }
        public string Address { get; set; }
        public string AssetAccount { get; set; }
        public string BranchName { get; set; }
        public string CallingCode { get; set; }
        public string ChargingMethod { get; set; }
        public string Class { get; set; }
        public string ClassIds { get; set; }
        public string CnicNo_FormBNo { get; set; }
        public string CoaCatagoryIds { get; set; }
        public string Code { get; set; }
        public string CompanyName { get; set; }
        public string ContactNo { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string ExtraClass { get; set; }
        public string FeeCatagory { get; set; }
        public string FeeName { get; set; }
        public string FilterBy { get; set; }
        public string FirstName { get; set; }
        public string FunctionCondition { get; set; }
        public string InputText { get; set; }
        public string LastName { get; set; }
        public string LiabilityAccount { get; set; }
        public string CostOfSaleAccount { get; set; }
        public string DB_IF_PARAM { get; set; }
        public string LookUpActionCondition { get; set; }
        public string Menu { get; set; }
        public string SubMenu { get; set; }
        public string MobileNumber { get; set; }
        public string NTNNo { get; set; }
        public string OperationType { get; set; }
        public string SubOperationType { get; set; }
        public string ParentCnic { get; set; }
        public string ParentName { get; set; }
        public string ParentNICNo { get; set; }
        public string Remarks { get; set; }
        public string SettingGuID { get; set; }
        public string ReportTitle { get; set; }
        public string ReportType { get; set; }
        public string ResedenitalAddress { get; set; }
        public string ReturnType { get; set; }
        public string RevenueAccount { get; set; }
        public string RightPath { get; set; }
        public string SESActionCondition { get; set; }
        public string Session { get; set; }
        public string SessionPeriod { get; set; }
        public string StatusCode { get; set; }
        public string StudyGroup { get; set; }
        public string StudyGroupIds { get; set; }
        public string StudyLevel { get; set; }
        public string StudyLevelIds { get; set; }
        public string RegistrationNumber { get; set; }
        public string StudentName { get; set; }
        public string StudentCNIC { get; set; }
        public string BirthDate { get; set; }
        public string DomicileDistrict { get; set; }
        public string FatherName { get; set; }
        public string FatherCNIC { get; set; }
        public string GuardianName { get; set; }
        public string GuardianCNIC { get; set; }
        public string StudentMobile { get; set; }
        public string StudentEmail { get; set; }
        public string ParentMobile { get; set; }
        public string LandLine { get; set; }
        public string EmergencyMobile { get; set; }
        public string SearchParameter { get; set; }
        public string CompanyWebsite { get; set; } 
        public string LogoPath { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Name { get; set; } 
        public string UserName { get; set; } 
        public string Password { get; set; } 
        public string AllowedCampusIds { get; set; }
        public string Semester { get; set; }
        public string Subject { get; set; }
        public string FeeCategoryIds { get; set; }
        public string FeeType { get; set; }


        public static explicit operator List<object>(SQLParamters v)
        {
            throw new NotImplementedException();
        }
    }
}