var InterViewList;

$(document).ready(function () {
    PopulateInterViewListTable();
});

function PopulateInterViewListTable() {
    InterViewList = $('#TblInterviewList').DataTable({
        "responsive": true,
        "ordering": false,
        "paging": false,
        "processing": true,
        "ajax": {
            "url": '/Lists/PopulateInterViewList',
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
            

            //{ "data": null, "title": GetChecBoxAll() },
            {
                "data": null, "title": "",
                "defaultContent": "",
                "render": function (data, type, full, meta) {
                    return GetChecBox();
                }
            },
            { "data": "HRI_InterViewCode", "title": "I-No" },
            { "data": "ADVapp_ApplicantCode", "title": "Applicant No" },
            { "data": "ADVapp_ApplicantName", "title": "Name" },
            { "data": "HRI_AdvertismentCode", "title": "Advertisment" },
            { "data": "HRI_InterviewDate", "title": "Date" },
            { "data": "HRI_InterViewTime", "title": "Time" },
            {
                "data": null, "title": "Status",
                "defaultContent": "",
                "render": function (data, type, full, meta) {
                        return '<td> <span class="custom-badge status-green">' + data["HRI_Status"]+'</span></td>';
                },
            },
            {
                "data": null, "title": "ACTIONS",
                "defaultContent": "",
                "render": function (data, type, full, meta) {
                    var InterViewCode = data["HRI_InterViewCode"];
                    return '<a id="InterViewAssessmentCode' + InterViewCode + '" title="Click here for Interview Assessment" class="view text-primary btn btn-lg" href="/HiringAndRecruitmentModule/ViewForms/InterViewAssessment?Code=' + InterViewCode + '"><i class="fa fa-edit" aria-hidden="true"></i></a>';
                    //return '<a id="ApproveOrReject' + InterViewCode + '" title="Click here to Approve Or Reject" class="view text-primary btn btn-lg" href="/HiringAndRecruitmentModule/SaveDataIntoDbController/ApprovalOrRejection?Code=' + InterViewCode + '"><i class="fa fa-edit" aria-hidden="true"></i></a>';
                }
            },
        ],
    });
    InterViewList.on('order.dt search.dt', function () {
        InterViewList.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
    toastr.success("Inter-View List Has Been Loaded Successfully!", "Intimation");

    $('#TblInterviewList tbody').on('click', '.Selected', function () {
        $('#DivDecision').show();
    });
}


$('#SubmitDownApproveHiring').click(function () {   
    var HRInterviews = []; // Corrected the variable name here
    InterViewList.$('input[type="checkbox"]:checked').each(function () {
        var $row = $(this).closest('tr');
        var idx = InterViewList.row($row).index();

        var isChecked = $row.find('td:eq(1) input[type="checkbox"]').prop('checked');
        var InterViewCode = InterViewList.cell({ row: idx, column: 2 }).data();
        var ApplicantCode = InterViewList.cell({ row: idx, column: 3 }).data();
        var AdvertismentCode = InterViewList.cell({ row: idx, column: 5 }).data();



        var jsonArgInterViewList = {
            Status: isChecked ? 6 : 7,
            InterViewCode: InterViewCode,
            ApplicantCode: ApplicantCode,
            AdvertismentCode: AdvertismentCode
        };
        HRInterviews.push(jsonArgInterViewList);
    });
    $.ajax({
        type: "POST",
        url: '/SaveDataIntoDb/UpdateHRMApplicantInterviews',
        data: { 'HRInterviews': HRInterviews }, // Use the correct variable here
        dataType: "json",
        success: function () {
            toastr.success("Saved As Employee");
            InterViewList.ajax.reload();

        },
        error: function () {
            toastr.error("Failed To Save");
        }
    });
});
$('#SubmitDownRejectHiring').click(function () {
    var HRInterviews = []; // Corrected the variable name here
    InterViewList.$('input[type="checkbox"]:checked').each(function () {
        var $row = $(this).closest('tr');
        var idx = InterViewList.row($row).index();

        var isChecked = $row.find('td:eq(1) input[type="checkbox"]').prop('checked');
        var InterViewCode = InterViewList.cell({ row: idx, column: 2 }).data();
        var ApplicantCode = InterViewList.cell({ row: idx, column: 3 }).data();
        var AdvertismentCode = InterViewList.cell({ row: idx, column: 5 }).data();



        var jsonArgInterViewList = {
            Status: isChecked ? 7 : 6,
            InterViewCode: InterViewCode,
            ApplicantCode: ApplicantCode,
            AdvertismentCode: AdvertismentCode
        };
        HRInterviews.push(jsonArgInterViewList);
    });
    $.ajax({
        type: "POST",
        url: '/SaveDataIntoDb/UpdateHRMApplicantInterviews',
        data: { 'HRInterviews': HRInterviews }, // Use the correct variable here
        dataType: "json",
        success: function () {
            debugger
            toastr.success("Saved As Employee");
            InterViewList.ajax.reload();
        },
        error: function () {
            toastr.error("Failed To Save");
        }
    });
});