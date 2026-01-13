var ApplicantListTable = "";

$(document).ready(function () {
    PopulateApplicantListTable();
});

function PopulateApplicantListTable() {
    var AdvertismentCode = $('#HiddenFieldAdvertismentCode').val();
    ApplicantListTable = $('#TblApplicantList').DataTable({
        "responsive": true,
        "ordering": false,
        "paging": false,
        "processing": true,
        "ajax": {
            "url": '/Lists/PopulateApplicantsByAdvertismentCode?AdvertismentCode=' + AdvertismentCode,
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            },
            "error": function (xhr, error, thrown) {
                toastr.error("Failed to load Job list.", "Error");
            }
        },
        "columns": [
            { "data": null, "title": "#", "orderable": false },
            { "data": "ADVapp_AdvertismentCode", "title": "Advertisment" },
            { "data": "ADVapp_ApplicantCode", "title": "Applicant Code" },
            { "data": "ADVapp_ApplicantName", "title": "Name" },
            { "data": "ADVapp_Phone", "title": "Phone" },
            { "data": "ADVapp_Email", "title": "Email" },
            {
                "data": null, "title": "Status",
                "defaultContent": "",
                "render": function (data, type, full, meta) {
                    return '<td> <span class="custom-badge status-green">' + data["ADVapp_Status"] +'</span></td>';
                },
            },
            {
                "data": null, "title": "ACTIONS",
                "defaultContent": "",
                "render": function (data, type, full, meta) {
                    return GetViewbtn("View CV", "/HiringAndRecruitmentModule/Reports/CVReportApplicantByCode?AdvertismentCode=" + data["ADVapp_AdvertismentCode"] + "&ApplicantCode=" + data["ADVapp_ApplicantCode"], "print") +
                         GetViewbtn("Schedule Interview", "/HiringAndRecruitmentModule/ViewForms/ScheduleInterView?AdvertismentCode=" + data["ADVapp_AdvertismentCode"] + "&ApplicantCode=" + data["ADVapp_ApplicantCode"], "edit");
                }
            },
            {
                "data": null,
                "title": "CV",
                "render": function (data, type, full, meta) {
                    // Assuming the file path is stored in the "filePath" column
                    var cvFilePath = data["ADVapp_CV_PDF_URL"];
                    if (cvFilePath) {
                        // Return a link/button to view the CV
                        return '<a href="' + cvFilePath + '" target="_blank" class="btn btn-primary">View CV</a>';
                    } else {
                        // If no CV is uploaded, display "N/A" or any other message
                        return "N/A";
                    }
                }
            },
            {
                "data": null, "title": "CV",
                "defaultContent": "",
                "render": function (data, type, full, meta) {
                    var cvFilePath = data["ADVapp_CV_PDF_URL"];
                    return '<a href="' + cvFilePath + '" target="_blank">View CV</a>';
                },
            },
        ],
        "columnDefs": [
            { "visible": false, "targets": 1 } // Hide the AdvertismentCode column initially
        ],
        "rowGroup": {
            "dataSrc": "AdvertismentCode"
        }
    });
    ApplicantListTable.on('order.dt search.dt', function () {
        ApplicantListTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
    toastr.success("Patient List Has Been Loaded Successfully!", "Intimation");
}
