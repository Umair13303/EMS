/*----------------------------------** GLOBAL VARIABLE FOR PAGE :: CREATE/UPDATE BM_BRANCH **-----------------------------------------------*/
var OperationType = "";
var DDL_Condition = "";
var DB_OperationType = $('#HiddenFieldDB_OperationType').val();
var IsFieldClear = false;


var StudyLevelIds = "";
var StudyGroupIds = "";
    

/*----------------------------------** FUNCTION FOR::PAGE LOADER **------------------------------------------------------------------------------*/
$(document).ready(function () {
    DB_OperationType = $('#HiddenFieldDB_OperationType').val();
    switch (DB_OperationType) {
        case DBOperation.INSERT:
            $('#DivButtonSubmitDown').show();
            $('#DivButtonUpdateDown').hide();
            break;
        case DBOperation.UPDATE:
            GET_BM_BRANCH_LISTBYPARAM();
            $('#DivButtonSubmitDown').hide();
            $('#DivButtonUpdateDown').show();
            break;
    }
    PopulateDropDownLists();
    ChangeCase();

});

/*----------------------------------** FUNCTION FOR::CHANGE CASE LOADER **-----------------------------------------------------------------------*/
function ChangeCase() {

    $("#DropDownListStudyLevels").attr('data-width', '100%').select2({
        placeholder: 'Select an Option',
        multiple: true,
    }).change(function (event) {
        if (event.target == this) {
            StudyLevelIds = $(this).val();
        }
    });
    $("#DropDownListStudyGroups").attr('data-width', '100%').select2({
        placeholder: 'Select an Option',
        multiple: true,
    }).change(function (event) {
        if (event.target == this) {
            StudyGroupIds = $(this).val();
        }
    });

    //-----------FOR ::EDIT CASE

    $('#DropDownListCampusSetting').change(function () {
        if (!IsFieldClear) {
            IsFieldClear = true;
            ClearInputFields();
            IsFieldClear = false;
        }
    });
}

function PopulateDropDownLists() {
    PopulateMT_BM_Branch_ListByParam();
    PopulateLK_PolicyPeriod_List();
    PopulateLK_RollCallSystem_List();
    PopulateLK_BillingMethod_List();
    PopulateLK_ChallanMethod_List();
    PopulateLK_StudyLevel_List();
    PopulateLK_StudyGroup_List();
}

/*----------------------------------** FUNCTION FOR:: RENDER DROP DOWN FROM DB_MAIN-- STORED PROCEDURE **----------------------------------------------*/

function PopulateMT_BM_Branch_ListByParam() {
    switch (DB_OperationType) {
        case DBOperation.INSERT:
            DDL_Condition = MDB_LIST_CONDITION.BM_BRANCH_BY_ALLOWEDBRANCHIDS_FORNEWINSERT;
            break;
        case DBOperation.UPDATE:
            DDL_Condition = MDB_LIST_CONDITION.BM_BRANCH_BY_ALLOWEDBRANCHIDS_FORUPDATERECORD;
            break;
    }
    var JsonArg = {
        DB_IF_PARAM: DDL_Condition,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ABranch/CBranchSettingManagmentUI/GET_MT_BM_BRANCH_BYPARAMTER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '</option>';
            }
            $("#DropDownListCampus").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });

}


/*----------------------------------** FUNCTION FOR:: RENDER DROP DOWN FROM DB_LOOKUP-- LINQUERY **----------------------------------------------*/
function PopulateLK_PolicyPeriod_List() {
    $.ajax({
        type: "POST",
        url: BasePath + "/ABranch/CBranchSettingManagmentUI/GET_LK1_POLICYPERIOD",
        data: { },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListPolicyPeriod").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLK_RollCallSystem_List() {
    $.ajax({
        type: "POST",
        url: BasePath + "/ABranch/CBranchSettingManagmentUI/GET_LK1_ROLLCALLSYSTEM",
        data: { },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListRollCallSystem").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLK_BillingMethod_List() {
    $.ajax({
        type: "POST",
        url: BasePath + "/ABranch/CBranchSettingManagmentUI/GET_LK1_BILLINGMETHOD",
        data: { },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListBillingMethod").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}


/*----------------------------------** FUNCTION FOR:: RENDER DROP DOWN FROM DB_LOOKUP-- STORED PROCEDURE ** --------------------------------------*/
function PopulateLK_ChallanMethod_List() {
    var JsonArg = {
        DB_IF_PARAM: LK_LIST_CONDITION.CHALLANMETHOD_LIST,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ABranch/CBranchSettingManagmentUI/GET_LK1_CHALLANMETHOD_BYPARAMTER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListChallanMethod").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLK_StudyLevel_List() {
    var JsonArg = {
        DB_IF_PARAM: LK_LIST_CONDITION.STUDYLEVEL_LIST,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ABranch/CBranchSettingManagmentUI/GET_LK1_STUDYLEVEL_BYPARAMTER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListStudyLevels").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLK_StudyGroup_List() {

    var JsonArg = {
        DB_IF_PARAM: LK_LIST_CONDITION.STUDYGROUP_LIST,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ABranch/CBranchSettingManagmentUI/GET_LK1_STUDYGROUP_BYPARAMTER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListStudyGroups").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}



/*----------------------------------** FUNCTION FOR:: DATABASE OPERATION (VALIDATE,UPSERT,CLEAR) **----------------------------------------------*/
function ValidateInputFields() {
    if ($('#DropDownListCampus').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListPolicyPeriod').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListChallanMethod').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListRollCallSystem').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListBillingMethod').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListStudyLevels').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListStudyGroups').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxRemarks').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    return true;
}
$('#ButtonSubmitDown').click(function (event) {
    event.preventDefault();
    var IS_VALID = ValidateInputFields();
    if (IS_VALID) {
        try {
            OperationType = DBOperation.INSERT;
            UpSertDataIntoDB();
        }
        catch {
            GetMessageBox(err, 505);
        }
    }
});
$('#ButtonUpdateDown').click(function (event) {
    event.preventDefault();
    var IS_VALID = ValidateInputFields();
    if (IS_VALID) {
        try {
            OperationType = DBOperation.UPDATE;
            UpSertDataIntoDB();
        }
        catch {
            GetMessageBox(err, 505);
        }
    }
});
function UpSertDataIntoDB() {

    var CampusId = $('#DropDownListCampus :selected').val();
    var PolicyPeriodId = $('#DropDownListPolicyPeriod :selected').val();
    var ChallanMethodId = $('#DropDownListChallanMethod :selected').val();
    var RollCallSystemId = $('#DropDownListRollCallSystem :selected').val();
    var BillingMethodId = $('#DropDownListBillingMethod :selected').val();
    var Remarks = $('#TextBoxRemarks').val();

    var CampusSettingGuID = $('#HiddenFieldCampusSettingGuID').val();
    var JsonArg = {
        GuID: CampusSettingGuID,
        OperationType: OperationType,
        CampusId: CampusId,
        PolicyPeriodId: PolicyPeriodId,
        ChallanMethodId: ChallanMethodId,
        RollCallSystemId: RollCallSystemId,
        BillingMethodId: BillingMethodId,
        StudyLevelIds: StudyLevelIds.toString(),
        StudyGroupIds: StudyGroupIds.toString(),
        Remarks: Remarks,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ABranch/CBranchSettingManagmentUI/UpSert_Into_BM_BranchSetting",
        dataType: 'json',
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            GetMessageBox(data.Message, data.StatusCode);
        },
        complete: function () {
            stopLoading();
            ClearInputFields();
        },
        error: function (jqXHR, error, errorThrown) {
            GetMessageBox("The Transaction Can Not Be Performed Due To Serve Activity", 500);
        },
    });

}
function ClearInputFields() {
    //-----------NOT CLEARING REQUIRED FIELD
    $('.form-control').not('#DropDownListCampusSetting').val('');
    $('.select2').not('#DropDownListCampusSetting').val('-1').change();
    $('form').removeClass('Is-Valid');
}
