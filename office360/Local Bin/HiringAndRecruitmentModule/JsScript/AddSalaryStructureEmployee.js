$(document).ready(function () {
    debugger
    var Code = $('#HiddenFieldEmployeeCode').val();
    LoadEmployeeDetailsByCode(Code);
});
function LoadEmployeeDetailsByCode(Code) {
    debugger
    $.ajax({
        type: "POST",
        url: "/DropDown/PopulateEmployeeByCode?Code=" + Code,
        data: "{}",
        success: function (data) {
            debugger
            for (var i = 0; i < data.length; i++) {
                $(".EmployeeName").text(data[i].E_EmployeeName);
                $(".EmployeeDesignation").text(data[i].E_Designation);
            }
        }
    });
}

function ValidateSalaryStructure() {
    if (!$('#TextBoxBasicSalary').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxEffectiveFrom').RequiredTextBoxInputGroup()) {
        return false;
    }
    //if (!$('#TextBoxExpiryDate').RequiredTextBoxInputGroup()) {
    //    return false;
    //}
    return true;
}

$('#SubmitDown').click(function (event) {
    debugger
    event.preventDefault();
    var IsValid = ValidateSalaryStructure();
    if (IsValid) {
        debugger
        var EmployeeCode = $('#HiddenFieldEmployeeCode').val();
        var BasicSalary = $('#TextBoxBasicSalary').val();
        var EffectiveFrom = $('#TextBoxEffectiveFrom').val();
        //var ExpiryDate = $('#TextBoxExpiryDate').val();
        var JsonArg = {
            EmployeeCode: EmployeeCode,
            BasicSalary: BasicSalary,
            EffectiveFrom: EffectiveFrom
            ExpiryDate: ExpiryDate
        }
        $.ajax({
            type: "POST",
            url: '/SaveDataIntoDb/SaveNewInterViewScheduleForApplicant',
            data: { 'HRMSalaryStructure': (JsonArg) },
            dataType: "json",
            success: function () {
                toastr.success("Success", "Saved Successfully");
            },

            error: function () {
                toastr.error("Error", "Failed To Save");
            }
        });

        try {

        }
        catch (err) {
            alert(err);
        }
    }
});