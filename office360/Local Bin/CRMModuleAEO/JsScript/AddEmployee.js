$(document).ready(function () {
    MaskingInputFields();
    PopulateCity();
    PopulateBanksList();
    PopulateBranches();
    PopulateDesignations();
    PopulateEmployeeType();
    $('#DropDownListBranch').change(function () {
        var BranchId = $('#DropDownListBranch :selected').val();
        PopulateDepartmentsByBranch(BranchId);
    });
    $('#DropDownBankName').change(function () {

        var BankId = $('#DropDownBankName :selected').val();
        PopulateBankBranchesByBankId(BankId);
    });
});
function PopulateCity() {
    $.ajax({
        type: "POST",
        url: "/PatientModuleArea/DropDownList/PopulateCity",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select An Option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].CityName + '</option>';
            }
            $("#DropDownCity").html(s);
        },
    });
}
function PopulateBanksList() {
    $.ajax({
        type: "POST",
        "url": '/DropDown/PopulateBanksList',
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select An Option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].BankName + '</option>';
            }
            $("#DropDownBankName").html(s);
        },
    });
}
function PopulateBankBranchesByBankId(BankId) {
    $.ajax({
        type: "POST",
        "url": '/DropDown/PopulateBankBranchesByBankId?BankId=' + BankId,
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select An Option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].BranchName + '</option>';
            }
            $("#DropDownBankBranch").html(s);
        },
    });
}
function PopulateBranches() {
    $.ajax({
        type: "POST",
        url: "/DropDown/PopulateBranches",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select An Option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].BranchName + '</option>';
            }
            $("#DropDownListBranch").html(s);
        },
    });
}
function PopulateDesignations() {
    $.ajax({
        type: "POST",
        url: "/DropDown/PopulateDesignations",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select An Option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Designation + '</option>';
            }
            $("#DropDownDesignation").html(s);
        },
    });
}
function PopulateEmployeeType() {
    $.ajax({
        type: "POST",
        url: "/DropDown/PopulateEmployeeTypes",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select An Option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].EmployeeTypes + '</option>';
            }
            $("#DropDownEmployeeType").html(s);
        },
    });
}
function PopulateDepartmentsByBranch(BranchId) {
    $.ajax({
        type: "POST",
        "url": '/DropDown/PopulateDepartmentsByBranchId?BranchId=' + BranchId,
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select An Option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Name + '</option>';
            }
            $("#DropDownDepartment").html(s);
        },
    });
}
function MaskingInputFields() {
    $('#TextBoxCnic').inputmask("mask", { "mask": "99999-9999999-9" });
    $('#TextBoxPhone').inputmask("mask", { "mask": "0999-9999999" });
    $('#TextBoxHome').inputmask("mask", { "mask": "0999-9999999" });
    $('#TextBoxNTNNumber').inputmask("mask", { "mask": "999999-9" });
}
$('#DropDownIncomeTaxStatus').change(function () {
    var IncomeTaxStatus = $('#DropDownIncomeTaxStatus :selected').val();
    $('#TextBoxNTNNumber').val==(null);
    if (IncomeTaxStatus == 0) {
        $('#DivTextBoxNTNNumber').slideUp('Slow')
    }
    else if (IncomeTaxStatus == 1) {
        $('#DivTextBoxNTNNumber').slideDown('Slow')
    }
});
function ValidateEmployeeDetails() {
    var IncomeTaxStatus = $('#DropDownIncomeTaxStatus :selected').val();

    if (!$('#TextBoxEmployeeName').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxFatherHusbandName').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxCnic').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxNationality').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#DropDownMartialStatus').RequiredListItem()) {
        return false;
    }
    if (!$('#DropDownGender').RequiredListItem()) {
        return false;
    }
    if (!$('#TextBoxDOB').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxAge').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxEmail').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxPhone').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxHome').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#DropDownCountry').RequiredListItem()) {
        return false;
    }
    if (!$('#DropDownCity').RequiredListItem()) {
        return false;
    }
    if (!$('#TextBoxAddress').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#DropDownIncomeTaxStatus').RequiredListItem()) {
        return false;
    }
    if (IncomeTaxStatus == 1) {
        if (!$('#TextBoxNTNNumber').RequiredListItem()) {
            return false;
        }
    }
    if (!$('#DropDownBankName').RequiredListItem()) {
        return false;
    }
    if (!$('#DropDownBankBranch').RequiredListItem()) {
        return false;
    }
    if (!$('#TextBoxAccountTitle').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxAccountNumber').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#DropDownListBranch').RequiredListItem()) {
        return false;
    }
    if (!$('#DropDownDepartment').RequiredListItem()) {
        return false;
    }
    if (!$('#DropDownDesignation').RequiredListItem()) {
        return false;
    }
    if (!$('#DropDownEmployeeType').RequiredListItem()) {
        return false;
    }
    if (!$('#TextBoxJoiningDate').RequiredTextBoxInputGroup()) {
        return false;
    }
    return true;

}
$('#SubmitDown').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateEmployeeDetails();
    if (IsValid) {
        try {
            var NTNNumber = null;
            var EmployeeName = $('#TextBoxEmployeeName').val();
            var FatherHusbandName = $('#TextBoxFatherHusbandName').val();
            var Cnic = $('#TextBoxCnic').val();
            var Nationality = $('#TextBoxNationality').val();
            var MartialStatus = $('#DropDownMartialStatus :selected').val();
            var Gender = $('#DropDownGender :selected').val();
            var DOB = $('#TextBoxDOB').val();
            var Age = $('#TextBoxAge').val();
            var Email = $('#TextBoxEmail').val();
            var Phone = $('#TextBoxPhone').val();
            var Home = $('#TextBoxHome').val();
            var CountryId = $('#DropDownCountry :selected').val();
            var CityId = $('#DropDownCity :selected').val();
            var Address = $('#TextBoxAddress').val();
            var IncomeTaxStatus = $('#DropDownIncomeTaxStatus :selected').val();
            var NTNNumber = $('#TextBoxNTNNumber').val();
            var BankNameId = $('#DropDownBankName :selected').val();
            var BankBranchId = $('#DropDownBankBranch :selected').val();
            var AccountTitle = $('#TextBoxAccountTitle').val();
            var AccountNumber = $('#TextBoxAccountNumber').val();
            var BranchId = $('#DropDownListBranch :selected').val();
            var DepartmentId = $('#DropDownDepartment :selected').val();
            var DesignationId = $('#DropDownDesignation :selected').val();
            var EmployeeTypeId = $('#DropDownEmployeeType :selected').val();
            var JoiningDate = $('#TextBoxJoiningDate').val();


            var JsonArg = {
                EmployeeName: EmployeeName,
                FatherHusbandName: FatherHusbandName,
                Cnic: Cnic,
                Nationality: Nationality,
                MartialStatus: MartialStatus,
                Gender: Gender,
                DOB: DOB,
                Age: Age,
                Email: Email,
                Phone: Phone,
                Home: Home,
                CountryId: CountryId,
                CityId: CityId,
                Address: Address,
                IncomeTaxStatus: IncomeTaxStatus,
                NTNNumber: NTNNumber,
                BankNameId: BankNameId,
                BankBranchId: BankBranchId,
                AccountNumber: AccountNumber,
                AccountTitle: AccountTitle,
                BranchId: BranchId,
                DepartmentId: DepartmentId,
                DesignationId: DesignationId,
                EmployeeTypeId: EmployeeTypeId,
                JoiningDate: JoiningDate
            };
            $.ajax({
                type: "POST",
                url: '/SaveDataIntoDb/SaveNewEmployee',
                data: { 'employees': (JsonArg) },
                dataType: "json",
                success: function () {
                    toastr.success(EmployeeName, "Saved As Employee");
                },

                error: function () {
                    toastr.error(EmployeeName, "Failed To Save");
                }
            });
        }
        catch (err) {
            alert(err);
        }
    }

    

});
