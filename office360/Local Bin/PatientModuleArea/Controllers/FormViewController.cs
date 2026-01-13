using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static office360.Areas.PatientModuleArea.Data.Patients;

namespace office360.Areas.PatientModuleArea.Controllers
{
    public class FormViewController : Controller
    {
        HMSEntities db = new HMSEntities();




        public ActionResult AddPatient()
        {
            #region CALLING STORE PROCEDURE TO VERIFY THAT IS USER AUTHORIZED TIO VIEW FORM
            var SessionUsersEmail = Session["UsersEmail"];
            var SessionPassword = Session["UsersPassword"];
            var SessionRequestedForm_Name = "AddPatient";
            var SessionCompanyId = Session["CompanyId"];
            if (SessionUsersEmail != null && Session != null && SessionCompanyId != null)
            {
                var @param1 = new SqlParameter("@Email", SessionUsersEmail == null ? DBNull.Value : (object)SessionUsersEmail);
                var @param2 = new SqlParameter("@Password", SessionPassword == null ? DBNull.Value : (object)SessionPassword);
                var @param3 = new SqlParameter("@FormName", SessionRequestedForm_Name);
                var @param4 = new SqlParameter("@ImplementedCompanyId", SessionCompanyId == null ? DBNull.Value : (object)SessionCompanyId);
                var data = db.Database.SqlQuery<MenuUsersFormSetting_AccessControl_Result>("MenuUsersFormSetting_AccessControl @Email, @Password, @FormName,@ImplementedCompanyId", @param1, @param2, @param3, @param4).ToList();
                if (data.Count > 0)
                {
                    var Authroization = data.FirstOrDefault();
                    var LoggedInEmail = Authroization.Email.ToString();
                    var LoggedInPassword = Authroization.Password;
                    if (LoggedInEmail != null && LoggedInPassword != null)
                    #endregion
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Home/LoginPage");
                    }
                }
            }
            return RedirectToAction("Home/LoginPage");
        }
        public ActionResult AddPatientAppointment()
        {
            #region CALLING STORE PROCEDURE TO VERIFY THAT IS USER AUTHORIZED TIO VIEW FORM
            var SessionUsersEmail = Session["UsersEmail"];
            var SessionPassword = Session["UsersPassword"];
            var SessionRequestedForm_Name = "AddPatientAppointment";
            var SessionCompanyId = Session["CompanyId"];
            if (SessionUsersEmail != null && Session != null && SessionCompanyId != null)
            {
                var @param1 = new SqlParameter("@Email", SessionUsersEmail == null ? DBNull.Value : (object)SessionUsersEmail);
                var @param2 = new SqlParameter("@Password", SessionPassword == null ? DBNull.Value : (object)SessionPassword);
                var @param3 = new SqlParameter("@FormName", SessionRequestedForm_Name);
                var @param4 = new SqlParameter("@ImplementedCompanyId", SessionCompanyId == null ? DBNull.Value : (object)SessionCompanyId);
                var data = db.Database.SqlQuery<MenuUsersFormSetting_AccessControl_Result>("MenuUsersFormSetting_AccessControl @Email, @Password, @FormName,@ImplementedCompanyId", @param1, @param2, @param3, @param4).ToList();
                if (data.Count > 0)
                {
                    var Authroization = data.FirstOrDefault();
                    var LoggedInEmail = Authroization.Email.ToString();
                    var LoggedInPassword = Authroization.Password;
                    if (LoggedInEmail != null && LoggedInPassword != null)
                    #endregion
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Home/LoginPage");
                    }
                }
            }
            return RedirectToAction("Home/LoginPage");
        }
        public ActionResult PatientListViewAll()
        {
            #region CALLING STORE PROCEDURE TO VERIFY THAT IS USER AUTHORIZED TIO VIEW FORM
            var SessionUsersEmail = Session["UsersEmail"];
            var SessionPassword = Session["UsersPassword"];
            var SessionRequestedForm_Name = "PatientListViewAll";
            var SessionCompanyId = Session["CompanyId"];
            if (SessionUsersEmail != null && Session != null && SessionCompanyId != null)
            {
                var @param1 = new SqlParameter("@Email", SessionUsersEmail == null ? DBNull.Value : (object)SessionUsersEmail);
                var @param2 = new SqlParameter("@Password", SessionPassword == null ? DBNull.Value : (object)SessionPassword);
                var @param3 = new SqlParameter("@FormName", SessionRequestedForm_Name);
                var @param4 = new SqlParameter("@ImplementedCompanyId", SessionCompanyId == null ? DBNull.Value : (object)SessionCompanyId);
                var data = db.Database.SqlQuery<MenuUsersFormSetting_AccessControl_Result>("MenuUsersFormSetting_AccessControl @Email, @Password, @FormName,@ImplementedCompanyId", @param1, @param2, @param3, @param4).ToList();
                if (data.Count > 0)
                {
                    var Authroization = data.FirstOrDefault();
                    var LoggedInEmail = Authroization.Email.ToString();
                    var LoggedInPassword = Authroization.Password;
                    if (LoggedInEmail != null && LoggedInPassword != null)
                    #endregion
                    {
                        return View("PatientAppointmentList");
                    }
                    else
                    {
                        return RedirectToAction("Home/LoginPage");
                    }
                }
            }
            return RedirectToAction("Home/LoginPage");
        }
        public ActionResult CreateMedicalPrescriptByAppointmentId(PatientsModuleSession PatientsModuleSession)
        {
            #region CALLING STORE PROCEDURE TO VERIFY THAT IS USER AUTHORIZED TIO VIEW FORM
            var SessionUsersEmail = Session["UsersEmail"];
            var SessionPassword = Session["UsersPassword"];
            var SessionRequestedForm_Name = "CreateMedicalPrescriptByAppointmentId";
            var SessionCompanyId = Session["CompanyId"];
            if (SessionUsersEmail != null && Session != null && SessionCompanyId != null)
            {
                var @param1 = new SqlParameter("@Email", SessionUsersEmail == null ? DBNull.Value : (object)SessionUsersEmail);
                var @param2 = new SqlParameter("@Password", SessionPassword == null ? DBNull.Value : (object)SessionPassword);
                var @param3 = new SqlParameter("@FormName", SessionRequestedForm_Name);
                var @param4 = new SqlParameter("@ImplementedCompanyId", SessionCompanyId == null ? DBNull.Value : (object)SessionCompanyId);
                var data = db.Database.SqlQuery<MenuUsersFormSetting_AccessControl_Result>("MenuUsersFormSetting_AccessControl @Email, @Password, @FormName,@ImplementedCompanyId", @param1, @param2, @param3, @param4).ToList();
                if (data.Count > 0)
                {
                    var Authroization = data.FirstOrDefault();
                    var LoggedInEmail = Authroization.Email.ToString();
                    var LoggedInPassword = Authroization.Password;
                    if (LoggedInEmail != null && LoggedInPassword != null)
                    #endregion
                    {
                        var @Paramerter1 = new SqlParameter("@AppointmentCode", PatientsModuleSession.AppointmentCode);
                        var @Paramerter2 = new SqlParameter("@CompanyId", SessionCompanyId);
                        var PatientDetails = db.Database.SqlQuery<PatientAppointment_GetDetailByAppointmentId_Result>("PatientAppointment_GetDetailByAppointmentId @AppointmentCode, @CompanyId", @Paramerter1, @Paramerter2).ToList();
                        if(PatientDetails.Count > 0)
                        {
                            var List = PatientDetails.FirstOrDefault();

                            #region VIEW BAG VARIABLES
                            ViewBag.PA_Id = List.PA_Id;
                            ViewBag.PA_AppointmentCode = List.PA_AppointmentCode;
                            ViewBag.PA_PatientCode = List.PA_PatientCode;
                            ViewBag.PA_DepartmentId = List.PA_DepartmentId;
                            ViewBag.PA_DepartmentName = List.PA_DepartmentName;
                            ViewBag.PA_DoctorCode = List.PA_DoctorCode;
                            ViewBag.PA_DateofAppointment = List.PA_DateofAppointment;
                            ViewBag.PA_TimeofAppointment = List.PA_TimeofAppointment;
                            ViewBag.PA_PatientEmail = List.PA_PatientEmail;
                            ViewBag.PA_PatientPhone = List.PA_PatientPhone;
                            ViewBag.PA_Message = List.PA_Message;
                            ViewBag.PA_AppointmentStatus = List.PA_AppointmentStatus;
                            ViewBag.PA_CompanyId = List.PA_CompanyId;
                            ViewBag.PA_BranchId = List.PA_BranchId;
                            ViewBag.PA_CreatedById = List.PA_CreatedById;
                            ViewBag.PA_UpdatedOn = List.PA_UpdatedOn;
                            ViewBag.P_Id = List.P_Id;
                            ViewBag.P_Code = List.P_Code;
                            ViewBag.P_Name = List.P_Name;
                            ViewBag.P_FatherHusbandName = List.P_FatherHusbandName;
                            ViewBag.P_Cnic = List.P_Cnic;
                            ViewBag.P_Gender = List.P_Gender;
                            ViewBag.P_Phone = List.P_Phone;
                            ViewBag.P_DOB = List.P_DOB;
                            ViewBag.P_Address = List.P_Address;
                            ViewBag.P_Age = List.P_Age;
                            ViewBag.P_Email = List.P_Email;
                            ViewBag.P_UserName = List.P_UserName;
                            ViewBag.P_Password = List.P_Password;
                            ViewBag.P_CityId = List.P_CityId;
                            ViewBag.P_CityName = List.P_CityName;
                            ViewBag.P_CountryId = List.P_CountryId;
                            ViewBag.P_CreatedDate = List.P_CreatedDate;
                            ViewBag.P_CreatedById = List.P_CreatedById;
                            ViewBag.P_IsActive = List.P_IsActive;
                            ViewBag.P_DiscontinuationDate = List.P_DiscontinuationDate;
                            ViewBag.P_ReasonToInActive = List.P_ReasonToInActive;
                            ViewBag.LKC_Id = List.LKC_Id;
                            ViewBag.LKC_CompanyName = List.LKC_CompanyName;
                            ViewBag.LKC_AddressLine1 = List.LKC_AddressLine1;
                            ViewBag.LKC_AddressLine2 = List.LKC_AddressLine2;
                            ViewBag.LKC_CountryId = List.LKC_CountryId;
                            ViewBag.LKC_CityId = List.LKC_CityId;
                            ViewBag.LKC_Contact = List.LKC_Contact;
                            ViewBag.LKC_Fax = List.LKC_Fax;
                            ViewBag.LKC_Email = List.LKC_Email;
                            ViewBag.LKC_Website = List.LKC_Website;
                            ViewBag.LKC_IncomeTaxTypeId = List.LKC_IncomeTaxTypeId;
                            ViewBag.LKC_NTN = List.LKC_NTN;
                            ViewBag.LKC_WithHoldingAgent = List.LKC_WithHoldingAgent;
                            ViewBag.LKC_SalesTaxTypeId = List.LKC_SalesTaxTypeId;
                            ViewBag.LKC_STN = List.LKC_STN;
                            ViewBag.LKC_CompanyTypeId = List.LKC_CompanyTypeId;
                            ViewBag.LKC_CurrencyCode = List.LKC_CurrencyCode;
                            ViewBag.LKC_DecimalPlaces = List.LKC_DecimalPlaces;
                            ViewBag.LKC_AccountingSystem = List.LKC_AccountingSystem;
                            ViewBag.LKC_PeriodStartDate = List.LKC_PeriodStartDate;
                            ViewBag.LKC_PeriodEndsAfter = List.LKC_PeriodEndsAfter;
                            ViewBag.LKC_LogoImageUrl = List.LKC_LogoImageUrl;
                            ViewBag.LKC_OwnerId = List.LKC_OwnerId;
                            ViewBag.LKC_Status = List.LKC_Status;
                            ViewBag.LKC_CreatedById = List.LKC_CreatedById;
                            ViewBag.LKC_CreatedTime = List.LKC_CreatedTime;
                            ViewBag.LKC_UpdatedById = List.LKC_UpdatedById;
                            ViewBag.LKC_UpdatedTime = List.LKC_UpdatedTime;
                            ViewBag.LKC_ShortGUID = List.LKC_ShortGUID;
                            ViewBag.LKC_HashGUID = List.LKC_HashGUID;
                            ViewBag.LKC_EOBIDeductionRegistered = List.LKC_EOBIDeductionRegistered;
                            ViewBag.LKC_EOBIDeductionNo = List.LKC_EOBIDeductionNo;
                            ViewBag.LKC_SocialSecurityRegistered = List.LKC_SocialSecurityRegistered;
                            ViewBag.LKC_SocialSecurityNO = List.LKC_SocialSecurityNO;
                            ViewBag.LKC_IsProvidentFundAdopted = List.LKC_IsProvidentFundAdopted;
                            ViewBag.LKC_ProvidentFundRegistered = List.LKC_ProvidentFundRegistered;
                            ViewBag.LKC_EmployerContribution = List.LKC_EmployerContribution;
                            ViewBag.LKC_EmployeeContribution = List.LKC_EmployeeContribution;
                            ViewBag.LKC_ICTAccountId = List.LKC_ICTAccountId;
                            ViewBag.LKC_ProvidentDeduType = List.LKC_ProvidentDeduType;
                            ViewBag.LKC_IsHREnable = List.LKC_IsHREnable;
                            ViewBag.LKC_IsDonorApplicable = List.LKC_IsDonorApplicable;
                            ViewBag.LKC_RptSlogan = List.LKC_RptSlogan;
                            ViewBag.LKC_IsBranchMang = List.LKC_IsBranchMang;
                            ViewBag.LKC_IsFundMang = List.LKC_IsFundMang;
                            ViewBag.LKC_IsAutoAmortizationFunds = List.LKC_IsAutoAmortizationFunds;
                            ViewBag.LKC_IsProjectApplicable = List.LKC_IsProjectApplicable;
                            ViewBag.LKC_PeriodType = List.LKC_PeriodType;
                            ViewBag.LKC_LandingPolicyId = List.LKC_LandingPolicyId;
                            ViewBag.LKC_TaxMethodologyTypeId = List.LKC_TaxMethodologyTypeId;
                            ViewBag.LKC_EPProductTypeId = List.LKC_EPProductTypeId;
                            ViewBag.LKC_IsWHSalesTax = List.LKC_IsWHSalesTax;
                            ViewBag.LKC_IsVoucherCompanyWise = List.LKC_IsVoucherCompanyWise;
                            ViewBag.LKC_IsMedicalExemption = List.LKC_IsMedicalExemption;
                            ViewBag.LKC_ExcemptionPersentage = List.LKC_ExcemptionPersentage;
                            ViewBag.LKC_IsVenueInOfficalAssignment = List.LKC_IsVenueInOfficalAssignment;
                            ViewBag.LKC_IsFixedAssetsAllowed = List.LKC_IsFixedAssetsAllowed;
                            ViewBag.LKC_IsBudgetAllowed = List.LKC_IsBudgetAllowed;
                            ViewBag.LKC_IsSandWichLeavePolicy = List.LKC_IsSandWichLeavePolicy;
                            ViewBag.LKC_IsCOAPrioritySorting = List.LKC_IsCOAPrioritySorting;
                            ViewBag.LKC_IsVehicle = List.LKC_IsVehicle;
                            ViewBag.LKC_RegisteredAreaIds = List.LKC_RegisteredAreaIds;
                            ViewBag.LKC_IsConstructionCompany = List.LKC_IsConstructionCompany;
                            ViewBag.LKC_IsOpenAllAccount = List.LKC_IsOpenAllAccount;
                            ViewBag.LKC_IsMIS = List.LKC_IsMIS;
                            ViewBag.LKC_IsSalaryTaxOpen = List.LKC_IsSalaryTaxOpen;
                            ViewBag.LKC_IsSalaryJV = List.LKC_IsSalaryJV;
                            ViewBag.LKC_TaxPeriodStartDate = List.LKC_TaxPeriodStartDate;
                            ViewBag.LKC_HRPeriodStartDate = List.LKC_HRPeriodStartDate;
                            ViewBag.LKC_ProvinceId = List.LKC_ProvinceId;
                            ViewBag.LKC_PFTypeId = List.LKC_PFTypeId;
                            ViewBag.LKC_PFAccountId = List.LKC_PFAccountId;
                            ViewBag.LKC_APManagementId = List.LKC_APManagementId;
                            ViewBag.LKC_ARManagementId = List.LKC_ARManagementId;
                            ViewBag.LKC_IsFedAllow = List.LKC_IsFedAllow;
                            ViewBag.LKC_IsFranchiseFeeAllow = List.LKC_IsFranchiseFeeAllow;
                            ViewBag.LKC_IsCustomerDistanceAllow = List.LKC_IsCustomerDistanceAllow;
                            ViewBag.PU_Id = List.PU_Id;
                            ViewBag.PU_UserName = List.PU_UserName;
                            ViewBag.PU_Email = List.PU_Email;
                            ViewBag.PU_Password = List.PU_Password;
                            ViewBag.PU_UsersFormGroupSettingId = List.PU_UsersFormGroupSettingId;
                            ViewBag.LKCI_Id = List.LKCI_Id;
                            ViewBag.LKCI_CityName = List.LKCI_CityName;
                            ViewBag.LKCI_CountryId = List.LKCI_CountryId;

                            #endregion
                        }
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Home/LoginPage");
                    }
                }
            }
            return RedirectToAction("Home/LoginPage");
        }

        public ActionResult MedicalHistory()
        {
            #region CALLING STORE PROCEDURE TO VERIFY THAT IS USER AUTHORIZED TIO VIEW FORM
            var SessionUsersEmail = Session["UsersEmail"];
            var SessionPassword = Session["UsersPassword"];
            var SessionRequestedForm_Name = "MedicalHistory";
            var SessionCompanyId = Session["CompanyId"];
            if (SessionUsersEmail != null && Session != null && SessionCompanyId != null)
            {
                var @param1 = new SqlParameter("@Email", SessionUsersEmail == null ? DBNull.Value : (object)SessionUsersEmail);
                var @param2 = new SqlParameter("@Password", SessionPassword == null ? DBNull.Value : (object)SessionPassword);
                var @param3 = new SqlParameter("@FormName", SessionRequestedForm_Name);
                var @param4 = new SqlParameter("@ImplementedCompanyId", SessionCompanyId == null ? DBNull.Value : (object)SessionCompanyId);
                var data = db.Database.SqlQuery<MenuUsersFormSetting_AccessControl_Result>("MenuUsersFormSetting_AccessControl @Email, @Password, @FormName,@ImplementedCompanyId", @param1, @param2, @param3, @param4).ToList();
                if (data.Count > 0)
                {
                    var Authroization = data.FirstOrDefault();
                    var LoggedInEmail = Authroization.Email.ToString();
                    var LoggedInPassword = Authroization.Password;
                    if (LoggedInEmail != null && LoggedInPassword != null)
                    #endregion
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Home/LoginPage");
                    }
                }
            }
            return RedirectToAction("Home/LoginPage");
        }
        public ActionResult PaymentHistory()
        {
            #region CALLING STORE PROCEDURE TO VERIFY THAT IS USER AUTHORIZED TIO VIEW FORM
            var SessionUsersEmail = Session["UsersEmail"];
            var SessionPassword = Session["UsersPassword"];
            var SessionRequestedForm_Name = "PaymentHistory";
            var SessionCompanyId = Session["CompanyId"];
            if (SessionUsersEmail != null && Session != null && SessionCompanyId != null)
            {
                var @param1 = new SqlParameter("@Email", SessionUsersEmail == null ? DBNull.Value : (object)SessionUsersEmail);
                var @param2 = new SqlParameter("@Password", SessionPassword == null ? DBNull.Value : (object)SessionPassword);
                var @param3 = new SqlParameter("@FormName", SessionRequestedForm_Name);
                var @param4 = new SqlParameter("@ImplementedCompanyId", SessionCompanyId == null ? DBNull.Value : (object)SessionCompanyId);
                var data = db.Database.SqlQuery<MenuUsersFormSetting_AccessControl_Result>("MenuUsersFormSetting_AccessControl @Email, @Password, @FormName,@ImplementedCompanyId", @param1, @param2, @param3, @param4).ToList();
                if (data.Count > 0)
                {
                    var Authroization = data.FirstOrDefault();
                    var LoggedInEmail = Authroization.Email.ToString();
                    var LoggedInPassword = Authroization.Password;
                    if (LoggedInEmail != null && LoggedInPassword != null)
                    #endregion
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Home/LoginPage");
                    }
                }
            }
            return RedirectToAction("Home/LoginPage");
        }

    }
}