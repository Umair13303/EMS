
var OperationType = "";
var DB_OperationType = $('#HiddenFieldDB_OperationType').val();
var IsFieldClear = false;

var table = "";
var ClassIds;
var ChallanNumberForCampus;

$(document).ready(function () {

    DB_OperationType = $('#HiddenFieldDB_OperationType').val();
    switch (DB_OperationType) {
        case PARAMETER.DB_OperationType.INSERT:
            $('#DivButtonSubmitDown').show();
            $('#DivButtonUpdateDown').hide();
            break;
        case PARAMETER.DB_OperationType.UPDATE:
            //GET_APPSESSION_BY_GENERALBRANCH();
            $('#DivButtonSubmitDown').hide();
            $('#DivButtonUpdateDown').show();
            break;
    }
    InitDataTable();
    PopulateDropDownLists();
    ChangeCase();
});

//-----------UTTILTIY COMPONENT
function InitDatePickerDataTable() {
    var SessionPeriod = $('#TextBoxSessionPeriod').val();
    var Dates = SessionPeriod.split("to");
    var PeriodStartDate = Dates[0].trim().toString();

    // Corrected handling of PeriodEndDate
    var PeriodEndDate = (Dates[1] ? Dates[1].trim() : PeriodStartDate).toString();

    flatpickr(document.getElementsByClassName('DatePickerSimple'), {
        dateFormat: "Y-m-d",
        enableTime: false,
        noCalendar: false,
        minDate: PeriodStartDate,
        maxDate: PeriodEndDate,
    });
}


//-----------ALL DATA TABLE
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
function InsertDataInto_SessionDetailTable() {
    startLoading();
    table.clear().draw();
    var row_data = [];

    try {
        for (var i = 0; i < ChallanNumberForCampus; i++) {

            row_data[0] = "";
            row_data[1] = "<input Id='TextBoxSessionDescription" + (i + 1) + "' type='text' class='form-control form-control-sm' disabled='' value='Session Period No:" + (i + 1)+"'/>";
            row_data[2] = "<input Id='TextBoxPeriodStartDate" + (i + 1) + "' type='text' class='form-control form-control-sm DatePickerSimple' placeholder='YYYY-MM-DD'/>";
            row_data[3] = "<input Id='TextBoxPeriodEndDate" + (i + 1) + "' type='text' class='form-control form-control-sm DatePickerSimple' placeholder='YYYY-MM-DD'/>";

            table.row.add(row_data).draw();
            stopLoading();
        }
    } catch (err) {
        GetMessageBox(err, 500);
    }
}


function PopulateDropDownLists() {
    PopulateMT_GeneralBranch_ListByParam();
    PopulateLK_EnrollmentType_List();

}

//-----------ALL CHANGE CASES
function ChangeCase() {
   
    $('#TextBoxSessionPeriod').change(function (event) {
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
            //CHECK_APPSESSION_FOR_CLASS(ClassIds);
        }
    });

    $('#DropDownListSession').change(function () {
        if (!IsFieldClear) {
            IsFieldClear = true;
            ClearInputFields();
            IsFieldClear = false;
        }
    });
}

//-----------ALL DROPDOWN LIST
function PopulateLK_EnrollmentType_List() {
    var JsonArg = {
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MAcademicSessionUI/GET_LK1_ENROLLMENTTYPES",
        data: { 'PostedData': (JsonArg) },
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
function PopulateMT_GeneralBranch_ListByParam() {
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.BRANCH_BY_USER_ALLOWEDBRANCHIDS,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MAcademicSessionUI/GET_MT_GENERALBRANCH_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '">' + data[i].Description + '</option>';
            }
            $("#DropDownListCampus").html(s);
        },
        complete: function () {
            $("#DropDownListCampus").val(BranchId).change();

            stopLoading();
        },
    });
}
function PopulateMT_AppClass_ListByParam() {
    var JsonArg = {
        CampusId: $('#DropDownListCampus :selected').val(),
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.APPCLASS_BY_GENERALBRANCH,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MAcademicSessionUI/GET_MT_APPCLASS_BYPARAMETER",
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

//-----------DB OPERATION CALL
$('#ButtonSubmitDown').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateInputFields();
    if (IsValid) {
        try {
            OperationType = PARAMETER.DB_OperationType.INSERT;
            UpSertDataIntoDB();
        }
        catch (err) {
            GetMessageBox(err, 505);
        }
    }
});
$('#ButtonUpdateDown').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateInputFields();
    if (IsValid) {
        try {
            OperationType = PARAMETER.DB_OperationType.UPDATE;
            UpSertDataIntoDB();
        }
        catch {
            GetMessageBox(err, 505);
        }
    }
});

function ValidateInputFields() {

    if ($('#DropDownListCampus').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListEnrollmentType').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxSessionDescription').RequiredTextBoxInputGroup() == false) {
        return false;
    }  
    if ($('#TextBoxSessionPeriod').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListClass').RequiredDropdown() == false) {
        return false;
    }

    table.rows().every(function (rowIdx, tableLoop, rowLoop) {

        var _Description = table.cell({ row: rowIdx, column: 1 }).node();
        var Description = $('input', _Description).val();
        if (Description == "" || Description == undefined || Description == null) {
            GetMessageBox("In-Correct Description at Row #: " + (rowIdx+1),500);
            return false;
        }

        var _PeriodStartOn = table.cell({ row: rowIdx, column: 2 }).node();
        var PeriodStartOn = $('input', _PeriodStartOn).val();

        if (PeriodStartOn == "" || PeriodStartOn == undefined || PeriodStartOn == null) {
            GetMessageBox("In-Correct Period Start Date at Row #: "  + (rowIdx + 1), 500);
            return false;
        }
        var _PeriodEndOn = table.cell({ row: rowIdx, column: 3 }).node();
        var PeriodEndOn = $('input', _PeriodEndOn).val();
        if (PeriodEndOn == "" || PeriodEndOn == undefined || PeriodEndOn == null) {
            
            GetMessageBox("In-Correct Period End Date at Row #: " + + (rowIdx + 1), 500);
            return false;

        }

    });
    return true;
}

function UpSertDataIntoDB() {

    var CampusId            = $('#DropDownListCampus :selected').val();
    var EnrollmentTypeId    = $('#DropDownListEnrollmentType :selected').val();
    var Description         = $('#TextBoxSessionDescription').val();
    var SessionPeriod       = $('#TextBoxSessionPeriod').val();
    var Dates               = SessionPeriod.split("to");
    var SessionStartDate    = Dates[0].trim();
    var SessionEndDate      = Dates[1].trim();
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
    
        var _Description = table.cell({ row: rowIdx, column: 1 }).node();
        var Description = $('input', _Description).val();

        var _PeriodStartOn = table.cell({ row: rowIdx, column: 2 }).node();
        var PeriodStartOn = $('input', _PeriodStartOn).val();

        var _PeriodEndOn = table.cell({ row: rowIdx, column: 3 }).node();
        var PeriodEndOn = $('input', _PeriodEndOn).val();


        var JsonArg_SessionDetail = {
            Description: Description,
            PeriodStartOn: PeriodStartOn,
            PeriodEndOn: PeriodEndOn,
        }
        SessionDetailArray.push(JsonArg_SessionDetail);

    });

    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MAcademicSessionUI/UpSert_Into_AppSession",
        dataType: 'json',
        data: { 'PostedData': (JsonArg), 'PostedDataDetail': (SessionDetailArray) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            GetMessageBox(data.Message, data.StatusCode);
            ClearInputFields();
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
    //-----------NOT CLEARING REQUIRED FIELD
    $('.form-control').not('#DropDownListSession').val('');
    $('.select2').not('#DropDownListSession').val('-1').change();
    $('form').removeClass('Is-Valid');
    table.clear().draw();
}


//-----------LOAD ENTERY RECORD :: CHALLAN SETTING FOR SELECTED CAMPUS
function FN_GENERALBRANCHSETTING_INFO_BY_CAMPUSID() {
    var CampusId = $('#DropDownListCampus :selected').val();
    if (CampusId != null && CampusId != undefined && CampusId != "" && CampusId != "-1") {

        var JsonArg = {
            CampusId: CampusId,
        }
        $.ajax({
            type: "POST",
            url: BasePath + "/ACompany/MAcademicSessionUI/FN_GENERALBRANCHSETTING_INFO_BY_CAMPUSID",
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
                    InsertDataInto_SessionDetailTable();
                }
            },
            complete: function () {
                stopLoading();
            },
        });
    }
}


