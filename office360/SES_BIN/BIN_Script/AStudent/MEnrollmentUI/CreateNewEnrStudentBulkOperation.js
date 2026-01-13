
var OperationType = "";
var SubOperationType = "";
var DB_OperationType = $('#HiddenFieldDB_OperationType').val();
var IsFieldClear = false;
var table = "";


$(document).ready(function () {
    switch (DB_OperationType)
    {

        case PARAMETER.DB_OperationType.INSERT:

            $('#DivButtonSubmitDown').show();
            $('#DivButtonUpdateDown').hide();
            break;

        case PARAMETER.DB_OperationType.UPDATE:
            $('#DivButtonSubmitDown').hide();
            $('#DivButtonUpdateDown').show();
            break;
    }
    InitDataTable();
    PopulateDropDownLists();
    ChangeCase();
});



//----------- ALL DATA TABLE FUNCTION
function InitDataTable() {
    table = $('#MainTableEnrollmentList').DataTable({
        "responsive": true, "ordering": false,
        "processing": true, "pagination": false,
        "paging": false,
        "columns": [
            { "title": "#", "orderable": false, },
            { "title": "Reg",  },//1
            { "title": "Student Name", },//2
            { "title": "Student CNIC", },//3
            { "title": "D.O.B", },//4
            { "title": "Religion", },//5
            { "title": "Country", },//6
            { "title": "Domicile", },//7
            { "title": "Father Name", },//8
            { "title": "Father CNIC", },//9
            { "title": "Occupation", },//10
            { "title": "Is Father Alive", },//11
            { "title": "Guardian", },//12
            { "title": "Guardian CNIC", },//13
            { "title": "Student Mobile", },//14
            { "title": "Student Email", },//15
            { "title": "Parent Mobile", },//16
            { "title": "LandLine", },//17
            { "title": "Emergency Mobile", },//18
            { "title": "Address", },//19
            { "title": "ReligionId", },//20
            { "title": "CountryId", },//21
            { "title": "OccupationId", },//22


       
        ],
        "columnDefs": [
            { visible: false, targets: [20,21,22] },
        ],
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });

    }).draw();
}
$("#ButtonPopulateExcel").click(function () {
    var file = $("#UploadExcelFile").prop("files")[0];
    if (file) {
        startLoading();
        var reader = new FileReader();
        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, { type: 'binary' });
            var worksheet = workbook.Sheets[workbook.SheetNames[0]];
            var jsonData = XLSX.utils.sheet_to_json(worksheet, { header: 1 });
            if (jsonData.length < 2) {
                alert("The Excel file must have at least two rows (headers and data).");
                return;
            }

            // Check if the data exceeds 1000 rows
            var rowCount = jsonData.length - 1; // Exclude header row
           

            var headers = jsonData[1];
            var rows = jsonData.slice(2);
            var dataObj = [];
            rows.forEach(function (row) {
                var obj = {};
                headers.forEach(function (header, index) {
                    obj[header] = row[index];
                });
                dataObj.push(obj);
            });

            PopulateDataTable(dataObj);
        };
        reader.readAsBinaryString(file);
    } else {
        alert("Failed to load file");
    }
    $("#UploadExcelFile").val('');
});
function PopulateDataTable(dataObj) {
    // Clear the table only once
    table.clear();

    // Use map to process the data and generate rows
    const rowsToAdd = dataObj.map(function (row) {
        return [
            '', // Add empty field if needed
            row["Reg"],
            row["Student Name"],
            row["Student CNIC"],
            row["Birth Date"],
            row["Religion"],
            row["Country"],
            row["Domicile"],
            row["Father Name"],
            row["Father CNIC"],
            row["Occupation"],
            row["Is Father Alive"],
            row["Guardian"],
            row["Guardian CNIC"],
            row["Student Mobile"],
            row["Student Email"],
            row["Parent Mobile"],
            row["LandLine"],
            row["Emergency Mobile"],
            row["Address"],
            row["ReligionId"],
            row["CountryId"],
            row["OccupationId"],
        ];
    });

    // Add all rows at once
    table.rows.add(rowsToAdd);

    // Redraw the table
    table.draw();
    stopLoading();

}

function PopulateDropDownLists() {
    PopulateMT_GeneralBranch_ListByParam();
    PopulateLK_AdmissionCatagory_List();
}


//-----------ALL CHANGE CASES
function ChangeCase() {
    $('#DropDownListCampus').change(function (event) {
        event.preventDefault();
        $('#DropDownListSession').val('-1').change();
        PopulateMT_AppSession_ListByParam();
        //GET_GENERALBRANCH_DETAILBYID();
    });
    $('#DropDownListSession').change(function (event) {
        event.preventDefault();
        $('#DropDownListClass').val('-1').change();
        PopulateMT_AppSessionDetail_ListByParam();
        PopulateMT_AppClass_ListByParam();
    });

    $('#DropDownListClass').change(function (event) {
        event.preventDefault();
        var ClassId = $('#DropDownListClass :selected').val();
        if (ClassId != null && ClassId != undefined && ClassId != "" && ClassId != "-1") {
            CHECK_FEESTRUCTURE_FOR_CLASS();
        }

    });



}

//-----------ALL DROPDOWN LIST
function PopulateMT_GeneralBranch_ListByParam() {
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.BRANCH_BY_USER_ALLOWEDBRANCHIDS,
    }
    $.ajax({

        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_MT_GENERALBRANCH_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCampus").html(s);
            if (RoleId == Roles.RoleID_ADMIN || RoleId == Roles.RoleID_DEVELOPER) {
                $("#DropDownListCampus").prop('disabled', Status.Close);
            }
            else {
                $("#DropDownListCampus").prop('disabled', Status.Open);
            }
        },
        complete: function () {
            $("#DropDownListCampus").val(BranchId).change();

            stopLoading();
        },
    });
}
function PopulateMT_AppSession_ListByParam() {
    var CampusId = $('#DropDownListCampus :selected').val();
    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.APPSESSION_FOR_NEW_ADMISSION,
        CampusId: CampusId,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_MT_APPSESSION_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option ' +
                    'data-ClassIds="' + data[i].ClassIds + '" ' +
                    'value="' + data[i].Id + '">' + data[i].Description +
                    '</option>';
            }
            $("#DropDownListSession").html(s);

        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMT_AppSessionDetail_ListByParam() {

    var SessionId = $('#DropDownListSession :selected').val();

    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.APPSESSIONDETAIL_BY_APPSESSION,
        SessionId: SessionId,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_MT_APPSESSIONDETAIL_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '">' + data[i].Description + '</option>';
            }
            $("#DropDownListRegisteredPeriod").html(s);
        },
        complete: function () {
            stopLoading();
        },

    });

};
function PopulateMT_AppClass_ListByParam() {

    var CampusId = $('#DropDownListCampus :selected').val();
    var ClassIds = $('#DropDownListSession :selected').attr('data-ClassIds');

    var JsonArg = {
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.APPCLASS_BY_APPSESSION,
        CampusId: CampusId,
        ClassIds: ClassIds,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_MT_APPCLASS_BYPARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '">' + data[i].Description + '</option>';
            }
            $("#DropDownListClass").html(s);
        },
        complete: function () {
            stopLoading();
        },

    });

};
function PopulateLK_AdmissionCatagory_List() {
    var JsonArg = {
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GET_LK1_ADMISSIONCATAGORY",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListAdmissionCatagory").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}




//-----------DB OPERATION CALL
function ValidateInputFields() {
    if ($('#DropDownListCampus').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListSession').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListRegisteredPeriod').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListClass').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListAdmissionCatagory').RequiredDropdown() == false) {
        return false;
    }
   
    return true;
}
$('#ButtonSubmitDown').click(function (event) {
    event.preventDefault();

    var IsValid = true;
    if (IsValid) {
        try {
            OperationType = PARAMETER.DB_OperationType.INSERT;
            SubOperationType = PARAMETER.DB_SubOperationType.ENRSTUDENT_NEWADMISSION_INSERT_EXCELUPLOAD;
            UpSertDataIntoDB();
        }
        catch (err) {
            GetMessageBox(err, 500);

        }
    }
});


function UpSertDataIntoDB() {
    var CampusId = $("#DropDownListCampus :selected").val();
    var SessionId = $("#DropDownListSession :selected").val();
    var RegisteredPeriodId = $("#DropDownListRegisteredPeriod :selected").val();
    var ClassId = $("#DropDownListClass :selected").val();
    var AdmissionCatagoryId = $("#DropDownListAdmissionCatagory :selected").val();







    var EnrStudentDetail = $('#MainTableEnrollmentList').DataTable().rows().data().toArray();
    var customJson = EnrStudentDetail.map(function (row, index) {
        return {
            OperationType: OperationType,
            SubOperationType: SubOperationType,

            CampusId: CampusId,
            SessionId: SessionId,
            RegisteredPeriodId: RegisteredPeriodId,
            ClassId: ClassId,
            AdmissionCatagoryId: AdmissionCatagoryId,

            RegistrationNo: row[1],
            StudentName: row[2],
            StudentCNIC: row[3],
            BirthDate: row[4],
            //ReligionId: row[5],
            //CountryId: row[6],
            DomicileDistrict: row[7],
            FatherName: row[8],
            FatherCNIC: row[9],
            OccupationId: row[10],
            IsFatherAlive: row[11],
            GuardianName: row[12],
            GuardianCNIC: row[13],
            StudentMobile: row[14],
            StudentEmail: row[15],
            ParentMobile: row[16],
            LandLine: row[17],
            EmergencyMobile: row[18],
            Address: row[19],
            ReligionId: row[20],
            CountryId: row[21],
            OccupationId: row[22],


        };
    });


    var JsonArg = {
        OperationType: OperationType,
        SubOperationType: SubOperationType,

        CampusId: CampusId,
        SessionId: SessionId,
        RegisteredPeriodId: RegisteredPeriodId,
        ClassId: ClassId,
        AdmissionCatagoryId: AdmissionCatagoryId,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/UpSert_Into_EnrStudent",
        dataType: 'json',
        data: { 'PostedDataDetail': customJson },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            GetMessageBox(data.Message, data.StatusCode);
        },
        complete: function () {
            stopLoading();
        },
        error: function (jqXHR, error, errorThrown) {
            GetMessageBox("The Transaction Can Not Be Performed Due To Serve Activity", 500);

        },
    });

}
function ClearInputFields() {
    table.clear();

    $('.form-control').val('');
    $('.select2').val('-1').change();
    $('form').removeClass('Is-Valid');
}



//-----------DUPLICATE CHECK :: IF FEE STRUCTURE ALREADY EXIST FOR SELECTED CLASS
function CHECK_FEESTRUCTURE_FOR_CLASS() {
    var SessionId = $('#DropDownListSession :selected').val();
    var ClassId = $('#DropDownListClass :selected').val();
    var JsonArg = {
        SessionId: SessionId,
        ClassId: ClassId,
    }

    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/CHECK_FEESTRUCTURE_FOR_CLASS",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            table.clear().draw();
            if ((data.length < 0) || (data.length == 0) || (data == undefined)) {
                GetMessageBox("No Fee Found For Selected Class", 500);
                return;
            }
        },
        complete: function () {
            stopLoading();
        },
    });
}
