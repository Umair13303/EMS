
var OperationType = "";
var SubOperationType = "";
var DB_OperationType = $('#HiddenFieldDB_OperationType').val();
var IsFieldClear = false;

$(document).ready(function () {


    switch (DB_OperationType) {

        case PARAMETER.DB_OperationType.INSERT:

            $('#DivButtonSubmitDown').show();
            $('#DivButtonUpdateDown').hide();
            break;

        case PARAMETER.DB_OperationType.UPDATE:
            GET_ENRSTUDENT_LISTBYPARAM();
            $('#DivButtonUpdateDown').show();
            $('#DivButtonSubmitDown').hide();

            break;
    }
    PopulateDropDownLists();
    ChangeCase();

});


function PopulateDropDownLists() {
    PopulateMT_GeneralBranch_ListByParam();
    PopulateLK_AdmissionCatagory_List();
    PopulateLK_Religion_List();
    PopulateLK_Country_List();
    PopulateLK_Occupation_List();

}

//-----------ALL CHANGE CASES
function ChangeCase() {
    $('#DropDownListCampus').change(function (event) {
        event.preventDefault();
        $('#DropDownListSession').val('-1').change();
        PopulateMT_AppSession_ListByParam();
        if (DB_OperationType == PARAMETER.DB_OperationType.UPDATE) {
            GET_ENRSTUDENT_LISTBYPARAM();
        }

    });
    $('#DropDownListSession').change(function (event) {
        event.preventDefault();
        $('#DropDownListClass').val('-1').change();
        PopulateMT_AppSessionDetail_ListByParam();
        PopulateMT_AppClass_ListByParam();
    });
    $('#DropDownListClass').change(function (event) {
        event.preventDefault();
        var ClassId = $('#DropDownListClass :selected').val();
        if (ClassId != null && ClassId != undefined && ClassId != "" && ClassId != "-1") {
            CHECK_FEESTRUCTURE_FOR_CLASS();
        }

    });


    //-----------FOR ::EDIT CASE
    $('#DropDownListStudent').change(function (event) {
        if (!IsFieldClear) {
            IsFieldClear = true;
            ClearInputFields();
            IsFieldClear = false;
        }
    });
}
//-----------ALL DROPDOWN LIST
function PopulateMT_GeneralBranch_ListByParam() {
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.BRANCH_BY_USER_ALLOWEDBRANCHIDS,
    }
    $.ajax({

        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_MT_GENERALBRANCH_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCampus").html(s);
            if (RoleId == Roles.RoleID_ADMIN || RoleId == Roles.RoleID_DEVELOPER) {
                $("#DropDownListCampus").prop('disabled', Status.Close);
            }
            else {
                $("#DropDownListCampus").prop('disabled', Status.Open);
            }
        },
        complete: function () {
            $("#DropDownListCampus").val(BranchId).change();

            stopLoading();
        },
    });
}
function PopulateMT_AppSession_ListByParam() {
    var CampusId = $('#DropDownListCampus :selected').val();
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.APPSESSION_BY_GENERALBRANCH,
        CampusId: CampusId,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_MT_APPSESSION_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option ' +
                    'data-ClassIds="' + data[i].ClassIds + '" ' +
                    'value="' + data[i].Id + '">' + data[i].Description +
                    '</option>';
            }
            $("#DropDownListSession").html(s);

        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMT_AppSessionDetail_ListByParam() {

    var SessionId = $('#DropDownListSession :selected').val();

    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.APPSESSIONDETAIL_BY_APPSESSION,
        SessionId: SessionId,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_MT_APPSESSIONDETAIL_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '">' + data[i].Description + '</option>';
            }
            $("#DropDownListRegisteredPeriod").html(s);
        },
        complete: function () {
            stopLoading();
        },

    });

};
function PopulateMT_AppClass_ListByParam() {

    var CampusId = $('#DropDownListCampus :selected').val();
    var ClassIds = $('#DropDownListSession :selected').attr('data-ClassIds');

    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.APPCLASS_BY_APPSESSION,
        CampusId: CampusId,
        ClassIds: ClassIds,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_MT_APPCLASS_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '">' + data[i].Description + '</option>';
            }
            $("#DropDownListClass").html(s);
        },
        complete: function () {
            stopLoading();
        },

    });

};
function PopulateLK_AdmissionCatagory_List() {
    var JsonArg = {
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_LK1_ADMISSIONCATAGORY",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListAdmissionCatagory").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLK_Religion_List() {
    var JsonArg = {
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_LK1_RELIGION",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListReligion").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLK_Country_List() {
    var JsonArg = {
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_LK1_COUNTRY",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCountry").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLK_Occupation_List() {
    var JsonArg = {
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_LK1_OCCUPATION",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListOccupation").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}




//-----------DB OPERATION CALL
function ValidateInputFields() {
    debugger
    if ($('#TextBoxFirstName').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxLastName').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxDateofBirth').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxCnicNo_FormBNo').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListGender').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListMartialStatus').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListReligion').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListNationality').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxResedenitalAddress').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxMobileNumber').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxEmailAddress').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxParentName').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxParentNICNo').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListParentStudyLevel').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListOccupation').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListRelationship').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxMonthlyIncome').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListClass').RequiredDropdown() == false) {
        return false;
    }
    return true;
}
$('#ButtonSubmitDown').click(function (event) {
    event.preventDefault();
  
    var IsValid = true;
    if (IsValid) {
        try {
            OperationType = PARAMETER.DB_OperationType.INSERT;
            SubOperationType = PARAMETER.DB_SubOperationType.ENRSTUDENT_NEWADMISSION_INSERT;
            UpSertDataIntoDB();
        }
        catch (err) {
            GetMessageBox(err, 500);

        }
    }
});
$('#ButtonUpdateDown').click(function (event) {
    event.preventDefault();
  
    var IsValid = true;
    if (IsValid) {
        try {
            OperationType = PARAMETER.DB_OperationType.UPDATE;
            SubOperationType = PARAMETER.DB_SubOperationType.ENRSTUDENT_NEWADMISSION_UPDATE;
            UpSertDataIntoDB();
        }
        catch (err) {
            GetMessageBox(err, 500);

        }
    }
});



function UpSertDataIntoDB() {
    var CampusId = $("#DropDownListCampus :selected").val();
    var SessionId = $("#DropDownListSession :selected").val();
    var RegisteredPeriodId = $("#DropDownListRegisteredPeriod :selected").val();
    var ClassId = $("#DropDownListClass :selected").val();
    var AdmissionCatagoryId = $("#DropDownListAdmissionCatagory :selected").val();
    var RegistrationNo = $("#TextBoxRegistrationNo").val();
    var StudentName = $("#TextBoxStudentName").val();
    var StudentCNIC = $("#TextBoxStudentCNIC").val();
    var BirthDate = $("#TextBoxBirthDate").val();
    var ReligionId = $("#DropDownListReligion :selected").val();
    var CountryId = $("#DropDownListCountry :selected").val();
    var DomicileDistrict = $("#TextBoxDomicileDistrict").val();
    var FatherName = $("#TextBoxFatherName").val();
    var FatherCNIC = $("#TextBoxFatherCNIC").val();
    var OccupationId = $("#DropDownListOccupation :selected").val();
    var IsFatherAlive = $("#CheckBoxIsFatherAlive").val();
    var GuardianName = $("#TextBoxGuardianName").val();
    var GuardianCNIC = $("#TextBoxGuardianCNIC").val();
    var StudentMobile = $("#TextBoxStudentMobile").val();
    var StudentEmail = $("#TextBoxStudentEmail").val();
    var ParentMobile = $("#TextBoxParentMobile").val();
    var LandLine = $("#TextBoxLandLine").val();
    var EmergencyMobile = $("#TextBoxEmergencyMobile").val();
    var Address = $("#TextBoxAddress").val();




    var EnrStudentGUID = $('#HiddenFieldEnrStudentGuID').val();

    var EnrStudentDetail = [];


    var JsonArg = {
        GuID: EnrStudentGUID,
        OperationType: OperationType,
        SubOperationType: SubOperationType,

        CampusId: CampusId,
        SessionId: SessionId,
        RegisteredPeriodId: RegisteredPeriodId,
        ClassId: ClassId,
        AdmissionCatagoryId: AdmissionCatagoryId,
        RegistrationNo: RegistrationNo,
        StudentName: StudentName,
        StudentCNIC: StudentCNIC,
        BirthDate: BirthDate,
        ReligionId: ReligionId,
        CountryId: CountryId,
        DomicileDistrict: DomicileDistrict,
        FatherName: FatherName,
        FatherCNIC: FatherCNIC,
        OccupationId: OccupationId,
        IsFatherAlive: IsFatherAlive,
        GuardianName: GuardianName,
        GuardianCNIC: GuardianCNIC,
        StudentMobile: StudentMobile,
        StudentEmail: StudentEmail,
        ParentMobile: ParentMobile,
        LandLine: LandLine,
        EmergencyMobile: EmergencyMobile,
        Address: Address,

    }
    EnrStudentDetail.push(JsonArg);


    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/UpSert_Into_EnrStudent",
        dataType: 'json',
        data: { 'PostedDataDetail': (EnrStudentDetail) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            GetMessageBox(data.Message, data.StatusCode);
        },
        complete: function () {
            stopLoading();
        },
        error: function (jqXHR, error, errorThrown) {
            GetMessageBox("The Transaction Can Not Be Performed Due To Serve Activity", 500);

        },
    });
}
function ClearInputFields() {
    $('.form-control').not('#DropDownListCampus,#DropDownListStudent').val('');
    $('.select2').not('#DropDownListCampus,#DropDownListStudent').val('-1').change();
    $('form').removeClass('Is-Valid');
}



//-----------DUPLICATE CHECK :: IF FEE STRUCTURE ALREADY EXIST FOR SELECTED CLASS
function CHECK_FEESTRUCTURE_FOR_CLASS() {
    var SessionId = $('#DropDownListSession :selected').val();
    var ClassId = $('#DropDownListClass :selected').val();
    var JsonArg = {
        SessionId: SessionId,
        ClassId: ClassId,
    }

    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/CHECK_FEESTRUCTURE_FOR_CLASS",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            if ((data.length < 0) || (data.length == 0) || (data == undefined)) {
                GetMessageBox("No Fee Found For Selected Class", 500);
                return;
            }
        },
        complete: function () {
            stopLoading();
        },
    });
}



//-----------LOAD ENTERY RECORD :: EDIT
$('#ButtonSubmitGetInfoForEdit').click(function () {
    startLoading();

    ClearInputFields();
    if ($('#DropDownListStudent').RequiredDropdown() == false) {
        return;
    }
    else {
        GET_ENRSTUDENT_DETAILBYID();
    }
});
function GET_ENRSTUDENT_LISTBYPARAM() {
    $('#DropDownListStudent').empty();
    $('#DropDownListStudent').select2({
        placeholder: 'Search By Student Name / Student CNIC / Student Code',
        minimumInputLength: 4,
        ajax: {
            url: BasePath + "/AStudent/MEnrollmentUI/GET_MT_ENRSTUDENT_BYPARAMETER",
            type: "POST",
            delay: 250,
            data: function (params) {
                return {
                    PostedData: { // Wrap in `PostedData`
                        SearchParameter: params.term,
                        DB_IF_PARAM: PARAMETER.DB_IF_Condition.ENRSTUDENT_BY_GENERALBRANCH_SEARCH_PARAMETER,
                        CampusId: $('#DropDownListCampus :selected').val() ?? BranchId
                    }
                };
            },
            beforeSend: function () {
                startLoading();
            },
            processResults: function (data) {
                return {
                    results: data.data.map(function (item) {
                        return {
                            id: item.GuID,
                            text: item.Description,
                            ClassDecor: item.Description,
                        };
                    })
                };
            },
            complete: function () {
                stopLoading();
            },
        },
    });
}
function GET_ENRSTUDENT_DETAILBYID() {
    var CampusId = $('#DropDownListCampus :selected').val();
    var StudentId = $('#DropDownListStudent :selected').val();
    if (StudentId != null && StudentId != undefined && StudentId != "" && StudentId != "-1") {

        var JsonArg = {
            GuID: StudentId,
            CampusId: CampusId,
        }
        $.ajax({
            type: "POST",
            url: BasePath + "/AStudent/MEnrollmentUI/GET_MT_ENRSTUDENT_DETAILBYID",
            dataType: 'json',
            data: { 'PostedData': (JsonArg) },
            beforeSend: function () {
                startLoading();
            },
            success: function (data) {
                setTimeout(function () {
                    $('#DropDownListSession').val(data[0].SessionId).change().prop('disabled', true);
                }, 3000);
                setTimeout(function () {
                    $('#DropDownListRegisteredPeriod').val(data[0].RegisteredPeriodId).change().prop('disabled', true);
                }, 4000);
                setTimeout(function () {
                    $('#DropDownListClass').val(data[0].ClassId).change().prop('disabled', true);
                }, 5000);
                $("#DropDownListAdmissionCatagory").val(data[0].AdmissionCatagoryId).change();
                $("#TextBoxRegistrationNo").val(data[0].RegistrationNo);
                $("#TextBoxStudentName").val(data[0].StudentName);
                $("#TextBoxStudentCNIC").val(data[0].StudentCNIC);
                $("#TextBoxBirthDate").val(data[0].BirthDate);
                $("#DropDownListReligion").val(data[0].ReligionId).change();
                $("#DropDownListCountry").val(data[0].CountryId).change();
                $("#TextBoxDomicileDistrict").val(data[0].DomicileDistrict);
                $("#TextBoxFatherName").val(data[0].FatherName);
                $("#TextBoxFatherCNIC").val(data[0].FatherCNIC);
                $("#DropDownListOccupation").val(data[0].OccupationId).change();
                $("#CheckBoxIsFatherAlive").val(data[0].IsFatherAlive);
                $("#TextBoxGuardianName").val(data[0].GuardianName);
                $("#TextBoxGuardianCNIC").val(data[0].GuardianCNIC);
                $("#TextBoxStudentMobile").val(data[0].StudentMobile);
                $("#TextBoxStudentEmail").val(data[0].StudentEmail);
                $("#TextBoxParentMobile").val(data[0].ParentMobile);
                $("#TextBoxLandLine").val(data[0].LandLine);
                $("#TextBoxEmergencyMobile").val(data[0].EmergencyMobile);
                $("#TextBoxAddress").val(data[0].Address);
                $('#HiddenFieldEnrStudentGuID').val(data[0].GuID);
            },
            complete: function () {
                stopLoading();
            },

        });
    }


}