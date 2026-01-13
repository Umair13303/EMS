/*----------------------------------** GLOBAL VARIABLE FOR PAGE :: CREATE/UPDATE APP CLASS **-----------------------------------------------*/
var OperationType = "";
var DDL_Condition = "";
var DB_OperationType = $('#HiddenFieldDB_OperationType').val();
var IsFieldClear = false;

/*----------------------------------** FUNCTION FOR::PAGE LOADER **------------------------------------------------------------------------------*/
$(document).ready(function () {
    DB_OperationType = $('#HiddenFieldDB_OperationType').val();
    switch (DB_OperationType) {
        case DBOperation.INSERT:
            $('#DivButtonSubmitDown').show();
            $('#DivButtonUpdateDown').hide();
            break;
        case DBOperation.UPDATE:
            GET_APPCLASS_LISTBYPARAM();
            $('#DivButtonSubmitDown').hide();
            $('#DivButtonUpdateDown').show();
            break;
    }
    PopulateDropDownLists();
    ChangeCase();

});

function PopulateDropDownLists() {
    PopulateMT_GeneralBranch_ListByParam();
}

/*----------------------------------** FUNCTION FOR::CHANGE CASE LOADER **-----------------------------------------------------------------------*/
function ChangeCase() {

    $('#DropDownListCampus').change(function () {
        PopulateLK_StudyGroup_List();
        PopulateLK_StudyLevel_List();
    });
    //-----------FOR ::EDIT CASE
    $('#DropDownListClass').change(function () {
        if (!IsFieldClear) {
            IsFieldClear = true;
            ClearInputFields();
            IsFieldClear = false;
        }
    });
}

/*----------------------------------** FUNCTION FOR:: RENDER DROP DOWN FROM DB_MAIN-- STORED PROCEDURE **--------------------------------------*/
function PopulateMT_GeneralBranch_ListByParam() {
    switch (DB_OperationType) {
        case DBOperation.INSERT:
            DDL_Condition = MDB_LIST_CONDITION.GENERALBRANCH_BY_USER_ALLOWEDBRANCHIDS_FORNEWINSERT;
            break;
        case DBOperation.UPDATE:
            DDL_Condition = MDB_LIST_CONDITION.GENERALBRANCH_BY_USER_ALLOWEDBRANCHIDS_FORUPDATERECORD;
            break;
    }
    var JsonArg = {
        DB_IF_PARAM: DDL_Condition,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MClassUI/GET_MT_GENERALBRANCH_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCampus").html(s);
            if (RoleId == Roles.Admin || RoleId == Roles.Developer) {
                $("#DropDownListCampus").prop('disabled', Status.Close);
            }
            else {
                $("#DropDownListCampus").prop('disabled', Status.Open);
            }
        },
        complete: function () {
            $('#DropDownListCampus').val(BranchId).change();
            stopLoading();
        },
    });
}


/*----------------------------------** FUNCTION FOR:: RENDER DROP DOWN FROM DB_LOOKUP-- STORED PROCEDURE **--------------------------------------*/
function PopulateLK_StudyGroup_List() {
    var CampusId = $('#DropDownListCampus :selected').val();

    var JsonArg = {
        CampusId: CampusId,
        DB_IF_PARAM: LK_LIST_CONDITION.STUDYGROUP_BY_BRANCHSETTING,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MClassUI/GET_LK1_STUDYGROUP_BYPARAMTER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListStudyGroup").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLK_StudyLevel_List() {
    var CampusId = $('#DropDownListCampus :selected').val();
    var JsonArg = {
        CampusId: CampusId,
        DB_IF_PARAM: LK_LIST_CONDITION.STUDYLEVEL_BY_BRANCHSETTING,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MClassUI/GET_LK1_STUDYLEVEL_BYPARAMTER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListStudyLevel").html(s);
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
    if ($('#TextBoxDescription').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListStudyLevel').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListStudyGroup').RequiredDropdown() == false) {
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
    var Description = $('#TextBoxDescription').val();
    var StudyGroupId = $('#DropDownListStudyGroup :selected').val();
    var StudyLevelId = $('#DropDownListStudyLevel :selected').val();

    var ClassGuID = $('#HiddenFieldClassGuID').val();

    var JsonArg = {
        GuID: ClassGuID,
        OperationType: OperationType,
        CampusId: CampusId,
        Description: Description,
        StudyLevelId: StudyLevelId,
        StudyGroupId: StudyGroupId,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MClassUI/UpSert_Into_AppClass",
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
    $('.form-control').not('#DropDownListClass,#DropDownListCampus').val('');
    $('.select2').not('#DropDownListClass,#DropDownListCampus').val('-1').change();
    $('form').removeClass('Is-Valid');
}


/*----------------------------------** FUNCTION FOR:: UPDATE CLASS (LOAD DROPDOWN,DATA FOR CLASSID) **-----------------------------------------*/

$('#ButtonSubmitGetInfoForEdit').click(function () {
    startLoading();

    ClearInputFields();
    if ($('#DropDownListClass').RequiredDropdown() == false) {
        return;
    }
    else {
        GET_APPCLASS_INFOBYGUID();
    }
    stopLoading();

});
function GET_APPCLASS_LISTBYPARAM() {
    $('#DropDownListClass').empty();
    $('#DropDownListClass').select2({
        placeholder: 'Search By Class Name / Class Code',
        minimumInputLength: 3,
        ajax: {
            url: BasePath + "/ACompany/MClassUI/GET_MT_APPCLASS_BYPARAMETER_SEARCH",
            type: "POST",
            delay: 250,
            data: function (params) {
                return {
                    PostedData: { 
                        SearchParameter: params.term,
                        DB_IF_PARAM: DOCUMENT_LIST_CONDITION.APPCLASS_BY_ALLOWEDBRANCHIDS_SEARCH_PARAMETER_UPDATECLASS,
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
                    }),
                    pagination: {
                        more: data.hasMore, // Assume that the response contains hasMore to indicate if there are more pages
                    }
                };
            },

            complete: function () {
                stopLoading();
            },
        },
    });
}
function GET_APPCLASS_INFOBYGUID() {
    var ClassId = $('#DropDownListClass :selected').val();
    if (ClassId != null && ClassId != undefined && ClassId != "" && ClassId != "-1") {

        var JsonArg = {
            GuID: ClassId,
        }
        $.ajax({
            type: "POST",
            url: BasePath + "/ACompany/MClassUI/GET_MT_APPCLASS_INFOBYGUID",
            dataType: 'json',
            data: { 'PostedData': (JsonArg) },
            beforeSend: function () {
                startLoading();
            },
            success: function (data) {
                $('#DropDownListCampus').val(data[0].CampusId).change();
                $('#TextBoxDescription').val(data[0].Description);

                setTimeout(function () {
                    $('#DropDownListStudyLevel').val(data[0].StudyLevelId).change();
                }, 1000);
                setTimeout(function () {
                    $('#DropDownListStudyGroup').val(data[0].StudyGroupId).change();

                }, 1500);
                $('#HiddenFieldClassGuID').val(data[0].GuID);

             

            },
            complete: function () {
                stopLoading();
            },

        });
    }


}