

var MedicalPrescriptionTable;

$(document).ready(function () {
    CreateMedicalPrescriptions();

    var AppointmentCode = $('#HiddenFieldAppointmentCode').val();
    PopulateDataForAppointment(AppointmentCode);
});

function CreateMedicalPrescriptions() {
    MedicalPrescriptionTable = $('#PatientAppointmentMedicalPrescriptionTable').DataTable({
        "responsive": true,
        "ordering": false,
        "paging": false,
        "columns": [
            { "title": "#" },
            { "title": "Prescription Type" },
            { "title": "Medicine / Test Name" },
            { "title": "Course (For Medicines)" },
            { "title": "Recommended Dosage" },
            { "title": "Action(s)" }
        ],
        "order": [[0, 'desc']],
        "columnDefs": [
            {
                "visible": false,
                "targets": [0]
            }
        ],
        "createdRow": function (row, data, index) {
            // Additional logic if needed
            AddEditDeleteButtons(row);
        }
    });

    MedicalPrescriptionTable.on('order.dt search.dt', function () {
        MedicalPrescriptionTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            MedicalPrescriptionTable.cell(i, 0).data(i + 1);
            cell.innerHTML = i + 1;
        });
    }).draw();
}

function AddEditDeleteButtons(row) {
    $(row).find('.delete-btn').click(function () {
        var rowData = MedicalPrescriptionTable.row(row).data();
        MedicalPrescriptionTable.row(row).remove().draw();
        toastr.success("Prescription Deleted");
    });

    $(row).find('.edit-btn').click(function () {
        var rowData = MedicalPrescriptionTable.row(row).data();
        // Perform edit operation using the rowData
        // Display a modal or navigate to a new page for editing
        toastr.info("Edit functionality will be implemented here");
    });
}

$("#AppendMedicalPrescription").click(function (event) {
    try {
        var row_data = [];
        var PrescriptionAdviceType = $('#DropDownPrescriptType :selected').val();
        var MedicineTestName = $('#TextBoxMedicineTestName').val();
        var MedicationPeriod = "";
        var RecommendedDosage = "";
        var NULLABLE = 'Not Applicable';

        if (PrescriptionAdviceType === "Medicine") {
            MedicationPeriod = $('#TextBoxMedicationPeriod').val();
            RecommendedDosage = $('#TextBoxRecomendedDosage').val();
        } else if (PrescriptionAdviceType === "Diagnosis Test") {
            MedicationPeriod = NULLABLE;
            RecommendedDosage = NULLABLE;
        }

        row_data[0] = "";
        row_data[1] = PrescriptionAdviceType;
        row_data[2] = MedicineTestName;
        row_data[3] = MedicationPeriod;
        row_data[4] = RecommendedDosage;
        row_data[5] = "<td style='color: red'><a title='Click here to Delete' class='delete-btn btn btn-sm'><i class='fa fa-remove cross'></i></a></td>" + "<td style='color: red'><a title='Click here to Edit' class='edit-btn btn btn-sm'><i class='fa fa-pencil'></i></a></td>";

        var IsAlreadyExist = false;
        MedicalPrescriptionTable.column(2).data().each(function (value, index) {
            if (value === row_data[2] || value === row_data[1]) {
                IsAlreadyExist = true;
            }
        });

        if (IsAlreadyExist) {
            toastr.warning("This Already Exists", MedicineTestName);
        } else {
            MedicalPrescriptionTable.row.add(row_data).draw();
            toastr.success("Prescription Amended", "Success ...!");
            $('#DivGenerateMedicalPrescription').show();
        }
    } catch (err) {
        // Handle the error if needed
    }
});

$('#DropDownPrescriptType').change(function () {
    var PrescriptType = $('#DropDownPrescriptType :selected').val();
    $('#TextBoxMedicineTestName').val('');
    $('#TextBoxMedicationPeriod').val('');
    $('#TextBoxRecomendedDosage').val('');

    if (PrescriptType === "Diagnosis Test") {
        $('#TextBoxMedicineTestName').prop("disabled", false);
        $('#TextBoxMedicationPeriod').prop("disabled", true);
        $('#TextBoxRecomendedDosage').prop("disabled", true);
    } else if (PrescriptType === "Medicine") {
        $('#TextBoxMedicineTestName').prop("disabled", false);
        $('#TextBoxMedicationPeriod').prop("disabled", false);
        $('#TextBoxRecomendedDosage').prop("disabled", false);
    } else {
        $('#TextBoxMedicineTestName').prop("disabled", true);
        $('#TextBoxMedicationPeriod').prop("disabled", true);
        $('#TextBoxRecomendedDosage').prop("disabled", true);
    }
});

$('#GenerateMedicalPrescription').click(function () {
    var MedicalPrescriptionDetails = [];

    var PatientCode = $("#HiddenFieldPatientCode").val();
    var AppointmentCode = $("#HiddenFieldAppointmentCode").val();
    var AdditionalNote = $("#TextAreaAdditionalRemarks").val();
    var AppointmentStatus = $("#DropDownAppointmentStatus :selected").val();

    var jsonArg = new Object();
    jsonArg.PatientCode = PatientCode;
    jsonArg.AppointmentCode = AppointmentCode;
    jsonArg.AdditionalNote = AdditionalNote;




    MedicalPrescriptionTable.rows().every(function (index, element) {
        var PrescriptionType = this.cell(index, 1).render('display');
        var Test_MedicineName = this.cell(index, 2).render('display');
        var CourseDays = this.cell(index, 3).render('display');
        var Dosage = this.cell(index, 4).render('display');
        var jsonArgMedicalPrescriptionDetails = {
            PrescriptionType: PrescriptionType,
            Test_MedicineName: Test_MedicineName,
            CourseDays: CourseDays,
            Dosage: Dosage
        };
        MedicalPrescriptionDetails.push(jsonArgMedicalPrescriptionDetails);
    });

    $.ajax({
        type: "POST",
        url: '/SaveDataIntoTable/SavePrescriptionReportAgainstAppointment?AppointmentStatus=' + AppointmentStatus,
        data: {
            'MedicalPrescriptionsParent': (jsonArg),
            'MedicalPrescriptionDetails': (MedicalPrescriptionDetails)
        },
        dataType: "json",
        success: function () {
            toastr.success("Prescription Saved");
        },
        error: function () {
            toastr.error("Failed To Save");
        }
    });
    return false;
});



function PopulateDataForAppointment(AppointmentCode) {
    debugger
    $.ajax({

        type: "POST",
        url: "/DropDownList/PopulatePatientNameAndAppointmentCode?AppointmentCode=" + AppointmentCode,
        data: "{}",
        success: function (data) {
            debugger
            for (var i = 0; i < data.length; i++) {
                $(".LKC_CompanyName").text(data[i].LKC_CompanyName);
                $(".LKC_AddressLine1").text(data[i].LKC_AddressLine1);
                $(".LKC_EmailLabel").text(data[i].LKC_Email);
                $(".LKC_ContactLabel").text(data[i].LKC_Contact);
                $(".PA_DepartmentName").text(data[i].PA_DepartmentName);
                $(".PA_AppointmentCode").text(data[i].PA_AppointmentCode);
                $(".PA_DateofAppointment").text(data[i].PA_DateofAppointment);
                $(".PA_TimeofAppointment").text(data[i].PA_TimeofAppointment);
                $(".P_Code").text(data[i].P_Code);
                $(".P_Name").text(data[i].P_Name);
                $(".P_Age").text(data[i].P_Age);
                $(".P_Gender").text(data[i].P_Gender);
                $(".P_Phone").text(data[i].P_Phone);
                $(".P_Email").text(data[i].P_Email);
                $(".P_CityName").text(data[i].P_CityName);
                $(".P_Address").text(data[i].P_Address);
            }
        },
    });
}


