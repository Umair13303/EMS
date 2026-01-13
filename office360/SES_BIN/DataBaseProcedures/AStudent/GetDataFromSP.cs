using office360.Common.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.HttpStatus;
using static office360.Models.General.DBListCondition;


namespace office360.Common.DataBaseProcedures.AStudent
{
    public class GetDataFromSP
    {

        #region LIST BY SEARCH PARAMETER
        public static List<EnrStudent_GetListByParam_Result> GET_MT_ENRSTUDENT_BYPARAM(_SqlParameters PostedData)
        {
            List<EnrStudent_GetListByParam_Result> DATA = new List<EnrStudent_GetListByParam_Result>();
            using (var db = new SESEntities())
            {
                DATA = db.EnrStudent_GetListByParam(
                                                            PostedData.DB_IF_PARAM,
                                                            Session_Manager.CompanyId,
                                                            PostedData.CampusId,
                                                            PostedData.SessionId,
                                                            PostedData.ClassId,
                                                            PostedData.SearchParameter
                                                          ).ToList();
            }
            return DATA;
        }
        #endregion

        #region HELPER FOR :: GET DETAIL FOR ID USING LINQ QUERY
        public static List<_SqlParameters> GET_MT_ENRSTUDENT_INFO_BY_GUID(_SqlParameters PostedData)
        {
            List<_SqlParameters> List = new List<_SqlParameters>();
            using (var db = new SESEntities())
            {
                List =
                       ((List<_SqlParameters>)
                       (from ES in db.EnrStudent
                        where
                            ES.CompanyId == Session_Manager.CompanyId && ES.CampusId == PostedData.CampusId && ES.GuID == PostedData.GuID
                        select new _SqlParameters
                        {
                            Id = ES.Id,
                            GuID = ES.GuID,
                            Code = ES.Code,
                            CampusId = ES.CampusId,
                            SessionId = ES.SessionId,
                            RegisteredPeriodId = ES.RegisteredPeriodId,
                            ClassId = ES.ClassId,
                            AdmissionCatagoryId = ES.AdmissionCatagoryId,
                            RegistrationNo = ES.RegistrationNo,
                            StudentName = ES.StudentName,
                            StudentCNIC = ES.StudentCNIC,
                            BirthDate = ES.BirthDate.ToString(),
                            ReligionId = ES.ReligionId,
                            CountryId = ES.CountryId,
                            DomicileDistrict = ES.DomicileDistrict,
                            FatherName = ES.FatherName,
                            FatherCNIC = ES.FatherCNIC,
                            OccupationId = ES.OccupationId,
                            IsFatherAlive = ES.IsFatherAlive,
                            GuardianName = ES.GuardianName,
                            GuardianCNIC = ES.GuardianCNIC,
                            StudentMobile = ES.StudentMobile,
                            StudentEmail = ES.StudentEmail,
                            ParentMobile = ES.ParentMobile,
                            LandLine = ES.LandLine,
                            EmergencyMobile = ES.EmergencyMobile,
                            Address = ES.Address,
                        }).ToList());
            }
            return List;
        }
        #endregion

        #region HELPER FOR :: DATA TABLE LIST
        public static List<EnrStudent_GetListBySearch_Result> GET_MT_ENRSTUDENT_LIST_SEARCHPARAM(_SqlParameters PostedData)
        {
            List<EnrStudent_GetListBySearch_Result> DATA = new List<EnrStudent_GetListBySearch_Result>();
            using (var db = new SESEntities())
            {
                DATA = db.EnrStudent_GetListBySearch(
                                                            Session_Manager.CompanyId,
                                                            PostedData.CampusId,
                                                            PostedData.SearchById,
                                                            PostedData.InputText
                                                          ).ToList();
            }
            return DATA;
        }
        #endregion

    }
}