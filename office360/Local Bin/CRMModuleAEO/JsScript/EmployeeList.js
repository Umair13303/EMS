
var EmployeeListTable = "";

$(document).ready(function () {
    PopulateEmployeeListTable();

});

function PopulateEmployeeListTable() {
    EmployeeListTable = $('#TblEmployeeList').DataTable({
        "responsive": true,
        "ordering": false,
        "paging": false,
        "processing": true,
        "ajax": {
            "url": '/Lists/PopulateEmployeeList',
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
            { "data": "E_Code", "title": "Code" },
            { "data": "E_EmployeeName", "title": "Name" },
            { "data": "E_Cnic", "title": "Cnic" },
            { "data": "E_Phone", "title": "Phone" },
            { "data": "E_BranchName", "title": "Branch" },
            { "data": "E_DepartmentName", "title": "Department" },
            {
                "data": null, "title": "Status",
                "defaultContent": "",
                "render": function (data, type, full, meta) {
                    var Status = data["E_Status"];
                    if (Status == 4) {
                        return '<td> <span class="custom-badge status-green">Serving</span></td>';
                    }
                    else {
                        return '<td> <span class="custom-badge status-red">Dis-Continued</span></td>';
                    }
                },
            },
            {
                "data": null, "title": "ACTIONS",
                "defaultContent": "",
                "render": function (data, type, full, meta) {
                    var Code = data["E_Code"];
                    return '<a id ="GenderateEmployeeRegistrationReport' + Code + '" title = "Click here to View EmployeeReport" class="view text-primary btn btn-lg"  href = "/CRMModuleAEO/Reports/EmployeeReport?Code=' + Code + '" > <i class="fa fa-print" aria-hidden="true"></i></a ></span >';


                }
            },


        ],
    });
    EmployeeListTable.on('order.dt search.dt', function () {
        EmployeeListTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;

        });
    }).draw();
    toastr.success("Patient List Has Been Load Successfully ...!", "Intimation");
    /*for serial No*/
}
