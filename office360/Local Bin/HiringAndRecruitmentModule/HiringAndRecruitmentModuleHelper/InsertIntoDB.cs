using Microsoft.Reporting.WebForms;
using office360.Models.EDMX;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using office360.Models.General;

namespace office360.Areas.HiringAndRecruitmentModule.HiringAndRecruitmentModuleHelper
{
    public class InsertIntoDB
    {
        public static string InsertIntoHRMAdvertisment(HRAdvertisments HRAdvertisment,string DocumentCode, out int? StatusCode)
        {
            string HASH_Guid = Guid.NewGuid().ToString();
            var ShortGUID = Guid.NewGuid().ToString("N").ToLower().Replace("1", "").Replace("o", "").Replace("0", "").Substring(0, 6);
            using (var db = new HMSEntities()) // Create a new instance of the context
            using (var dbTran = db.Database.BeginTransaction()) // Transaction
            {
                try
                {
                    #region VARIABLES
                    var Type = HRAdvertisment.Type;
                    var Title = HRAdvertisment.Title;
                    var StartDate = HRAdvertisment.StartDate;
                    var EndDate = HRAdvertisment.EndDate;
                    var Description = HRAdvertisment.Description;
                    #endregion
                    #region ADD ADVERTISMENT
                    db.HRAdvertisment_Insert(
                        DocumentCode
                        ,Type
                        ,StartDate
                        ,EndDate
                        ,Description
                        ,Utility.ShortGUID
                        ,Utility.HashGUID.ToString()
                        ,Sessions.UsersId
                        ,DateTime.Now
                        ,Sessions.CompanyId
                        ,(int?)Status.Open
                        ,Title

                    );
                    #endregion
                    dbTran.Commit();
                    StatusCode = 200;
                    return "SUCCESS";
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    StatusCode = 500;
                    throw ex;
                }
            }
        }

        public static string InsertIntoHRApplicants(HRApplicants HRApplicants,List<HREducationDetails> HREducationDetails, List<HREmploymentHistory> HREmploymentHistory, string DocumentCode, out int? StatusCode)
        {

            string HASH_Guid = Guid.NewGuid().ToString();
            var ShortGUID = Guid.NewGuid().ToString("N").ToLower().Replace("1", "").Replace("o", "").Replace("0", "").Substring(0, 6);
            var Time = DateTime.Now.ToLongTimeString();
            using (var db = new HMSEntities()) // Create a new instance of the context
            using (var dbTran = db.Database.BeginTransaction()) // Transaction
            {
                try
                {
                    #region CHECK IF INTERVIEW HAS ALREADY BEEN SCHEDULED OF A CANDIDATE AGAINST JOB CODE
                        var data = db.HRApplicants_DataCount(
                                                            Sessions.CompanyId
                                                          , HRApplicants.AdvertismentCode
                                                          , HRApplicants.Phone
                                                          , HRApplicants.Email
                                                            );
                        var IsAlreadyExist = data.FirstOrDefault();
                    #endregion
                    #region
                    string AdvertismentCode = HRApplicants.AdvertismentCode;
                    string ApplicantCode = HRApplicants.ApplicantCode;
                    string Name = HRApplicants.Name;
                    string Address = HRApplicants.Address;
                    string Phone = HRApplicants.Phone;
                    string Email = HRApplicants.Email;
                    #endregion
                    if (IsAlreadyExist == 0)
                    {
                        #region MAIN INSERT
                        db.HRApplicants_Insert(
                                                
                                                 AdvertismentCode
                                                ,ApplicantCode
                                                ,Name
                                                ,Address
                                                ,Phone
                                                ,Email
                                                ,ShortGUID
                                                ,HASH_Guid
                                                , Sessions.CompanyId
                                                ,(int?)Status.Open
                                                ,DateTime.Now
                            );
                        #endregion
                        #region EDUCATIONAL DETAILS 
                        foreach (var Detail in HREducationDetails)
                        {
                            var Degree = Detail.Degree;
                            var CommencmentDate = Detail.CommencmentDate;
                            var EndDate = Detail.EndDate;
                            var GPA_Percentage = Detail.GPA_Percentage;
                            db.HREducationDetails_Insert(
                                                    AdvertismentCode
                                                  , ApplicantCode
                                                  , Degree
                                                  , CommencmentDate
                                                  , EndDate
                                                  , GPA_Percentage
                                                  , (int?)Status.Open
                                                   );


                        }
                        #endregion
                        #region EMPLOYMENT DETAILS 
                        foreach(var Details in HREmploymentHistory)
                        {
                            var Employeer = Details.Employeer;
                            var Position = Details.Position;
                            var StartDate = Details.StartDate;
                            var EndDate = Details.EndDate;
                            db.HREmploymentHistory_Insert(
                                                    AdvertismentCode
                                                   ,ApplicantCode
                                                   ,Employeer
                                                   ,Position
                                                   ,StartDate
                                                   ,EndDate
                                                   ,(int?)Status.Open
                                                    );
                        }


                        #endregion
                        dbTran.Commit();
                        StatusCode = 200;
                        return "SUCCESS";
                    }
                    else
                    {
                        StatusCode = 305;
                        return "ALREADY EXIST";
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    StatusCode = 500;
                    throw ex;
                }
            }
        }

        public static string InsertIntoHRInterviews(HRInterviews HRInterviews, string DocumentCode, out int? StatusCode)
        {
            string HASH_Guid = Guid.NewGuid().ToString();
            var ShortGUID = Guid.NewGuid().ToString("N").ToLower().Replace("1", "").Replace("o", "").Replace("0", "").Substring(0, 6);

            using (var db = new HMSEntities()) // Create a new instance of the context
            using (var dbTran = db.Database.BeginTransaction()) // Transaction
            {
                     #region CHECK IF INTERVIEW HAS ALREADY BEEN SCHEDULED OF A CANDIDATE AGAINST JOB CODE
                try
                {
                    var data = db.HRInterView_DataCount(
                                                        Sessions.CompanyId
                                                      , HRInterviews.AdvertismentCode
                                                      , HRInterviews.ApplicantCode
                                                        );
                    var IsAlreadyExist = data.FirstOrDefault();
                #endregion
                    if (IsAlreadyExist ==0)
                    {
                        #region VARIABLES
                        var ApplicantCode = HRInterviews.ApplicantCode;
                        var AdvertismentCode = HRInterviews.AdvertismentCode;
                        var Date = HRInterviews.InterviewDate;
                        var Time = HRInterviews.InterViewTime;
                        var Message = "Your InterView Has Been Scheduled On " + HRInterviews.InterviewDate + "at " + HRInterviews.InterViewTime;

                        #endregion

                        #region INSERT IF DOES NOT EXIST

                        db.HRInterviews_Insert(
                            DocumentCode
                           ,ApplicantCode
                           ,AdvertismentCode
                           ,Date
                           ,Time
                           ,(int?)Status.Open
                           , Sessions.CompanyId
                           ,Utility.ShortGUID
                           ,Utility.HashGUID.ToString()
                           , Sessions.UsersId
                           ,DateTime.Now
                           ,Message
                           
                        );

                        db.HRApplicants_UpdateByParamter(
                            Sessions.CompanyId,
                            HRInterviews.ApplicantCode,
                            HRInterviews.AdvertismentCode,
                            2// for processed
                        );
                        dbTran.Commit();
                        StatusCode = 200;
                        return "SUCCESS";
                        #endregion

                    }
                    else
                    {
                        StatusCode = 305;
                        return "ALEADY EXIST";
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    StatusCode = 500;
                    throw ex;
                }
            }
        }

        public static string ApproveOrRejectHRMApplicantInterviews(List<HRInterviews> HRInterviews,  out int? StatusCode)
            {
            string HASH_Guid = Guid.NewGuid().ToString();
            var ShortGUID = Guid.NewGuid().ToString("N").ToLower().Replace("1", "").Replace("o", "").Replace("0", "").Substring(0, 6);
            var Time = DateTime.Now.ToLongTimeString();
            using (var db = new HMSEntities()) // Create a new instance of the context
            using (var dbTran = db.Database.BeginTransaction()) // Transaction
            {
                try
                {
                  foreach(var Applicant in HRInterviews)
                    {
                        db.HRApplicants_UpdateByParamter(
                        Sessions.CompanyId,
                        Applicant.ApplicantCode,
                        Applicant.AdvertismentCode,
                        Applicant.Status
                    );
                        db.HRInterviews_UpdateByParameter(
                                                           Sessions.CompanyId
                                                          ,Applicant.ApplicantCode
                                                          ,Applicant.AdvertismentCode
                                                          ,Applicant.Code
                                                          ,Applicant.Status
                                                          );


                    }

                    dbTran.Commit();
                    StatusCode = 200;
                    return "SUCCESS";
                }
                catch (Exception ex)
                {
                    StatusCode = 500;

                    dbTran.Rollback();
                    throw ex;
                }
            }
        }
    }
}
