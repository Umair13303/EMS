var Table = "";
$(document).ready(function () {
    PopulateEventListTable();
});
//POPULATE DATATABLE 
function PopulateEventListTable() {

    Table = $('#EventListTable').DataTable({
        "responsive": true,
        "ordering": false,
        "paging": false,
        "processing": true,
        "ajax": {
            "url": '/Lists/AEOEventsList',
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            },
            "error": function (xhr, error, thrown) {
                toastr.error("Failed to Load Existing Data.", "Error");
            }
        },
        "columns": [

            { "data": null, "title": "#", "orderable": false, },//0
            { "data": "Code", "title": "Code" }, //1
            { "data": "EventName", "title": "Event Name" }, //2
            { "data": "Venue", "title": "Venue Name" }, //3
            { "data": "StartDate", "title": "Start Date" }, //4
            { "data": "EndDate", "title": "End Date" }, //4
            {
                "data": null, "title": "Status",
                "defaultContent": "",
                "render": function (data, type, full, meta) {

                    var IsActive = data["IsActive"];
                    if (IsActive == 1) {
                        return '<td> <span class="custom-badge status-green">Exibition Allowed</span></td>';
                    }
                    else {
                        return '<td> <span class="custom-badge status-red">Exibition Ended</span></td>';
                    }
                },
            },
            {
                "data": null, "title": "Action(s)",
                "defaultContent": "",
                "render": function (data, type, full, meta) {

                    var Code = data["Code"];
                    return '<a id ="AddEventUsers' + Code + '" title = "Click here to Add Users For The Event" class="view text-primary btn btn-lg"  href = "/CRMModuleAEO/ViewForms/AEOAssementSetupAssignUsers?Code=' + Code + '" > <i class="fa fa-eye" aria-hidden="true"></i></a ></span >';

                },
            },
        ],
        'columnDefs': [
            {

            }
        ],

    });
    Table.on('order.dt search.dt', function () {
        Table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
    toastr.success("Loading Existing Data ...", "Intimation");
}
//SAVE DATA INTO TABLE
$('#SaveNewEvent').click(function (event) {
    var EventName = $('#TextBoxEventName').val();
    var StartDate = $('#TextBoxStartDate').val();
    var EndDate = $('#TextBoxEndDate').val();
    var Venue = $('#TextBoxEventVenue').val();
    var jsonArg = new Object();
    jsonArg.EventName = EventName;
    jsonArg.StartDate = StartDate;
    jsonArg.EndDate = EndDate;
    jsonArg.Venue = Venue;
    debugger
    $.ajax({

        type: "POST",
        url: '/CRMModuleAEO/SaveDataIntoDb/SaveNewPatient',
        data: { 'AEO_Events': (jsonArg) },
        dataType: "json",
        success: function () {
            toastr.success(EventName, "Event Saved Successfully");
            ClearInputFields();
            if (Table) {
                Table.destroy();
            }
            PopulateEventListTable();
        },

        error: function () {
            toastr.error(EventName, "Failed To Save Event");
        }
    });
    return false;

});
//CLEAR INPUT FIELDS
function ClearInputFields() {
    $('#TextBoxEventName').val('');
    $('#TextBoxStartDate').val('');
    $('#TextBoxEndDate').val('');
    $('#TextBoxEventVenue').val('');
}
