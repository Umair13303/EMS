/*----------------------------------** GLOBAL VARIABLE FOR PAGE :: CREATE/UPDATE APP SESSION **-----------------------------------------------------------------------------*/
var OperationType = "";
var DDL_Condition = "";
var DB_OperationType = $('#HiddenFieldDB_OperationType').val();
var IsFieldClear = false;


var table = "";
var ClassIds;
var ChallanNumberForCampus;


/*----------------------------------** FUNCTION FOR::PAGE LOADER **---------------------------------------------------------------------------------------------------------*/
$(document).ready(function () {

    DB_OperationType = $('#HiddenFieldDB_OperationType').val();
    switch (DB_OperationType) {
        case DBOperation.INSERT:
            $('#DivButtonSubmitDown').show();
            $('#DivButtonUpdateDown').hide();
            break;
        case DBOperation.UPDATE:
            GET_APPSESSION_LISTBYPARAM();
            $('#DivButtonSubmitDown').hide();
            $('#DivButtonUpdateDown').show();
            break;
    }
    InitDataTable();
    PopulateDropDownLists();
    ChangeCase();
});

/*----------------------------------** FUNCTION FOR::DATA TABLE OPERATION (INIITALIZING, DATEPICKER,UPDATE_INTO_DATATABLE, INSERT_INTO_DATABLE) **------------------------------------------------*/
function InitDataTable() {
    table = $('#MainTableSessionDetail').DataTable({
        "responsive": true, "ordering": false,
        "processing": true, "pagination": false,
        "paging": false,
        "columns": [
            { "title": "#", "orderable": false, },
            { "title": "Description" },
            { "title": "Start Date" },
            { "title": "End Date" },
            { "title": "Session Detail GUID", visible:false },
        ],
        "columnDefs": [
        ],
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });

    }).draw();
}
function InitDatePickerDataTable() {
    var PeriodStartDate = ($("#TextBoxSessionStartDate").val()).toString();
    var PeriodEndDate = ($("#TextBoxSessionEndDate").val()).toString();
    flatpickr(document.getElementsByClassName('DatePickerSimple'), {
        dateFormat: "Y-m-d",
        enableTime: false,
        noCalendar: false,
        allowInput: true,
        minDate: PeriodStartDate,
        maxDate: PeriodEndDate,
    });
}
function GET_APPSESSIONDETAIL_INFOBYGUID_OR_INSERT() {
    table.clear().draw();
    switch (DB_OperationType) {
        case DBOperation.INSERT:
            var row_data = [];

            for (var i = 0; i < ChallanNumberForCampus; i++) {
                row_data[0] = "";
                row_data[1] = "<input Id='TextBoxSessionDetailDescription" + (i + 1) + "' type='text' class='form-control form-control-sm' disabled='' value='Session Period No:" + (i + 1) + "'/>";
                row_data[2] = "<input Id='TextBoxPeriodStartDate" + (i + 1) + "' type='text' class='form-control form-control-sm DatePickerSimple' placeholder='YYYY-MM-DD'/>";
                row_data[3] = "<input Id='TextBoxPeriodEndDate" + (i + 1) + "' type='text' class='form-control form-control-sm DatePickerSimple' placeholder='YYYY-MM-DD'/>";
                row_data[4] = "";
                table.row.add(row_data).draw();
            }
            break;

        case DBOperation.UPDATE:
            var SessionId = $('#DropDownListSession :selected').val();
            if (SessionId != null && SessionId != undefined && SessionId != "" && SessionId != "-1") {
                var JsonArg = {
                    GuID: SessionId,
                }
                $.ajax({
                    type: "POST",
                    url: BasePath + "/ACompany/MSessionUI/GET_MT_APPSESSIONDETAIL_INFOBYGUID",
                    dataType: 'json',
                    data: { 'PostedData': (JsonArg) },
                    beforeSend: function () {
                        startLoading();
                    },
                    success: function (data) {
                        var row_data = [];

                        for (var i = 0; i < data.length; i++) {
                            row_data[0] = "";
                            row_data[1] = "<input Id='TextBoxSessionDetailDescription" + (i + 1) + "' type='text' class='form-control form-control-sm' disabled='' value='" + data[i].Description + "'/>";
                            row_data[2] = GET_DATEPICKER_FORUPDATE_INTOLIST(data[i].PeriodStartOn, DatePickerFormat.Simple, "TextBoxPeriodStartDate" + (i + 1), "DatePickerSimple");
                            row_data[3] = GET_DATEPICKER_FORUPDATE_INTOLIST(data[i].PeriodEndOn, DatePickerFormat.Simple, "TextBoxPeriodEndDate" + (i + 1), "DatePickerSimple");
                            row_data[4] = "<input Id='TextBoxSessionDetailGUID" + (i + 1) + "' type='text' class='form-control form-control-sm' disabled='' value='" + data[i].GuID + "'/>";
                            table.row.add(row_data).draw();
                        }

                    },
                    complete: function () {
                        stopLoading();
                    },

                });
            }
            break;
    }
}



function PopulateDropDownLists() {
    PopulateMT_GeneralBranch_ListByParam();
    PopulateLK_EnrollmentType_List();

}

/*----------------------------------** FUNCTION FOR::CHANGE CASE LOADER **--------------------------------------------------------------------------------------------------*/

function ChangeCase() {

    $('#TextBoxSessionEndDate,#TextBoxSessionStartDate').change(function (event) {
        event.preventDefault();
        InitDatePickerDataTable();
    });
    $('#DropDownListCampus').change(function (event) {
        event.preventDefault();
        $('#DropDownListClass').val('-1').change();
        PopulateMT_AppClass_ListByParam();
        FN_GENERALBRANCHSETTING_INFO_BY_CAMPUSID();
    });
    $("#DropDownListClass").attr('data-width', '100%').select2({
        placeholder: 'Select an Option',
        multiple: true,
    }).change(function (event) {
        if (event.target == this) {
            ClassIds = $(this).val();
        }
    });

    //-----------FOR ::EDIT CASE
    $('#DropDownListSession').change(function () {
        if (!IsFieldClear) {
            IsFieldClear = true;
            ClearInputFields();
            IsFieldClear = false;
        }
    });
}

/*----------------------------------** FUNCTION FOR:: RENDER DROP DOWN FROM DB_MAIN-- STORED PROCEDURE **-------------------------------------------------------------------*/
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
        url: BasePath + "/ACompany/MSessionUI/GET_MT_GENERALBRANCH_BYPARAMETER",
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
function PopulateMT_AppClass_ListByParam() {
    switch (DB_OperationType) {
        case DBOperation.INSERT:
            DDL_Condition = MDB_LIST_CONDITION.APPCLASS_BY_GENERALBRANCH_FORNEWINSERT;
            break;
        case DBOperation.UPDATE:
            DDL_Condition = MDB_LIST_CONDITION.APPCLASS_BY_GENERALBRANCH_FORUPDATERECORD;
            break;
    }
    var JsonArg = {
        CampusId: $('#DropDownListCampus :selected').val(),
        DB_IF_PARAM: DDL_Condition,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MSessionUI/GET_MT_APPCLASS_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListClass").html(s);

        },
        complete: function () {
            stopLoading();
        },
    });
}


/*----------------------------------** FUNCTION FOR:: RENDER DROP DOWN FROM DB_LOOKUP-- LINQUERY **-------------------------------------------------------------------------*/
function PopulateLK_EnrollmentType_List() {
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MSessionUI/GET_LK1_ENROLLMENTTYPES",
        data: {  },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListEnrollmentType").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}

/*----------------------------------** FUNCTION FOR:: GET DOCUMENT SETTING (GENERAL_BRANCH_SETTING BY CAMPUSID) FROM DB_MAIN-- DBO_FUNCTION **------------------------------*/
function FN_GENERALBRANCHSETTING_INFO_BY_CAMPUSID() {
    var CampusId = $('#DropDownListCampus :selected').val();
    if (CampusId != null && CampusId != undefined && CampusId != "" && CampusId != "-1") {

        var JsonArg = {
            CampusId: CampusId,
        }
        $.ajax({
            type: "POST",
            url: BasePath + "/ACompany/MSessionUI/FN_GENERALBRANCHSETTING_INFO_BY_CAMPUSID",
            dataType: 'json',
            data: { 'PostedData': (JsonArg) },
            beforeSend: function () {
                startLoading();
            },
            success: function (data) {
                if (data.length == undefined) {
                }
                else {
                    ChallanNumberForCampus = data[0].AllowedChallanNo;
                    GET_APPSESSIONDETAIL_INFOBYGUID_OR_INSERT();
                }
            },
            complete: function () {
                stopLoading();
            },
        });
    }
}

/*----------------------------------** FUNCTION FOR:: DATABASE OPERATION (VALIDATE,UPSERT,CLEAR) **-------------------------------------------------------------------------*/
function ValidateInputFields() {

    if ($('#DropDownListCampus').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListEnrollmentType').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxDescription').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxSessionStartDate').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxSessionEndDate').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListClass').RequiredDropdown() == false) {
        return false;
    }

    var IS_SESSIONDETAIL_VALID = true;

    table.rows().every(function (rowIdx, tableLoop, rowLoop) {

        var _Description = table.cell({ row: rowIdx, column: 1 }).node();
        var Description = $('input', _Description).val();
        if (Description == "" || Description == undefined || Description == null) {
            GetMessageBox("In-Correct Description at Row #: " + (rowIdx + 1), 500);
            IS_SESSIONDETAIL_VALID = false;
            return false;
        }

        var _PeriodStartOn = table.cell({ row: rowIdx, column: 2 }).node();
        var PeriodStartOn = $('input', _PeriodStartOn).val();

        if (PeriodStartOn == "" || PeriodStartOn == undefined || PeriodStartOn == null) {
            GetMessageBox("In-Correct Period Start Date at Row #: " + (rowIdx + 1), 500);
            IS_SESSIONDETAIL_VALID = false;
            return false;
        }

        var _PeriodEndOn = table.cell({ row: rowIdx, column: 3 }).node();
        var PeriodEndOn = $('input', _PeriodEndOn).val();
        if (PeriodEndOn == "" || PeriodEndOn == undefined || PeriodEndOn == null) {
            GetMessageBox("In-Correct Period End Date at Row #: " + (rowIdx + 1), 500);
            IS_SESSIONDETAIL_VALID = false;
            return false;
        }
    });

    return IS_SESSIONDETAIL_VALID; // Return false if any row validation fails, otherwise return true
}
$('#ButtonSubmitDown').click(function (event) {
    event.preventDefault();
    var IS_VALID = ValidateInputFields();
    if (IS_VALID) {
        try {
            OperationType = DBOperation.INSERT;
            UpSertDataIntoDB();

        }
        catch (err) {
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
    var CampusId            = $('#DropDownListCampus :selected').val();
    var EnrollmentTypeId    = $('#DropDownListEnrollmentType :selected').val();
    var Description         = $('#TextBoxDescription').val();
    var SessionStartDate    = $('#TextBoxSessionStartDate').val().toString();
    var SessionEndDate      = $('#TextBoxSessionEndDate').val().toString();
    var SessionGuID         = $('#HiddenFieldSessionGuID').val();

    var JsonArg = {
        GuID: SessionGuID,
        OperationType: OperationType,
        CampusId: CampusId,
        Description: Description,
        EnrollmentTypeId: EnrollmentTypeId,
        SessionStartDate: SessionStartDate,
        SessionEndDate: SessionEndDate,
        ClassIds: ClassIds.toString(),
    }
    var SessionDetailArray = [];
    table.rows().every(function (rowIdx, tableLoop, rowLoop) {
        var _Description    = table.cell({ row: rowIdx, column: 1 }).node();
        var Description     = $('input', _Description).val();

        var _PeriodStartOn = table.cell({ row: rowIdx, column: 2 }).node();
        var PeriodStartOn   = $('input', _PeriodStartOn).val();

        var _PeriodEndOn    = table.cell({ row: rowIdx, column: 3 }).node();
        var PeriodEndOn = $('input', _PeriodEndOn).val();

        var _SessionDetailGUID = table.cell({ row: rowIdx, column: 4 }).node();
        var SessionDetailGUID = $('input', _SessionDetailGUID).val();

        var JsonArg_SessionDetail = {
            Description: Description,
            PeriodStartOn: PeriodStartOn,
            PeriodEndOn: PeriodEndOn,
            GuID: SessionDetailGUID,
        }
        SessionDetailArray.push(JsonArg_SessionDetail);
    });
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MSessionUI/UpSert_Into_AppSession",
        dataType: 'json',
        data: { 'PostedData': (JsonArg), 'PostedDataDetail': (SessionDetailArray) },
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
    $('.form-control').not('#DropDownListSession').val('');
    $('.select2').not('#DropDownListSession').val('-1').change();
    $('form').removeClass('Is-Valid');
    table.clear().draw();
}


/*----------------------------------** FUNCTION FOR:: UPDATE SESSION (LOAD DROPDOWN,DATA FOR SESSIONID) **-----------------------------------------*/

$('#ButtonSubmitGetInfoForEdit').click(function () {
    ClearInputFields();
    if ($('#DropDownListSession').RequiredDropdown() == false) {
        return;
    }
    else {
        GET_APPSESSION_INFOBYGUID();
    }

});
function GET_APPSESSION_LISTBYPARAM() {
    $('#DropDownListSession').empty();
    $('#DropDownListSession').select2({
        placeholder: 'Search By Session Name / Session Code',
        minimumInputLength: 3,
        ajax: {
            url: BasePath + "/ACompany/MSessionUI/GET_MT_APPSESSION_BYPARAMETER_SEARCH",
            type: "POST",
            delay: 250,
            data: function (params) {
                return {
                    PostedData: {
                        SearchParameter: params.term,
                        DB_IF_PARAM: DOCUMENT_LIST_CONDITION.APPSESSION_BY_ALLOWEDBRANCHIDS_SEARCH_PARAMETER_UPDATESESSION,
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
                            SessionId: item.Id,
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
function GET_APPSESSION_INFOBYGUID() {
    var SessionId = $('#DropDownListSession :selected').val();
    if (SessionId != null && SessionId != undefined && SessionId != "" && SessionId != "-1") {

        var JsonArg = {
            GuID: SessionId,
        }
        $.ajax({
            type: "POST",
            url: BasePath + "/ACompany/MSessionUI/GET_MT_APPSESSION_INFOBYGUID",
            dataType: 'json',
            data: { 'PostedData': (JsonArg) },
            beforeSend: function () {
                startLoading();
            },
            success: function (data) {

                $('#DropDownListCampus').val(data[0].CampusId).change();
                $('#DropDownListEnrollmentType').val(data[0].EnrollmentTypeId).change();
                $('#TextBoxDescription').val(data[0].Description);


                GET_DATEPICKER_FORUPDATE_INPUTFIELD(data[0].SessionStartDate, DatePickerFormat.Simple, "TextBoxSessionStartDate");
                GET_DATEPICKER_FORUPDATE_INPUTFIELD(data[0].SessionEndDate, DatePickerFormat.Simple, "TextBoxSessionEndDate");
                $('#HiddenFieldSessionGuID').val(data[0].GuID)
                setTimeout(function () {
                    $('#DropDownListClass').val(data[0].ClassIds.split(',')).change();
                }, 500);
            },
            complete: function () {
                stopLoading();

            },

        });
    }
}
