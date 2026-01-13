var OperationType = "";
var DB_OperationType = $('#HiddenFieldDB_OperationType').val();
var IsFieldClear = false;

var table = "";
var FeeSettingIsSecurity;
var FeeSettingIsRefundable;
var FeeSettingIsDiscount;
var WH_Percentage;
var WH_SlabAmount;
var WH_FixedCharges;
var WH_IsOnExceedingAmount;

$(document).ready(function () {
    InitDataTable();
    PopulateDropDownLists();
    ChangeCase();

    switch (DB_OperationType) {
        case PARAMETER.DB_OperationType.INSERT:
            $('#DivButtonSubmitDown').show();
            $('#DivButtonUpdateDown').hide();
            break;
        case PARAMETER.DB_OperationType.UPDATE:
            GET_ACCFEESTRUCTURE_LISTBYPARAM();
            $('#DivButtonSubmitDown').hide();
            $('#DivButtonUpdateDown').show();
            break;
    }


});

//-----------ALL DATA TABLE
function InitDataTable() {
    table = $('#MainTableFeeStructure').DataTable({
        "responsive": true, "ordering": false,
        "processing": true, "pagination": false,
        "paging": false,
        "columns": [
            { "title": "#", "orderable": false, },
            { "title": "Fee" },
            { "title": "Amount" },
            { "title": "Action" },
            { "title": "FeeTypeId", },
            { "title": "RevenueAccountId", },
            { "title": "AssetAccountId", },
            { "title": "LiabilityAccountId", },
            { "title": "CostOfSaleAccountId", },
        ],
        "columnDefs": [
            { visible: false, targets: [4, 5, 6, 7, 8] },
        ],
    });
    table.on('draw', function () {
        CalcBoxDataTable();
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });

    }).draw();
    addDataTableFooter(
                                       table, 1, "TDTotalFeeExclusiveAmount", "Recievable Fee", 1)
        .then(() => addDataTableFooter(table, 1, "TDTotalFeeAmount", "Total Fee Incl. Tax", 3))
        .then(() => addDataTableFooter(table, 1, "TDWHTAmount", "Applicable Tax", 2));
}

//-----------DATA TABLE BASED OPERATION

$('#ButtonPlus').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateInputFieldsFeeStructure();
    if (IsValid) {
        try {

            var AssetAccountId = null; var RevenueAccountId = null; var CostOfSaleAccountId = null; var LiabilityAccountId = null;
            var row_data = [];
            var FeeType = $('#DropDownListFeeType :selected').text();
            var FeeTypeId = $('#DropDownListFeeType :selected').val();
            var Amount = parseFloat($('#TextBoxAmount').val()) || 0;

            if (FeeSettingIsSecurity == true && FeeSettingIsDiscount == true) {
                AssetAccountId = $('#DropDownListAssetAccount :selected').val();
                LiabilityAccountId = $('#DropDownListLiabilityAccount :selected').val();
                CostOfSaleAccountId = $('#DropDownListCostOfSaleAccount :selected').val();
            }
            else if (FeeSettingIsSecurity == false && FeeSettingIsDiscount == true) {
                RevenueAccountId = $('#DropDownListRevenueAccount :selected').val();
                AssetAccountId = $('#DropDownListAssetAccount :selected').val();
                CostOfSaleAccountId = $('#DropDownListCostOfSaleAccount :selected').val();
            }
            else if (FeeSettingIsSecurity == true && FeeSettingIsDiscount == false) {
                AssetAccountId = $('#DropDownListAssetAccount :selected').val();
                LiabilityAccountId = $('#DropDownListLiabilityAccount :selected').val();
            }
            else if (FeeSettingIsSecurity == false && FeeSettingIsDiscount == false) {
                RevenueAccountId = $('#DropDownListRevenueAccount :selected').val();
                AssetAccountId = $('#DropDownListAssetAccount :selected').val();
            }
            row_data[0] = "";
            row_data[1] = FeeType;
            row_data[2] = Amount.toFixed(2);
            row_data[3] = GetDeletebtn() + GetEditbtn();
            row_data[4] = FeeTypeId;
            row_data[5] = RevenueAccountId;
            row_data[6] = AssetAccountId;
            row_data[7] = LiabilityAccountId;
            row_data[8] = CostOfSaleAccountId;
            var IsAlreadyExist = false;
            table.column(1).data().each(function (value, index) {
                if (value == (row_data[1])) {
                    IsAlreadyExist = true;
                }
            });
            if (IsAlreadyExist) {
                GetMessageBox("Duplicate Record Exist", 505)
            }
            else {
                table.row.add(row_data).draw();
                ClearOtherFeeSetting();
            }
            IsAlreadyExist = false;
        }
        catch (err){
            GetMessageBox(err, 500);
        }
    }
});
function ValidateInputFieldsFeeStructure() {
    if ($('#DropDownListWHTaxPolicy').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListFeeType').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxAmount').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    return true;
}
$('#MainTableFeeStructure tbody').on('click', '.delete', function (e) {
    var selectedRow = $(this).closest('tr');
    var RIdx = table.row(selectedRow).index();
    table.row(selectedRow).remove().draw();
});
$('#MainTableFeeStructure tbody').on('click', '.edit', function (e) {

    startLoading();

    var selectedRow = $(this).closest('tr');
    var RIdx = table.row(selectedRow).index();
    var Amount = table.cell(RIdx, 2).data();
    var FeeTypeId = table.cell(RIdx, 4).data();
    var RevenueAccountId = table.cell(RIdx, 5).data() ?? null;
    var AssetAccountId = table.cell(RIdx, 6).data() ?? null;
    var LiabilityAccountId = table.cell(RIdx, 7).data() ?? null;
    var CostOfSaleAccountId = table.cell(RIdx, 8).data() ?? null;
    $('#TextBoxAmount').val(Amount);
    $('#DropDownListFeeType').val(FeeTypeId).change();
    setTimeout(function () {
        // Code to run after timeout
        $('#DropDownListRevenueAccount').val(RevenueAccountId).change();
        $('#DropDownListAssetAccount').val(AssetAccountId).change();
        $('#DropDownListLiabilityAccount').val(LiabilityAccountId).change();
        $('#DropDownListCostOfSaleAccount').val(CostOfSaleAccountId).change();

        // Call the function during the timeout period
    }, 400); //
    stopLoading();

});
function CalcBoxDataTable() {


    var TotalFeeExclusiveAmount = table.column(2).data().reduce(function (a, b) {
        return parseFloat(a) + parseFloat(b);
    }, 0);
   

    var TaxAmount = 0;
    switch (WH_IsOnExceedingAmount) {
        case true:
            if (TotalFeeExclusiveAmount > WH_SlabAmount) {

                var ExceedingAmount = parseFloat(TotalFeeExclusiveAmount - WH_SlabAmount);
                TaxAmount = parseFloat(WH_FixedCharges + ((ExceedingAmount / 100) * WH_Percentage));
            }
            else {
                TaxAmount = 0.00;
            }
            break;
        default:
            TaxAmount = parseFloat(WH_FixedCharges + ((TotalFeeExclusiveAmount / 100) * WH_Percentage));
            break;
    }
    
    var TotalFeeAmount = TaxAmount + TotalFeeExclusiveAmount;

    $('#TDTotalFeeExclusiveAmount').text(TotalFeeExclusiveAmount.toFixed(2));
    $('#TDWHTAmount').text(TaxAmount.toFixed(2));
    $('#TDTotalFeeAmount').text(TotalFeeAmount.toFixed(2));
}
function ClearOtherFeeSetting() {
    $('#DropDownListRevenueAccount').val('-1').change();
    $('#DropDownListAssetAccount').val('-1').change();
    $('#DropDownListLiabilityAccount').val('-1').change();
    $('#DropDownListCostOfSaleAccount').val('-1').change();
}

function PopulateDropDownLists() {
    PopulateMT_GeneralBranch_ListByParam();
    PopulateMT_StructureFeeType_ListByParam();
    PopulateLK_WHTaxPolicy_List();
    PopulateMT_Account_REV_List();
    PopulateMT_Account_AST_List();
    PopulateMT_Account_LIAB_List();
    PopulateMT_Account_COS_List();
}

//-----------ALL CHANGE CASES
function ChangeCase() {

    $('#DropDownListCampus').change(function (event) {
        event.preventDefault();
        $('#DropDownListSession').val('-1').change();
        PopulateMT_AppSession_ListByParam();
        if (DB_OperationType == PARAMETER.DB_OperationType.UPDATE) {
            GET_ACCFEESTRUCTURE_LISTBYPARAM();
        }
    });
    $('#DropDownListSession').change(function (event) {
        event.preventDefault();
        $('#DropDownListClass').val('-1').change();
        PopulateMT_AppClass_ListByParam();
    });
    $('#DropDownListWHTaxPolicy').change(function (event) {
        event.preventDefault();
        var Selected_WHTaxPolicyId  = $('#DropDownListWHTaxPolicy :selected');
        WH_Percentage               = parseFloat(Selected_WHTaxPolicyId.attr('data-Percentage')) || 0;
        WH_SlabAmount               = parseFloat(Selected_WHTaxPolicyId.attr('data-SlabAmount')) || 0;
        WH_FixedCharges             = parseFloat(Selected_WHTaxPolicyId.attr('data-FixedCharges')) || 0;
        WH_IsOnExceedingAmount      = $('#DropDownListWHTaxPolicy :selected').attr('data-IsOnExceedingAmount') === 'true';
        CalcBoxDataTable();
    });
    $('#DropDownListFeeType').change(function (event) {
        event.preventDefault();
        GET_STRUCTUREFEETYPE_DETAILBYID();
    });

    //-----------FOR ::EDIT CASE
    $('#DropDownListFeeStructure').change(function () {
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
        url: BasePath + "/AAccounts/MFeeUI/GET_MT_GENERALBRANCH_BYPARAMETER",
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
            if (RoleId == Roles.RoleID_ADMIN || RoleId == Roles.RoleID_DEVELOPER) {
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
function PopulateMT_AppSession_ListByParam() {
    var CampusId = $('#DropDownListCampus :selected').val();
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.APPSESSION_BY_GENERALBRANCH,
        CampusId: CampusId,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_MT_APPSESSION_BYPARAMETER",
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
        url: BasePath + "/AAccounts/MFeeUI/GET_MT_APPCLASS_BYPARAMETER",
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
function PopulateLK_WHTaxPolicy_List() {
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.WHTAXPOLICY_LIST,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_LK1_WHTAXPOLICY_BYPARAMTER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option ' +
                    'data-Percentage="' + data[i].Percentage + '" ' + 'data-IsOnExceedingAmount="' + data[i].IsOnExceedingAmount + '" ' +
                    'data-SlabAmount="' + data[i].SlabAmount + '" ' + 'data-FixedCharges="' + data[i].FixedCharges + '" ' +
                    'value="' + data[i].Id + '">' + data[i].Description +
                    '</option>';
            }
            $("#DropDownListWHTaxPolicy").html(s);
        },
        complete: function () {
            stopLoading();
        },

    });
}
function PopulateMT_StructureFeeType_ListByParam() {
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.STRUCTUREFEETYPE_BY_ACADEMICFEE,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_MT_STRUCTUREFEETYPE_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option ' +
                    'data-FeeTypeGuId="' + data[i].GuID + '" ' +
                    'value="' + data[i].Id + '">' + data[i].Description +
                    '</option>';
            }
            $("#DropDownListFeeType").html(s);

        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMT_Account_REV_List() {
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
        CoaCatagoryIds: FILTER.COA_ACCOUNTTYPE.SALES_REVENUES,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListRevenueAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMT_Account_AST_List() {
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
        CoaCatagoryIds: FILTER.COA_ACCOUNTTYPE.ACCOUNTS_RECEIVABLE,

    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListAssetAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMT_Account_LIAB_List() {
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
        CoaCatagoryIds: FILTER.COA_ACCOUNTTYPE.CURRENT_LIABILITIES + "," + FILTER.COA_ACCOUNTTYPE.OTHER_CURRENT_LIABILITIES,

    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListLiabilityAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMT_Account_COS_List() {
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
        CoaCatagoryIds: FILTER.COA_ACCOUNTTYPE.COST_OF_SALES,

    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value=' + data[i].Id + '>' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCostOfSaleAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}

//-----------DB OPERATION CALL
function ValidateInputFields() {

    if ($('#DropDownListCampus').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListSession').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListClass').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListWHTaxPolicy').RequiredDropdown() == false) {
        return false;
    }
    if (!table.data().count()) {
        GetMessageBox("No Fee Structure Has Been Entered", 505);
        return false;
    }
    return true;
}
$('#ButtonSubmitDown').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateInputFields();
    if (IsValid) {
        try {
            OperationType = PARAMETER.DB_OperationType.INSERT;

            UpSertDataIntoDB();
        }
        catch {
            alert(err);
            GetMessageBox(err, 500);
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
            alert(err);
            GetMessageBox(err, 500);
        }
    }
});
function UpSertDataIntoDB() {
    var CampusId = $('#DropDownListCampus :selected').val();
    var SessionId = $('#DropDownListSession :selected').val();
    var ClassId = $('#DropDownListClass :selected').val();
    var WHTaxPolicyId = $('#DropDownListWHTaxPolicy :selected').val();
    var TotalFeeExclusive = $('#TDTotalFeeExclusiveAmount').text();
    var WHTAmount = $('#TDWHTAmount').text();
    var TotalFee = $('#TDTotalFeeAmount').text();
    var FeeStructureGuID = $('#HiddenFieldFeeStructureGuID').val();

    var JsonArg = {
        OperationType: OperationType,
        GuID: FeeStructureGuID,
        CampusId: CampusId,
        SessionId: SessionId,
        ClassId: ClassId,
        WHTaxPolicyId: WHTaxPolicyId,
        TotalFeeExclusive: TotalFeeExclusive,
        WHTAmount: WHTAmount,
        TotalFee: TotalFee,
    }

    var FeeStructureDetailArray = [];
    table.rows().every(function (rowIdx, tableLoop, rowLoop) {
        var FeeAmount = table.cell({ row: rowIdx, column: 2 }).data();
        var FeeTypeId = table.cell({ row: rowIdx, column: 4 }).data();
        var RevenueAccountId = table.cell({ row: rowIdx, column: 5 }).data();
        var AssetAccountId = table.cell({ row: rowIdx, column: 6 }).data();
        var LiabilityAccountId = table.cell({ row: rowIdx, column: 7 }).data();
        var CostOfSaleAccountId = table.cell({ row: rowIdx, column: 8 }).data();

        var JsonArg_FeeStructureDetail = {
            FeeAmount: FeeAmount,
            FeeTypeId: FeeTypeId,
            RevenueAccountId: RevenueAccountId,
            AssetAccountId: AssetAccountId,
            LiabilityAccountId: LiabilityAccountId,
            CostOfSaleAccountId: CostOfSaleAccountId,
        }

        FeeStructureDetailArray.push(JsonArg_FeeStructureDetail);
    });

    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/Update_Insert_AccFeeStructure",
        dataType: 'json',
        data: { 'PostedData': (JsonArg), 'PostedDataDetail': (FeeStructureDetailArray), },
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
    $('.form-control').not('#DropDownListCampus, #DropDownListFeeStructure').val('');
    $('.select2').not('#DropDownListCampus, #DropDownListFeeStructure').val('-1').change();
    $('form').removeClass('Is-Valid');
    table.clear().draw();
}


//-----------LOAD ENTERY RECORD :: FEE TYPE SETTING
function GET_STRUCTUREFEETYPE_DETAILBYID() {
    var FeeTypeId = $('#DropDownListFeeType :selected').attr('data-FeeTypeGuId');
    if (FeeTypeId != null && FeeTypeId != undefined && FeeTypeId != "" && FeeTypeId != "-1") {

        var JsonArg = {
            GuID: FeeTypeId,
        }
        $.ajax({
            type: "POST",
            url: BasePath + "/AAccounts/MFeeUI/GET_MT_STRUCTUREFEETYPE_DETAILBYID",
            dataType: 'json',
            data: { 'PostedData': (JsonArg) },
            beforeSend: function () {
                startLoading();
            },
            success: function (data) {
                if (data.length == undefined) {
                }
                else {
                    ClearOtherFeeSetting();
                    FeeSettingIsSecurity = data[0].IsSecurity;
                    FeeSettingIsRefundable = data[0].IsRefundable;
                    FeeSettingIsDiscount = data[0].IsDiscount;

                    if (FeeSettingIsSecurity == true && FeeSettingIsDiscount == true) {
                        $('#DivDropDownListRevenueAccount').hide();
                        $('#DivDropDownListAssetAccount').show();
                        $('#DivDropDownListLiabilityAccount').show();
                        $('#DivDropDownListCostOfSaleAccount').show();
                    }
                    else if (FeeSettingIsSecurity == false && FeeSettingIsDiscount == true) {
                        $('#DivDropDownListRevenueAccount').show();
                        $('#DivDropDownListAssetAccount').show();
                        $('#DivDropDownListLiabilityAccount').hide();
                        $('#DivDropDownListCostOfSaleAccount').show();
                    }
                    else if (FeeSettingIsSecurity == true && FeeSettingIsDiscount == false) {
                        $('#DivDropDownListRevenueAccount').hide();
                        $('#DivDropDownListAssetAccount').show();
                        $('#DivDropDownListLiabilityAccount').show();
                        $('#DivDropDownListCostOfSaleAccount').hide();
                    }
                    else if (FeeSettingIsSecurity == false && FeeSettingIsDiscount == false) {
                        $('#DivDropDownListRevenueAccount').show();
                        $('#DivDropDownListAssetAccount').show();
                        $('#DivDropDownListLiabilityAccount').hide();
                        $('#DivDropDownListCostOfSaleAccount').hide();
                    }


                }
            },
            complete: function () {
                stopLoading();
            },
        });
    }
    else {
        $('#DivDropDownListRevenueAccount').hide();
        $('#DivDropDownListAssetAccount').hide();
        $('#DivDropDownListLiabilityAccount').hide();
        $('#DivDropDownListCostOfSaleAccount').hide();
    }
}


//-----------LOAD ENTERY RECORD :: EDIT
$('#ButtonSubmitGetInfoForEdit').click(function () {
    startLoading();

    ClearInputFields();
    if ($('#DropDownListFeeStructure').RequiredDropdown() == false) {
        return;
    }
    else {
        GET_ACCFEESTRUCTURE_DETAILBYID();
        GET_ACCFEESTRUCTUREDETAIL_DETAILBYID();
    }
});
function GET_ACCFEESTRUCTURE_LISTBYPARAM() {
    var DB_Condition = PARAMETER.DB_IF_Condition.ACCFEESTRUCTURE_BY_GENERALBRANCH
    var CampusId = $('#DropDownListCampus :selected').val() ?? BranchId;
    var JsonArg = {
        CampusId: CampusId,
        DB_IF_PARAM: DB_Condition,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_MT_ACCFEESTRUCTURE_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option ' +
                    'data-Id="' + data[i].Id + '" ' +
                    'value="' + data[i].GuID + '">' + data[i].Description +
                    '</option>';
            }
            $("#DropDownListFeeStructure").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function GET_ACCFEESTRUCTURE_DETAILBYID() {
    var CampusId = $('#DropDownListCampus :selected').val();

    var FeeStructureId = $('#DropDownListFeeStructure :selected').val();
    if (FeeStructureId != null && FeeStructureId != undefined && FeeStructureId != "" && FeeStructureId != "-1") {

        var JsonArg = {
            CampusId: CampusId,
            GuID: FeeStructureId,
        }
        $.ajax({
            type: "POST",
            url: BasePath + "/AAccounts/MFeeUI/GET_MT_ACCFEESTRUCTURE_DETAILBYID",
            dataType: 'json',
            data: { 'PostedData': (JsonArg) },
            beforeSend: function () {
                startLoading();
            },
            success: function (data) {

                $('#DropDownListSession').val(data[0].SessionId).change();
                setTimeout(function () {
                    $('#DropDownListClass').val(data[0].ClassId).change().prop('disabled', true);
                }, 2000);
                $('#DropDownListWHTaxPolicy').val(data[0].WHTaxPolicyId).change();

                $('#HiddenFieldFeeStructureGuID').val(data[0].GuID);
            },
            complete: function () {
                stopLoading();
            },

        });
    }
}
function GET_ACCFEESTRUCTUREDETAIL_DETAILBYID() {
    var FeeStructureId = $('#DropDownListFeeStructure :selected').attr('data-Id');
    if (FeeStructureId != null && FeeStructureId != undefined && FeeStructureId != "" && FeeStructureId != "-1") {
        var JsonArg = {
            FeeStructureId: FeeStructureId,
            DB_IF_PARAM: PARAMETER.DB_IF_Condition.ACCFEESTRUCTUREDETAIL_GET_INFO_BY_GUID,
        }
        $.ajax({
            type: "POST",
            url: BasePath + "/AAccounts/MFeeUI/GET_MT_ACCFEESTRUCTUREDETAIL_DETAILBYID",
            data: { 'PostedData': (JsonArg) },
            beforeSend: function () {
                startLoading();
            },
            success: function (data) {
                table.clear().draw();

                if (data.length > 0) {
                    for (var i in data) {
                        var row_data = [];
                        row_data[0] = '';
                        row_data[1] = data[i].FeeName;
                        row_data[2] = data[i].FeeAmount.toFixed(2);
                        row_data[3] = GetDeletebtn() + GetEditbtn();
                        row_data[4] = data[i].FeeTypeId;
                        row_data[5] = data[i].RevenueAccountId;
                        row_data[6] = data[i].AssetAccountId;
                        row_data[7] = data[i].LiabilityAccountId;
                        row_data[8] = data[i].CostOfSaleAccountId;

                        table.row.add(row_data);
                    }
                    table.draw();
                }
            },
            complete: function () {
                stopLoading();
            },
        });

    }
}


