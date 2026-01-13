var table;

$(document).ready(function () {
    table = $('#maintable').DataTable({
        "responsive": true,
        "ordering": false,
        "createdRow": function (row, data, index) {
            AddRow(row);
        },
        "bPaginate": false,
        "columns": [
            { "title": "#" }, /*0*/
            { "title": "Patient Name" }, 
            { "title": "Father or Husband Name" },
            { "title": "CNIC" },
            { "title": "Gender" },
            { "title": "Contact" },
            { "title": "Date of Birth" },
            { "title": "Address" },
            { "title": "Age" },
            { "title": "Email" },
            { "title": "User Name" },
            { "title": "City Name" },
            { "title": "Country Name" },
        ],
        "columnDefs": [
            //{ targets: [6], 'visible': false },
        ],
        "order": [[1, 'desc']],
    });

    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    table.on('draw', function () {
        if (!table.data().count()) {
            $(".jumbotron").css("display", "none");
        } else {
            $(".jumbotron").css("display", "block");
        }
    });
});

$("#buttonPopulateExcelData").click(function () {
    var file = $("#ExcelFileForBulkUpload").prop("files")[0];
    if (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, { type: 'binary' });
            var worksheet = workbook.Sheets[workbook.SheetNames[0]];
            var jsonData = XLSX.utils.sheet_to_json(worksheet, { header: 1 });
            var headers = jsonData[0];
            var rows = jsonData.slice(1);
            var ExcelDataObjects = [];

            rows.forEach(function (row) {
                var obj = {};
                headers.forEach(function (header, index) {
                    obj[header] = row[index];
                });
                ExcelDataObjects.push(obj);
            });

            PopulateDataTable(ExcelDataObjects);
        };
        reader.readAsBinaryString(file);
    } else {
        alert("Failed to load file");
    }
    $("#ExcelFileForBulkUpload").val('');
});

function PopulateDataTable(ExcelDataObjects) {
    table.clear().draw();
    $.each(ExcelDataObjects, function (index, row) {
        var rowData = [
            '',
            row["Patient Name"], // Name(s)
            row["Father or Husband Name"], // Name(s)
            row["CNIC"], // Father Name
            row["Gender"], // CNIC
            row["Contact"], // Contact
            row["Date of Birth"], // Email
            row["Address"], // Email
            row["Age"], // Email
            row["Email"], // Email
            row["UserName"], // Email
            row["City"], // Email
            row["Country"], // Email
        ];
        table.row.add(rowData);
    });
    table.draw();
}

