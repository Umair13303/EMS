var PatientListTable = "";
var AppointmentListTable = "";
$(document).ready(function () {

    $('#DropDownListType').change(function () {
        if (PatientListTable) {
            PatientListTable.destroy();
        }
        if (AppointmentListTable) {
            AppointmentListTable.destroy();
        }
        var ListType = $('#DropDownListType :selected').val();

   
        $('#StartDateForFilterPatients').val('');
        $('#EndDateForFilterPatients').val('');
        
        if (ListType == -1) {
                toastr.warning("Please Choose A List Type", "Warning");
                $('#DivSearchCriteria').hide();
            $('#DivPopulatePatientList').hide();
        }
        if (ListType == 1) {
                toastr.info("Enter Search String To Load Patients List...", "Intimation");
                $('#DivSearchCriteria').show();
                $('#DivPatientAppointmentList').hide();
            $('#DivPopulatePatientList').show();
        }
        if (ListType == 2) {
                toastr.info("Enter Search String To Load Appointment List...", "Intimation");
                $('#DivSearchCriteria').show();
                $('#DivPopulatePatientList').hide();
            $('#DivPatientAppointmentList').show();
        }

        
    });
});


      
$('#ButtonPopulateList').click(function () {

    if (AppointmentListTable) {
        AppointmentListTable.destroy();
    }
        var Start_Date = $('#StartDateForFilterPatients').val();
        var End_Date = $('#EndDateForFilterPatients').val();

        PopulateAppointmentListTable(Start_Date, End_Date);
    });

function PopulateAppointmentListTable(Start_Date, End_Date) {

    AppointmentListTable = $('#PatientAppointmentListTable').DataTable({
        "responsive": true,
        "ordering": false,
        "paging": false,
        "processing": true,
        "ajax": {
            "url": '/List/ViewPatientAppointmentList?StartDate=' + Start_Date + "&EndDate=" + End_Date,
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            },
            "error": function (xhr, error, thrown) {


                toastr.error("Failed to load appointment list.", "Error");
            }
        },
        "columns": [

            { "data": null, "title": "#", "orderable": false, },//0
            { "data": "AppointmentCode", "title": "Appointment Code" },//1
            { "data": "PatientCode", "title": "Patient Code" },//2
            { "data": "PatientName", "title": "PatientName" },//3
            { "data": "DepartmentName", "title": "Department Name" },//4
            { "data": "DoctorName", "title": "Doctor Name" },//5
            { "data": "DepartmentId", "title": "Department Id" },//6
            { "data": "DoctorCode", "title": "Doctor Code" },//7
            { "data": "DateofAppointment", "title": "Appointment Date" },//8
            { "data": "TimeofAppointment", "title": "Appointment Time" },//9
            {
                "data": null, "title": "Status",
                "defaultContent": "",
                "render": function (data, type, full, meta) {

                    var IsAttended = data["IsAttended"];
                    if (IsAttended == 1) {
                        return '<td> <span class="custom-badge status-green">Attended</span></td>';
                    }
                    else {
                        return '<td> <span class="custom-badge status-red">Not Attended</span></td>';
                    }
                },
            },
            {
                "data": null, "title": "Status",
                "defaultContent": "",
                "render": function (data, type, full, meta) {

                    var IsAttended = data["IsAttended"];

                    var AppointmentCode = data["AppointmentCode"];
                    var PatientCode = data["PatientCode"];
                    var AppointmentCode = data["AppointmentCode"];

                    if (IsAttended == 1) {
                        return  '<a id ="ViewMedicalPrescriptionReport" title = "View Prescription Report" class="view text-primary btn btn-lg"  href = "/PatientModuleArea/Reports/MedicalPrescriptionReport?AppointmentCode=' + AppointmentCode + '&PatientCode=' + PatientCode + '" > <i class="fa fa-eye" aria-hidden="true"></i></a ></span >' + 
                         '<a id ="DeleteMedicalPrescription" title = "Delete Prescription" class="view text-primary btn btn-lg"  href = "/PatientModuleArea/FormView/CreateMedicalPrescriptByAppointmentId?AppointmentCode=' + AppointmentCode + '" > <i class="fa fa-trash" aria-hidden="true"></i></a ></span >'+
                         '<a id ="DeleteAppointment" title = "Delete Appointment" class="view text-primary btn btn-lg"  href = "/PatientModuleArea/FormView/CreateMedicalPrescriptByAppointmentId?AppointmentCode=' + AppointmentCode + '" > <i class="fa fa-trash" aria-hidden="true"></i></a ></span >';

                    }
                    else {
                        return '<a id ="CreateMedicalPrescription" title = "Create Prescription" class="view text-primary btn btn-lg"  href = "/PatientModuleArea/FormView/CreateMedicalPrescriptByAppointmentId?AppointmentCode=' + AppointmentCode + '" > <i class="fa fa-edit" aria-hidden="true"></i></a ></span >';

                    }
                },
            },
        ],
        'columnDefs': [
            {
                visible: false,
                targets: [1,2,6,7]

            }
        ],

    });
    AppointmentListTable.on('order.dt search.dt', function () {
        AppointmentListTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
    toastr.success("Patient Appointment List Against Specified String Is Being Loaded ...", "Intimation");
}



    $('#ButtonPopulatePatientList').click(function () {
        debugger
        if (PatientListTable) {
            PatientListTable.destroy();
        }
        var StartDate = $('#StartDateForFilterPatients').val();
        var EndDate = $('#EndDateForFilterPatients').val();
        PopulatePatientListTable(StartDate, EndDate);
    });

function PopulatePatientListTable(StartDate, EndDate) {



    PatientListTable = $('#PatientListTable').DataTable({
        "responsive": true,
        "ordering": false,
        "paging": false,
        "processing": true,
        "ajax": {
            "url": '/List/ViewPatientList?StartDate=' + StartDate + "&EndDate=" + EndDate,
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            },
            "error": function (xhr, error, thrown) {
                toastr.error("Failed to load appointment list.", "Error");
            }
        },
        "columns": [

            { "data": null, "title": "#", "orderable": false, },
            { "data": "Code", "title": "Patient Code" },
            { "data": "Name", "title": "Name" },
            { "data": "Cnic", "title": "Cnic" },
            { "data": "Email", "title": "Email" },
            { "data": "Phone", "title": "Phone No" },
            {
                "data": null, "title": "Status",
                "defaultContent": "",
                "render": function (data, type, full, meta) {
                    var Status = data["IsActive"];
                    if (Status == true) {
                        return '<td> <span class="custom-badge status-green">Active</span></td>';
                    }
                    else {
                        return '<td> <span class="custom-badge status-red">In-Active</span></td>';
                    }
                },
            },
            {
                "data": null, "title": "ACTIONS",
                "defaultContent": "",
                "render": function (data, type, full, meta) {
                    var Code = data["Code"];
                    return '<a id ="GenderatePatientRegistrationReport' + Code + '" title = "Click here to View Patient Registration Report" class="view text-primary btn btn-lg"  href = "/PatientModuleArea/Reports/PatientRegistrationReport?Code=' + Code + '" > <i class="fa fa-print" aria-hidden="true"></i></a ></span >';
                    
                    
                }
            },


        ],
    });
    PatientListTable.on('order.dt search.dt', function () {
        PatientListTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;

        });
    }).draw();
    toastr.success("Patient List Has Been Load Successfully ...!", "Intimation");
    /*for serial No*/
}

function PatientRegistration(Code) {

    debugger
    $.ajax({
        type: "POST",
        url: "/PatientModuleArea/Reports/PatientRegistrationReport?Code=" + Code,
        //xhrFields: { responseType: 'blob' },
        beforeSend: function () {
        },
        success: function (report) {
            windows.replace(report);
            /*OpenReport(response, status, xhr);*/
        },
        complete: function () {
            
        },
        error: function (jqXHR, error, errorThrown) {
            errors(jqXHR, error, errorThrown);
        }

    });
}