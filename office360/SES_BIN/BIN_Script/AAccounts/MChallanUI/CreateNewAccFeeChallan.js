
var OperationType = "";
var DB_OperationType = $('#HiddenFieldDB_OperationType').val();
var IsFieldClear = false;
var FeeChallanDetailTable = "";
var EnrollmentStudentDetailTable = "";


$(document).ready(function () {

    DB_OperationType = $('#HiddenFieldDB_OperationType').val();
    switch (DB_OperationType) {
        case PARAMETER.DB_OperationType.INSERT:
            $('#DivButtonSubmitDown').show();
            $('#DivButtonUpdateDown').hide();
            break;
        case PARAMETER.DB_OperationType.UPDATE:
            $('#DivButtonSubmitDown').hide();
            $('#DivButtonUpdateDown').show();
            break;
    }
    InitDataTable();
    PopulateDropDownLists();
    ChangeCase();
});


//----------- ALL DATA TABLE FUNCTION
function InitDataTable() {
    FeeChallanDetailTable = $('#MainTableFeeStructure').DataTable({
        "responsive": true, "ordering": false,
        "processing": true, "pagination": false,
        "paging": false,
        "columns": [

            { "data": null,                     "title": "#", "orderable": false, },
            { "data": "FeeName",                "title": "Fee", },//1
            { "data": "RevenueAccount",         "title": "Revenue A/c", },//2
            { "data": "AssetAccount",           "title": "Asset A/c", },//3
            { "data": "LiabilityAccount",       "title": "Liability A/c", },//4
            { "data": "CostOfSaleAccount",      "title": "Cost Of Sale A/c", },//5
            { "data": "FeeAmount",              "title": "Amount", },//6

            { "data": "FeeTypeId",              "title": "FeeTypeId", },//7
            { "data": "FeeStructureId",         "title": "FeeStructureId", },//8

        ],
        "columnDefs": [
            { visible: false, targets: [7,8] },
        ],
    });
    FeeChallanDetailTable.on('order.dt search.dt', function () {
        FeeChallanDetailTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });

    }).draw();


    EnrollmentStudentDetailTable = $('#MainTableEnrollmentRegistration').DataTable({
        "responsive": true, "ordering": false,
        "processing": true, "pagination": false,
        "paging": false,
        "columns": [

            { "data": null,                     "title": "#", "orderable": false, },
            { "data": "Id",                     "title": "Id", },//1
            { "data": "GuID",                   "title": "Student GuID", },//1
            { "data": "Description",            "title": "Student Code", },//1
           

        ],
        "columnDefs": [
            { visible: false, targets: [] },
        ],
    });
    EnrollmentStudentDetailTable.on('order.dt search.dt', function () {
        EnrollmentStudentDetailTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });

    }).draw();


}
function ValidateInputFieldForDetailList() {
    if ($('#DropDownListCampus').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListSession').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListSessionPeriod').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListClass').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListChallanType').RequiredDropdown() == false) {
        return false;
    }
    return true;

};
$('#ButtonSubmitGetFeeChallanDetail').click(function (event) {
    event.preventDefault();

    var IsValid = ValidateInputFieldForDetailList();
    if (IsValid) {
        FeeChallanDetailTable.clear().draw();
        var CampusId = $('#DropDownListCampus :selected').val();
        var ClassId = $('#DropDownListClass :selected').val();
        var DB_IF_PARAM = $('#DropDownListChallanType :selected').attr('data-DB_IF_PARAM');

        var JsonArg = {
            DB_IF_PARAM: DB_IF_PARAM,
            CampusId: CampusId,
            ClassId: ClassId,
        };
        var queryString = $.param(JsonArg);
        FeeChallanDetailTable.ajax.url((BasePath + "/AAccounts/MChallanUI/GET_MT_ACCFEESTRUCTUREDETAIL_BYPARAM_FORDT?" + queryString)).load();
    }
});
$('#ButtonSubmitGetStudentDetail').click(function (event) {
    event.preventDefault();

    var IsValid = ValidateInputFieldForDetailList();
    if (IsValid) {
        var DB_IF_PARAM = "";

        EnrollmentStudentDetailTable.clear().draw();

        var CampusId = $('#DropDownListCampus :selected').val();
        var SessionId = $('#DropDownListSession :selected').val();
        var SessionPeriodId = $('#DropDownListSessionPeriod :selected').val();
        var ClassId = $('#DropDownListClass :selected').val();
        var ChallanTypeId = $('#DropDownListChallanType :selected').val();

        switch (ChallanTypeId) {

            case PARAMETER.ChallanType.CHALLAN_FOR_NEW_ADMISSION:
                DB_IF_PARAM = PARAMETER.DB_IF_Condition.ENRREGISTRATION_BY_APPCLASS_NEW_ADMISSION;
                break;

            case PARAMETER.ChallanType.CHALLAN_FOR_EXISTING_ADMISSION:
                DB_IF_PARAM = PARAMETER.DB_IF_Condition.ENRREGISTRATION_BY_APPCLASS_REGISTERED_ADMISSION;
                break;
        }

        var JsonArg = {
            CampusId: CampusId,
            SessionId: SessionId,
            SessionPeriodId: SessionPeriodId,
            ClassId: ClassId,
            DB_IF_PARAM: DB_IF_PARAM,
        }
        var queryString = $.param(JsonArg);
        EnrollmentStudentDetailTable.ajax.url((BasePath + "/AAccounts/MChallanUI/GET_MT_ENRREGISTRATION_BYPARAM_FORDT?" + queryString)).load();
    }
});



function PopulateDropDownLists() {
    PopulateMT_GeneralBranch_ListByParam();
    PopulateLK_ChallanType_List();
}


//-----------ALL CHANGE CASES
function ChangeCase() {
    $('#DropDownListCampus').change(function (event) {
        event.preventDefault();
        $('#DropDownListSession').val('-1').change();
            PopulateMT_AppSession_ListByParam();
    });
    $('#DropDownListSession').change(function (event) {
        event.preventDefault();
        $('#DropDownListClass').val('-1').change();
            PopulateMT_AppSessionDetail_ListByParam();
            PopulateMT_AppClass_ListByParam();
    });

    //$('#DropDownListClass').change(function (event) {
    //    event.preventDefault();
    //       // CHECK_FEESTRUCTURE_FOR_CLASS();
    //});

}


//-----------ALL DROPDOWN LIST
function PopulateMT_GeneralBranch_ListByParam() {
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.BRANCH_BY_USER_ALLOWEDBRANCHIDS,
    }
    $.ajax({

        type: "POST",
        url: BasePath + "/AAccounts/MChallanUI/GET_MT_GENERALBRANCH_BYPARAMETER",
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
        url: BasePath + "/AAccounts/MChallanUI/GET_MT_APPSESSION_BYPARAMETER",
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
        url: BasePath + "/AAccounts/MChallanUI/GET_MT_APPSESSIONDETAIL_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '">' + data[i].Description + '</option>';
            }
            $("#DropDownListSessionPeriod").html(s);
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
        url: BasePath + "/AAccounts/MChallanUI/GET_MT_APPCLASS_BYPARAMETER",
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
function PopulateLK_ChallanType_List() {
    var JsonArg = {
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MChallanUI/GET_LK1_CHALLANTYPE",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option ' +
                    'data-DB_IF_PARAM="' + data[i].DB_IF_PARAM + '" ' +
                    'value="' + data[i].Id + '">' + data[i].Description +
                    '</option>';
            }

            $("#DropDownListChallanType").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}





////-----------DUPLICATE CHECK :: IF FEE STRUCTURE EXIST FOR SELECTED CLASS
//function CHECK_FEESTRUCTURE_FOR_CLASS() {
//    var SessionId = $('#DropDownListSession :selected').val();
//    var ClassId = $('#DropDownListClass :selected').val();
//    var JsonArg = {
//        SessionId: SessionId,
//        ClassId: ClassId,
//    }

//    $.ajax({
//        type: "POST",
//        url: BasePath + "/AAccounts/MChallanUI/CHECK_FEESTRUCTURE_FOR_CLASS",
//        data: { 'PostedData': (JsonArg) },
//        beforeSend: function () {
//            startLoading();
//        },
//        success: function (data) {
//            if ((data.length < 0) || (data.length == 0) || (data == undefined)) {
//                GetMessageBox("No Fee Found For Selected Class", 500);
//                return true;

//            }
//            else if ((data.length > 0) || (data.length != 0) || (data != undefined))
//            {
//                return false;
//            }

//        },
//        complete: function () {
//            stopLoading();
//        },
//    });
//}