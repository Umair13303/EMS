var current = 0;
var tabs = $(".tab");
var tabs_pill = $(".tab-pills");

loadFormData(current);

function loadFormData(n) {
    $(tabs_pill[n]).addClass("active");
    $(tabs[n]).removeClass("d-none");

    $("#back_button").attr("disabled", n === 0);

    if (n === tabs.length - 1) {
        $("#next_button").text("Submit").removeAttr("onclick");
        $('.DivDB_OperationButton').show();
    }

    else {
        $("#next_button").attr("type", "button").text("Next").attr("onclick", "next()");
        $('.DivDB_OperationButton').hide();

    }
}

function next() {
    if (validateCurrentTab()) {
        $(tabs[current]).addClass("d-none");
        $(tabs_pill[current]).removeClass("active");

        current++;
        loadFormData(current);
    }
}

function back() {
    $(tabs[current]).addClass("d-none");
    $(tabs_pill[current]).removeClass("active");

    current--;
    loadFormData(current);
}

function validateCurrentTab() {
    var isValid = true;

    // Find all required fields in the current tab
    $(tabs[current]).find(".required").each(function () {
        if ($(this).val().trim() === "") {
            $(this).addClass("is-invalid"); // Add error class if field is empty
            isValid = false;
            GetMessageBox("Please Fill Required Fields",500)
        } else {
            $(this).removeClass("is-invalid"); // Remove error class if field is filled
        }
    });

    return isValid;
}
