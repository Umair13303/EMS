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
            $('#TextBoxQueryString').prop('disabled',true);
        }
        else {
            $('#TextBoxQueryString').prop('disabled',false);
        }
    });
}

//-----------ALL DATA TABLE LIST
function InitDateTable() {
    var ParentGroupColumn = 2;
    
    table = $('#MainTableFeeType').DataTable({
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
            { "data": null,                 "title": "#"                    },
            { "data": "FeeName",            "title": "Fee"                  },
            { "data": "FeeCatagory",        "title": "Catagory"             },
            { "data": "ChargingMethod",     "title": "Method",                  "orderable": true,  },
            { "data": "IsOnAdmission",      "title": "On Admission"         },
            { "data": "IsSecurity",         "title": "Is Security"          },
            { "data": "IsRefundable",       "title": "Is Refundable"        },
            { "data": "IsDiscount", "title": "Is Discountable" },
            {
                "data": null, "title": "Status", "defaultContent": "",
                "render": function (data, type, full, meta) {
                    return GetStatus(data["DocumentStatus"]);
                }
            },
        ],
        columnDefs: [
            { visible: false, targets: ParentGroupColumn },
            { "orderable": false, targets: [0, 1, 2, 4, 5, 6,7] }
        ],
        order: [[ParentGroupColumn, 'asc']],
        displayLength: 10,
        drawCallback: function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var Last_FeeCatagory = null;
            rows.each(function (row, index) {

                var data = table.row(row).data();
                var FeeCatagory = data["FeeCatagory"];
                if (FeeCatagory !== Last_FeeCatagory) {
                    $(row).before('<tr class="group"><td colspan="11" style="background-color: lightseagreen; color: white;"><b>Fee Catagory: ' + FeeCatagory + '</b></td></tr>');
                    Last_FeeCatagory = FeeCatagory;
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
    debugger
    table.ajax.url((BasePath + "/AAccounts/MFeeUI/GET_MT_STRUCTUREFEETYPE_LIST_BYPARAM_FORDT?" + queryString)).load();

   
}







