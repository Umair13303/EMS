$(document).ready(function () {



});
$('#SubmitDown').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateForm();
    if (IsValid) {
        try {
            var AdvertismentCode = $('#HiddenFieldAdvertismentCode').val();
            var ApplicantCode = $('#HiddenFieldApplicantCode').val();
            var InterViewDate = $('#TextBoxInterViewDate').val();
            var InterViewTime = $('#TextBoxInterViewTime').val();
            var JsonArg = {
                AdvertismentCode: AdvertismentCode,
                ApplicantCode: ApplicantCode,
                InterViewDate: InterViewDate,
                InterViewTime: InterViewTime
            };
            $.ajax({
                type: "POST",
                url: '/SaveDataIntoDb/InsertHRInterviews',
                data: { 'HRInterviews': (JsonArg) },
                dataType: "json",
                success: function () {
                    toastr.success("Success", "Saved Successfully");
                    ClearFields();
                },

                error: function () {
                    toastr.error("Error", "Failed To Save");
                }
            });



        }
        catch (err) {
            alert(err);
        }


    }

});

function ValidateForm() {
    if (!$('#HiddenFieldAdvertismentCode').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#HiddenFieldApplicantCode').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxInterViewDate').RequiredTextBoxInputGroup()) {
        return false;
    }
    if (!$('#TextBoxInterViewTime').RequiredTextBoxInputGroup()) {
        return false;
    }
    return true;
}
function ClearFields() {
    $('#HiddenFieldAdvertismentCode').val('');
    $('#HiddenFieldApplicantCode').val('');
    $('#TextBoxInterViewDate').val('');
    $('#TextBoxInterViewTime').val('');
    return true

}