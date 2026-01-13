
var Table = "";

$(document).ready(function () {
    PopulateAdvertismentList();
});

function PopulateAdvertismentList() {
    Table = $('#MainTable').DataTable({
        "responsive": true,
        "ordering": false,
        "paging": false,
        "processing": true,
        "ajax": {
            "url": '/Advertisment/PopulateAdvertisments',
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

            { "data": null, "title": "#", "orderable": false, },
            { "data": "Code", "title": "Code" },
            { "data": "Title", "title": "Title" },
            { "data": "StartDate", "title": "Start Date" },
            { "data": "EndDate", "title": "End Date" },
            { "data": "Description", "title": "Description" },
            {
                "data": null, "title": "Status",
                "defaultContent": "",
                "render": function (data, type, full, meta) {
                    return GetStatus(data["Status"]);
                },
            },
            {
                "data": null,
                "title": "ACTIONS",
                "defaultContent": "",
                "render": function (data, type, full, meta) {
                    return GetViewbtn('View Advertisment', '/HiringAndRecruitmentModule/Reports/AdvertismentReportbyCode?AdvertismentCode=' + data["Code"],"print")+
                        GetViewbtn('Apply For Job', '/HiringAndRecruitmentModule/Applicants/CreateResumeForJobTitle?AdvertismentCode=' + data["Code"],"edit")+
                        GetViewbtn('View Applicants List', '/HiringAndRecruitmentModule/Applicants/ApplicantsListForJob?AdvertismentCode=' + data["Code"],"eye");
                }
            },


        ],
    });
    Table.on('order.dt search.dt', function () {
        Table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;

        });
    }).draw();
    toastr.success("Advertisment List Has Been Load Successfully ...!", "Intimation");
    /*for serial No*/
}
