using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Areas.PatientModuleArea.Controllers
{
    public class SaveDataIntoTableController : Controller
    {

        HMSEntities db = new HMSEntities();
        // GET: PatientsModule/SaveDataIntoTable
        [HttpPost]
        public ActionResult SaveNewPatient(Patients PatientsDb)
        {
            #region FUNCTION VARIABLE
            var HASH_Guid = Guid.NewGuid();
            var UserPassword = Guid.NewGuid().ToString("N").ToLower().Replace("1", "").Replace("o", "").Replace("0", "").Substring(0, 6);
            var SessionUsersId = Session["UsersId"];
            #endregion
            using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction()) //transaction
            {
                try
                {
                    #region PASSING DATA IN PARAMETERS
                    var Code = new SqlParameter("@Code", "PRC" + '-' + DateTime.Now.ToShortDateString().Replace("/", "").Replace("0", ""));
                    var Name = new SqlParameter("@Name",PatientsDb.Name == null ? DBNull.Value : (object)PatientsDb.Name);
                    var FatherHusbandName = new SqlParameter("@FatherHusbandName", PatientsDb.FatherHusbandName == null ? DBNull.Value : (object)PatientsDb.FatherHusbandName);
                    var Cnic = new SqlParameter("@Cnic", PatientsDb.Cnic == null ? DBNull.Value : (object)PatientsDb.Cnic);
                    var Gender = new SqlParameter("@Gender", PatientsDb.Gender == null ? DBNull.Value : (object)PatientsDb.Gender);
                    var Phone = new SqlParameter("@Phone", PatientsDb.Phone == null ? DBNull.Value : (object)PatientsDb.Phone);
                    var DOB = new SqlParameter("@DOB", PatientsDb.DOB == null ? DBNull.Value : (object)PatientsDb.DOB);
                    var Address = new SqlParameter("@Address", PatientsDb.Address == null ? DBNull.Value : (object)PatientsDb.Address);
                    var Age = new SqlParameter("@Age", PatientsDb.Age);
                    var Email = new SqlParameter("@Email", PatientsDb.Email == null ? DBNull.Value : (object)PatientsDb.Email);
                    var UserName = new SqlParameter("@UserName", PatientsDb.UserName == null ? DBNull.Value : (object)PatientsDb.UserName);
                    var Password = new SqlParameter("@Password", UserPassword);
                    var CityId = new SqlParameter("@CityId", PatientsDb.CityId == null ? DBNull.Value : (object)PatientsDb.CityId);
                    var CountryId = new SqlParameter("@CountryId", PatientsDb.CountryId == null ? DBNull.Value : (object)PatientsDb.CountryId);
                    var CreatedDate = new SqlParameter("@CreatedDate", DateTime.Now);
                    var CreatedById = new SqlParameter("@CreatedById", SessionUsersId);
                    var IsActive = new SqlParameter("@IsActive", PatientsDb.IsActive);
                    var DiscontinuationDate = new SqlParameter("@DiscontinuationDate", PatientsDb.DiscontinuationDate == null ? DBNull.Value : (object)PatientsDb.DiscontinuationDate);
                    var ReasonToInActive = new SqlParameter("@ReasonToInActive", PatientsDb.ReasonToInActive == null ? DBNull.Value : (object)PatientsDb.ReasonToInActive);
                    var CompanyId = new SqlParameter("@CompanyId", Session["CompanyId"]);
                    var HASHGuid = new SqlParameter("@HASHGuid", HASH_Guid);
                    #endregion
                    #region SAVE DATA
                    var Event_Parameters_List = new List<SqlParameter>();
                    Event_Parameters_List.Add(Code);
                    Event_Parameters_List.Add(Name);
                    Event_Parameters_List.Add(FatherHusbandName);
                    Event_Parameters_List.Add(Cnic);
                    Event_Parameters_List.Add(Gender);
                    Event_Parameters_List.Add(Phone);
                    Event_Parameters_List.Add(DOB);
                    Event_Parameters_List.Add(Address);
                    Event_Parameters_List.Add(Age);
                    Event_Parameters_List.Add(Email);
                    Event_Parameters_List.Add(UserName);
                    Event_Parameters_List.Add(Password);
                    Event_Parameters_List.Add(CityId);
                    Event_Parameters_List.Add(CountryId);
                    Event_Parameters_List.Add(CreatedDate);
                    Event_Parameters_List.Add(CreatedById);
                    Event_Parameters_List.Add(IsActive);
                    Event_Parameters_List.Add(DiscontinuationDate);
                    Event_Parameters_List.Add(ReasonToInActive);
                    Event_Parameters_List.Add(CompanyId);
                    Event_Parameters_List.Add(HASHGuid);

                    var Event_Parameters_Array = Event_Parameters_List.ToArray();
                    var Eevnt_Data = db.Database.ExecuteSqlCommand("Patients_Insert @Code,@Name,@FatherHusbandName,@Cnic,@Gender,@Phone,@DOB,@Address,@Age,@Email,@UserName,@Password,@CityId,@CountryId,@CreatedDate,@CreatedById,@IsActive, @DiscontinuationDate, @ReasonToInActive,@CompanyId,@HASHGuid", Event_Parameters_Array);
                    #endregion
                    dbTran.Commit();
                    string message = "SUCCESS";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }

                catch (Exception ex)
                {
                    dbTran.Rollback();
                    throw ex;
                }
            }
        }

        public ActionResult SaveNewPatientAppointment(Models.EDMX.PatientAppointment PatientAppointment)
        {
            using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction()) //transaction
            {
                try
                {
                    #region PASSING DATA IN PARAMETERS
                    var CompanyId = Convert.ToInt32(Session["UsersCompanyId"]);
                    var BranchId = Convert.ToInt32(Session["UsersBranchId"]);
                    var UsersId = Convert.ToInt32(Session["UsersId"]);



                    var AppointmentCode = "PAC" + '-' + DateTime.Now.ToShortDateString().Replace("/", "").Replace("0", "");
                    var Appointment = db.PatientAppointment_Insert(
                                                                             AppointmentCode.ToString()
                                                                            ,PatientAppointment.PatientCode.ToString()
                                                                            ,(int?)PatientAppointment.DepartmentId
                                                                            ,PatientAppointment.DoctorCode.ToString()
                                                                            , PatientAppointment.DateofAppointment
                                                                            , PatientAppointment.TimeofAppointment.ToString()
                                                                            ,PatientAppointment.PatientEmail.ToString()
                                                                            ,PatientAppointment.PatientPhone.ToString()
                                                                            ,PatientAppointment.Message.ToString()
                                                                            ,PatientAppointment.IsAttended
                                                                            ,CompanyId
                                                                            ,BranchId
                                                                            ,UsersId
                                                                            ,PatientAppointment.Type.ToString()
                                                                            ,Guid.NewGuid().ToString()
                                                                            );
                    
                    #endregion
                    dbTran.Commit();
                    string message = "SUCCESS";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    throw ex;
                }
            }
        }

        public ActionResult SavePrescriptionReportAgainstAppointment(string AppointmentStatus, MedicalPrescriptions MedicalPrescriptionsParent,List<MedicalPrescriptionDetails> MedicalPrescriptionDetails)
        {
            bool Status = false;

            var CompanyId = Convert.ToInt32(Session["UsersCompanyId"]);
            var BranchId = Convert.ToInt32(Session["UsersBranchId"]);
            var UsersId = Convert.ToInt32(Session["UsersId"]);
            var PrescriptionCode = "SGMS" + '-' + DateTime.Now.ToShortDateString().Replace("/", "").Replace("0", "");
            using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction()) //transaction
            {
                try
                {
                    var PrescriptionParent = db.MedicalPrescriptions_Insert(
                                                                             CompanyId
                                                                            , PrescriptionCode
                                                                            , MedicalPrescriptionsParent.PatientCode
                                                                            , MedicalPrescriptionsParent.AppointmentCode
                                                                            , MedicalPrescriptionsParent.AdditionalNote
                                                                            , UsersId
                                                                            , DateTime.Now
                                                                            , Guid.NewGuid().ToString()
                                                                            );
                                                                            var PrescriptId = PrescriptionParent.FirstOrDefault();
                    foreach (var Content in MedicalPrescriptionDetails)
                    {
                        db.MedicalPrescriptionsDetails_Insert(
                                                                   (int?)PrescriptId
                                                                 , Content.PrescriptionType
                                                                 , Content.Test_MedicineName
                                                                 , Content.CourseDays
                                                                 , Content.Dosage
                                                                 , CompanyId
                                                                 );
                    }
                    if (AppointmentStatus == "Attended")
                    {
                        Status = true;
                    }
                    db.PatientAppointment_UpdateStatus( 
                                                                CompanyId
                                                           ,    DateTime.Now
                                                           ,    UsersId
                                                           ,    Status
                                                           ,    MedicalPrescriptionsParent.AppointmentCode,
                                                                MedicalPrescriptionsParent.PatientCode
                                                      );
                    dbTran.Commit();
                    string message = "SUCCESS";
                    return Json(new { Message = message, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    throw ex;
                }
            }

        }

    }
}