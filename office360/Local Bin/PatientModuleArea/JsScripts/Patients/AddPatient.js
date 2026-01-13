$(document).ready(function () {

    PoplateCity();
    $('#TextBoxCnic').inputmask("mask", { "mask": "99999-9999999-9" });
    $('#TextBoxPhone').inputmask("mask", { "mask": "0399-9999999" });

    $('#CheckBoxIsPatientActive').change(function () {
        var IsPatientActive = $("#CheckBoxIsPatientActive").prop('checked');
        $('#TextBoxDisctontinuationDate').val('');
        $('#DropDownReasonToInActive').val('');

        if (IsPatientActive == 1) {
            $('#DivTextBoxDisctontinuationDate').hide();
            $('#DivDropDownReasonToInActive').hide();
        }
        else {
            $('#DivTextBoxDisctontinuationDate').show('fast');
            $('#DivDropDownReasonToInActive').show('fast');
        }
    })
});
function PoplateCity() {
    $.ajax({
        type: "POST",
        url: "/DropDownList/PopulateCity",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">SELECT  CITY</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].CityName + '</option>';
            }
            $("#DropDownCity").html(s);
        },
    });
}


$("#Submit").click(function() {

    var DiscontinuationDate = null;
    var ReasonToInActive = null;        
    var Name = $("#TextBoxName").val();
    var FatherHusbandName = $("#TextBoxFatherHusbandName").val();
    var Cnic = $("#TextBoxCnic").val();
    var Gender = $("#DropDownGender :selected").val();
    var Phone = $("#TextBoxPhone").val();
    var DOB = $("#TextBoxDOB").val();
    var Address = $("#TextBoxAddress").val();
    var Age = $("#TextBoxAge").val();
    var Email = $("#TextBoxEmail").val();
    var UserName = $("#TextBoxUserName").val();
    var CityId = $("#DropDownCity :selected").val();
    var CountryId = $("#DropDownCountry").val();
    var IsPatientActive = $("#CheckBoxIsPatientActive").prop('checked');
    if (IsPatientActive == 0) {
        DiscontinuationDate = $("#TextBoxDisctontinuationDate").val();
        ReasonToInActive = $("#DropDownReasonToInActive :selected").val();
    }
    else {
        DiscontinuationDate = null;
        ReasonToInActive = null;
    }
    var jsonArg = new Object();
    jsonArg.Name = Name;
    jsonArg.FatherHusbandName = FatherHusbandName;
    jsonArg.Cnic = Cnic;
    jsonArg.Gender = Gender;
    jsonArg.Phone = Phone;
    jsonArg.DOB = DOB;
    jsonArg.Address = Address;
    jsonArg.Age = Age;
    jsonArg.Email = Email;
    jsonArg.UserName = UserName;
    jsonArg.CityId = CityId;
    jsonArg.CountryId = CountryId;
    jsonArg.IsActive = IsPatientActive;
    jsonArg.DiscontinuationDate = DiscontinuationDate;
    jsonArg.ReasonToInActive = ReasonToInActive;
    $.ajax({
        type: "POST",
        url: '/SaveDataIntoTable/SaveNewPatient',
        data: { 'PatientsDb': (jsonArg) },
        dataType: "json",
        success: function () {
            toastr.success(Name, "Patient Created");
            ClearInputFields();
        },

        error: function () {
            toastr.error(Name, "Failed To Save");
            //alert.error(Name, "Error Saving");
        }
    });
    return false;
});


function ClearInputFields() {
    $("#TextBoxName").val('');
    $("#TextBoxFatherHusbandName").val('');
    $("#TextBoxCnic").val('');
    $("#DropDownGender :selected").val('');
    $("#TextBoxPhone").val('');
    $("#TextBoxDOB").val('');
    $("#TextBoxAddress").val('');
    $("#TextBoxAge").val('');
    $("#TextBoxEmail").val('');
    $("#TextBoxUserName").val('');
    $("#DropDownCity").val('-1');
    $("#DropDownCountry").val('');
    $("#CheckBoxIsPatientActive").prop('checked', true).trigger('change');

}

function calculateAge() {

    var dob = new Date(document.getElementById("TextBoxDOB").value);
    var today = new Date();
    var yearsDiff = today.getFullYear() - dob.getFullYear();
    var monthsDiff = today.getMonth() - dob.getMonth();
    var daysDiff = today.getDate() - dob.getDate();

    if (monthsDiff < 0 || (monthsDiff === 0 && daysDiff < 0)) {
        yearsDiff--;
    monthsDiff += 12;
      }

    document.getElementById("TextBoxAge").value = yearsDiff + " years " + monthsDiff + " months";
}
