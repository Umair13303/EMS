$(document).ready(function () {
  
});
function ValidateFormData() {
    if (!$('#DropDownAdvertismentType').RequiredListItem()) {
        return false;
    }
    if (!$('#TextBoxTitle').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxStartDate').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxEndDate').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxDescription').RequiredTextBoxInputGroup()) {
        return false;
    }
    return true;
}
$('#SubmitDown').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateFormData();
    if (IsValid) {
        try {
            var Type = $('#DropDownAdvertismentType :selected').val();
            var Title = $('#TextBoxTitle').val();
            var StartDate = $('#TextBoxStartDate').val();
            var EndDate = $('#TextBoxEndDate').val();
            var Description = $('#TextBoxDescription').val();


            var JsonArg = {
                Type: Type,
                Title: Title,
                StartDate: StartDate,
                EndDate: EndDate,
                Description: Description
            };
            $.ajax({
                type: "POST",
                url: '/Advertisment/HRMPublishAdvertisment',
                data: { 'HRAdvertisment': (JsonArg) },
                dataType: "json",
                success: function (data) {
                    debugger
                    ClearFields();
                    var StatusCode = data.StatusCode;
                    var Message = data.Message;
                    GetMessageBox(StatusCode,Message)
                },
                error: function () {
                    toastr.error("Failed to make the AJAX request", "Error");
                }
            });
        }
        catch (err) {
            alert(err);
        }
    }

    

});
function ClearFields() {
    debugger
    $('#DropDownAdvertismentType').val('-1').trigger('change');
    $('#TextBoxTitle').val('');
    $('#TextBoxStartDate').val('');
    $('#TextBoxEndDate').val('');
    $('#TextBoxDescription').val('');
    return true
}