var table = "";

$(document).ready(function () {
    ChangeCase();
    InitDateTable();
});

function ChangeCase() {
    $('#DropDownListSearchBy').change(function (event) {
        $('#TextBoxQueryString').val('');
        event.preventDefault();
        var SearchBy = $('#DropDownListSearchBy :selected').val();
        if (SearchBy == 1) {
            $('#TextBoxQueryString').prop('disabled', true);
        }
        else {
            $('#TextBoxQueryString').prop('disabled', false);
        }
    });
}

//-----------ALL DATA TABLE LIST
function InitDateTable() {
    var ParentGroupColumn = 10;
    var SubGroupMenu = 3;


    table = $('#MainTableFeeStructure').DataTable({
         dom: '<"row"<"col-md-12"<"row"<"col-md-6"B><"col-md-6"f> > ><"col-md-12"rt> <"col-md-12"<"row"<"col-md-5"i><"col-md-7"p>>> >',
         buttons: {
            buttons: [
                { extend: 'copy', className: 'btn' },
                { extend: 'csv', className: 'btn' },
                { extend: 'excel', className: 'btn' },
                { extend: 'print', className: 'btn' }
            ]
        },
        "oLanguage": {
            "oPaginate": {
                "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>',
                "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>'
            },
            "sInfo": "Showing page _PAGE_ of _PAGES_",
            "sSearch": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-search"><circle cx="11" cy="11" r="8"></circle><line x1="21" y1="21" x2="16.65" y2="16.65"></line></svg>',
            "sSearchPlaceholder": "Search...",
            "sLengthMenu": "Results :  _MENU_"
        },
        "responsive": true,
        "ordering": true,
        "processing": true,
        "columns": [
            { "data": null, "title": "#" },
            { "data": "Code", "title": "Code" },
            { "data": "Session", "title": "Session", },
            { "data": "Class", "title": "Class" },
            { "data": "WHTaxPolicy", "title": "WH Tax Policy" },
            { "data": "TotalFee", "title": "Total Fee" },
            {
                "data": null, "title": "Status", "defaultContent": "",
                "render": function (data, type, full, meta) {
                    return GetStatus(data["DocumentStatus"]);
                }
            },
            {
                "data": null, "title": "Action(s)", "defaultContent": "",
                "render": function (data, type, full, meta) {
                    return GetViewbtn("AccFeeStructureUpdateStatus('" + data["GuID"] + "')", "Fee Structure", "Edit");
                }
            },
            { "data": "Id", "title": "Id" },
            { "data": "GuID", "title": "GuID" },
            { "data": "Campus", "title": "Campus" },

        ],
        columnDefs: [
            { visible: false, targets: [3, 8, 9,10] },
            { "orderable": false, targets: [0, 1, 2, 4, 5, 6, 7, 8, 9] }
        ],
        order: [[SubGroupMenu, 'asc']],
        displayLength: 10,
        drawCallback: function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            //LAST GROUPING VARIABLES
            var LastCampus = null;

            var LastClass = null;

            rows.each(function (row, index) {

                var data = table.row(row).data();
                var Campus = data["Campus"];
                var Class = data["Class"];


                if (Campus !== LastCampus) {
                    $(row).before('<tr class="group"><td colspan="11" style="background-color: lightseagreen; color: white;"><b>Campus: ' + Campus + '</b></td></tr>');
                    LastCampus = Campus;
                }

                if (Class !== LastClass) {
                    $(row).before('<tr class="subgroup"><td colspan="11" style="background-color: lightblue; color: white;"><b>Class: ' + Class + '</b></td></tr>');
                    LastClass = Class;
                }
            });
        }
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();/*for serial No*/
}



//-----------DB OPERATION CALL
$('#ButtonSearch').click(function (event) {
    event.preventDefault();
    var IsValid = true;
    if (IsValid) {
        try {
            DrawDataTable();

        }
        catch {
            GetMessageBox(err, 500);
        }
    }
});
function DrawDataTable() {
    var SearchById = $('#DropDownListSearchBy :selected').val();
    var InputText = $('#TextBoxQueryString').val();
    var JsonArg = {
        SearchById: SearchById,
        InputText: InputText,
    };

    var queryString = $.param(JsonArg);
    table.ajax.url((BasePath + "/AAccounts/MFeeUI/GET_MT_ACCFEESTRUCTURE_LIST_BYPARAM_FORDT?" + queryString)).load();
}

function AccFeeStructureUpdateStatus(GuID, UpdateDocumentStatus) {

    var JsonArg = {
        GuID: GuID,
        DocumentStatus: UpdateDocumentStatus
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/Update_Insert_AccFeeStructure",
        dataType: 'json',
        data: { 'PostedData': (JsonArg), },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            GetMessageBox(data.Message, data.Code);

        },
        complete: function () {
            stopLoading();
        },
        error: function (jqXHR, error, errorThrown) {
            GetMessageBox("The Transaction Can Not Be Performed Due To Serve Activity", 500);

        },
    });

}






