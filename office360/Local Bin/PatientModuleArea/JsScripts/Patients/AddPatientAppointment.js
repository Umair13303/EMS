    $(document).ready(function () {
    $('#TextBoxPatientPhoneNo').inputmask("mask", { "mask": "0399-9999999" });
    PopulatePatientName();
    PopulateDepartment();
        $('#DropDownPatientName').change(function () {
            var PatientCode = $('#DropDownPatientName :selected').val()
            PopulatePatientDetails(PatientCode);
        });
    PopulateDoctors();
});
function PopulatePatientName() {
    $.ajax({

        type: "POST",
        url: "/DropDownList/PoplatePatientName",
        data: "{}",

        success: function (data) {
            var s = '<option  value="-1">Select Patient Name From List Below</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Code + '">' + data[i].Name + ' # ( ' + data[i].Code +' )' +'' + '</option>';

            }
            $("#DropDownPatientName").html(s);
        },
    });
}
function PopulatePatientDetails(PatientCode) {
    $.ajax({

        type: "POST",
        url: "/DropDownList/PopulatePatientDetailsByCode?Code=" + PatientCode,
        data: "{}",

        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#TextBoxPatientEmail").val(data[i].P_Email);
                $("#TextBoxPatientPhoneNo").val(data[i].P_Phone);
            }
        },
    });
}
function PopulateDepartment() {
    $.ajax({

        type: "POST",
        url: "/DropDownList/PopulateDepartment",
        data: "{}",

        success: function (data) {
            var s = '<option  value="-1">Select Patient Name From List Below</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Name + '</option>';

            }
            $("#DropDownDepartmentList").html(s);
        },
    });
}
function PopulateDoctors() {
    $.ajax({

        type: "POST",
        url: "/DropDownList/PopulateDoctorsByCompanyId",
        data: "{}",

        success: function (data) {
            var s = '<option  value="-1">Select Doctor Name From List Below</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].E_Code + '">' + data[i].E_EmployeeName + '</option>';

            }
            $("#DropDownDoctorList").html(s);
        },
    });
}
$("#SaveNewAppointment").click(function () {
    var PatientCode = $('#DropDownPatientName :selected').val();
    var DepartmentId = $('#DropDownDepartmentList :selected').val();
    var DoctorCode = $('#DropDownDoctorList :selected').val();
    var DateofAppointment = $('#TextBoxDateofAppointment').val();
    var TimeofAppointment = $('#TextBoxTimeofAppointment').val();
    var PatientEmail = $('#TextBoxPatientEmail').val();
    var PatientPhone = $('#TextBoxPatientPhoneNo').val();
    var Message = $('#TextAreaMessage').val();
    var IsAttended = $('#CheckBoxAppointmentStatus').prop('checked');
    var Type = $('#DropDownAppointmentType :selected').text();
    
    var jsonArg = new Object();

    jsonArg.DepartmentId = DepartmentId;
    jsonArg.PatientCode = PatientCode;
    jsonArg.DoctorCode = DoctorCode;
    jsonArg.DateofAppointment = DateofAppointment;
    jsonArg.TimeofAppointment = TimeofAppointment;
    jsonArg.PatientEmail = PatientEmail;
    jsonArg.PatientPhone = PatientPhone;
    jsonArg.Message = Message;
    jsonArg.IsAttended = IsAttended;
    jsonArg.Type = Type;
    $.ajax({
        type: "POST",
        url: '/SaveDataIntoTable/SaveNewPatientAppointment',
        data: { 'PatientAppointment': (jsonArg) },
        dataType: "json",
        success: function () {
            toastr.success(PatientCode, "Appointment Created");

        },

        error: function () {
            toastr.error(PatientCode, "Failed To Create Appointment ");
            //alert.error(Name, "Error Saving");
        }
    });
    return false;
});

