var OperationType = "";
var DB_OperationType = $('#HiddenFieldDB_OperationType').val();
var IsFieldClear = false;

$(document).ready(function () {
    ChangeCase();

    switch (DB_OperationType) {
        case PARAMETER.DB_OperationType.INSERT:
            $('#DivButtonSubmitDown').show();
            $('#DivButtonUpdateDown').hide();
            break;
        case PARAMETER.DB_OperationType.UPDATE:
            GET_STRUCTUREDISCOUNTTYPE_LISTBYPARAM();
            $('#DivButtonSubmitDown').hide();
            $('#DivButtonUpdateDown').show();
            break;
    }
});


//-----------ALL CHANGE CASES
function ChangeCase() {
    //-----------FOR ::EDIT CASE
    $('#DropDownListDiscountType').change(function () {
        if (!IsFieldClear) {
            IsFieldClear = true;
            ClearInputFields();
            IsFieldClear = false;
        }
    });


}

//-----------DB OPERATION CALL
function ValidateInputFields() {

    if ($('#TextBoxCode').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxDescription').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxRemarks').RequiredTextBoxInputGroup() == false) {
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
    var Code            = $('#TextBoxCode').val();
    var Description     = $('#TextBoxDescription').val();
    var Remarks         = $('#TextBoxRemarks').val();

    var DiscountTypeGUID = $('#HiddenFieldDiscountTypeGUID').val();

    var JsonArg = {
        GuID: DiscountTypeGUID,
        OperationType: OperationType,
        Code: Code,
        Description: Description,
        Remarks: Remarks,
    }
      
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MDiscountUI/Update_Insert_AccDiscountType",
        dataType: 'json',
        data: {
            'PostedData': (JsonArg), 
        },
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
    $('.form-control').not('#DropDownListDiscountType').val('');
    $('.select2').not('#DropDownListDiscountType').val('-1').change();
    $('form').removeClass('Is-Valid');
}




//-----------LOAD ENTERY RECORD :: EDIT
$('#ButtonSubmitGetInfoForEdit').click(function () {
    ClearInputFields();
    if ($('#DropDownListDiscountType').RequiredDropdown() == false) {
        return;
    }
    else {
        GET_STRUCTUREDISCOUNTTYPE_DETAILBYID();
    }
});

function GET_STRUCTUREDISCOUNTTYPE_LISTBYPARAM() {
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.STRUCTUREDISCOUNTTYPE_LIST,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MDiscountUI/GET_MT_STRUCTUREDISCOUNTTYPE_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].GuID + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListDiscountType").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}

function GET_STRUCTUREDISCOUNTTYPE_DETAILBYID() {
    var DiscountTypeId = $('#DropDownListDiscountType :selected').val();

    var JsonArg = {
        GuID: DiscountTypeId,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MDiscountUI/GET_MT_STRUCTUREDISCOUNTTYPE_DETAILBYID",
        dataType: 'json',
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            $('#TextBoxCode').val(data[0].Code);
            $('#TextBoxDescription').val(data[0].Description);
            $('#TextBoxRemarks').val(data[0].Remarks);
            $('#HiddenFieldDiscountTypeGUID').val(data[0].GuID);
        },
        complete: function () {
            stopLoading();
        },
    });
}


