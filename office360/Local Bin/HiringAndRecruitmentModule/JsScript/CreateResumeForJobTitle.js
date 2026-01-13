
var EducationalDetailsTable ;
$('#SubmitDown').click(function (event) {
    event.preventDefault();
    debugger
    var IsValid = ValidateForms();
    if (IsValid) {
        try {
            var AdvertismentCode = $('#HiddenFieldAdverstismentCode').val();
var ApplicantName = $('#TextBoxtApplicantName').val();
var Profession = $('#TextBoxProfession').val();
var Address = $('#TextBoxAddress').val();
var Email = $('#TextBoxEmail').val();
var Phone = $('#TextBoxPhone').val();
var Education = $('#TextBoxEducation').val();
var Institute = $('#TextBoxInstitute').val();
var AdmissionDate = $('#TextBoxAdmissionDate').val();
var DegreeCompletionDate = $('#TextBoxDegreeCompletionDate').val();
var DivisionOrGPA = $('#TextBoxDivisionOrGPA').val();
var LastJob = $('#TextBoxLastJob').val();
var JobStartedOn = $('#TextBoxJobStartedOn').val();
var JobEndedOn = $('#TextBoxJobEndedOn').val();
var OtherJobs = $('#TextBoxOtherJobs').val();
var LastEmployer = $('#TextBoxLastEmployer').val();
            var OtherSkills = $('#TextBoxOtherSkills').val();
            var JsonArg = {
                AdvertismentCode: AdvertismentCode,
                ApplicantName: ApplicantName,
                Profession: Profession,
                Address: Address,
                Email: Email,
                Phone: Phone,
                Education: Education,
                Institute: Institute,
                AdmissionDate: AdmissionDate,
                DegreeCompletionDate: DegreeCompletionDate,
                DivisionOrGPA: DivisionOrGPA,
                LastJob: LastJob,
                JobStartedOn: JobStartedOn,
                JobEndedOn: JobEndedOn,
                OtherJobs: OtherJobs,
                LastEmployer: LastEmployer,
                OtherSkills: OtherSkills,
            };
          

            $.ajax({
                type: "POST",
                url: '/Applicants/CreateResumeForJobTitle',
                data: {"HRApplicants" :JsonArg},
                dataType: "json",
                success: function (data) {
                    var StatusCode = data.StatusCode;
                    var Message = data.Message;
                    GetMessageBox(StatusCode, Message)
                    
                },
                error: function (data) {
                    var StatusCode = data.StatusCode;
                    var Message = data.Message;
                    GetMessageBox(StatusCode, Message)
                }
            });
        } catch (err) {
            alert(err);
        }
    }
});
function ValidateForms() {
    if (!$('#TextBoxtApplicantName').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxProfession').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxAddress').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxEmail').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxPhone').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxEducation').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxInstitute').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxAdmissionDate').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxDegreeCompletionDate').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxDivisionOrGPA').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxLastJob').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxJobStartedOn').RequiredTextBoxInputGroup()) {
        isValid = false;
    }

    if (!$('#TextBoxJobEndedOn').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxLastEmployer').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxOtherJobs').RequiredTextBoxInputGroup()) {
        return false;
    }

    if (!$('#TextBoxOtherSkills').RequiredTextBoxInputGroup()) {
        return false;
    }


    return true;
}
function LoadAdvertismentDetails() {
    var advertisementCode = $('#HiddenFieldAdverstismentCode').val();
    var jsonData = {
        AdvertismentCode: advertisementCode
    };

    $.ajax({
        type: "POST",
        url: "/Applicants/GetAdvertismentDetailsByParamters",
        data: { Filters: (jsonData) },
        dataType: "json",
        success: function (response) {
            var data = response.data;
            debugger
            if (data && data.length > 0) {
                $(".AdvertismentType").text(data[0].Type);
                $(".JobDescription").text(data[0].Description);
                $(".StartDate").text(data[0].StartDate);
                $(".EndDate").text(data[0].EndDate);
                $(".Status").text(data[0].Status);
            }
        },
    });
}
$(document).ready(function () {
    LoadAdvertismentDetails();
    EducationTable();
    $("#EducationButtonPlus").click(function () {
        var IsValid = ValidateEducationalDetail();
        if (IsValid) {
            debugger
            try {
                var row_data = [];
                var Degree = $('#TextBoxDegree').val();
                var StartDate = $('#TextBoxStartDate').val();
                var EndDate = $('#TextBoxEndDate').val();
                var Institute = $('#TextBoxInstitute').val();
                var GPA_Percentage = $('#TextBoxGPA_Percentage').val();

                row_data[0] = "";
                row_data[1] = Degree;
                row_data[2] = StartDate;
                row_data[3] = EndDate;
                row_data[4] = Institute;
                row_data[5] = GPA_Percentage;

                EducationalDetailsTable.row.add(row_data).draw();
            }
            catch (err) {
                alert(err);
            }
        }






    });

});

function EducationTable() {
    EducationalDetailsTable = $('#EducationDetailTable').DataTable({
        "responsive": true,
        "ordering": false,
        "createdRow": function (row, data, index) {
            AddRow(row);
        },
        "paging": false,
        "columns": [
            { "title": "#" },
            { "title": "Degree" },
            { "title": "Start Date" },
            { "title": "End Date" },
            { "title": "Institute" },
            { "title": "GPA / %" },
        ],
        "order": [[0, 'desc']],
        'columnDefs': [
        ],
    });

    EducationalDetailsTable.on('order.dt search.dt', function () {
        EducationalDetailsTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            EducationalDetailsTable.cell(i, 0).data(i + 1);/*for Counter*/
            cell.innerHTML = i + 1;
        });
    }).draw();/*for serial No*/




}
function ValidateEducationalDetail() {
    return true;
}




