/*----------------------------------** FUNCTION FOR::GLOBAL VARIABLE **-------------------------------------------------------------------------------------*/
var SELECT_DDL = '.select2';
var PHONENO = '.PhoneNumber';
var MOBILENO = '.MobileNumber';
var CNIC = '.CNICNumber';
var EMAIL = '.EmailAddress';
var NTN = '.NTNNumber';
/*----------------------------------** FUNCTION FOR::INITIALIZER CLASSES **-------------------------------------------------------------------------------------*/
INITIALIZE_SELECT2(SELECT_DDL);
INITIALIZE_INPUT_MASK(PHONENO, MASKING.PHONENO);
INITIALIZE_INPUT_MASK(MOBILENO, MASKING.MOBILENO);
INITIALIZE_INPUT_MASK(CNIC, MASKING.CNIC);
INITIALIZE_INPUT_MASK(NTN, MASKING.NTN);
INITIALIZE_INPUT_MASK(EMAIL, MASKING.EMAIL);


/*----------------------------------** FUNCTION FOR::SELECT2 DROPDOWN **-------------------------------------------------------------------------------------*/
function INITIALIZE_SELECT2(selector) {
    //$(selector).select2();
}

/*----------------------------------** FUNCTION FOR::MASKING **---------------------------------------------------------------------------------------------*/

function INITIALIZE_INPUT_MASK(selector, maskPattern) {
    $(selector).inputmask(maskPattern);
}

/*----------------------------------** FUNCTION FOR::INPUT FIELD VALIDATION **------------------------------------------------------------------------------*/
$.fn.RequiredTextBoxInputGroup = function () {
    $(this).removeClass('is-invalid is-valid');
    $(this).css('border', ''); // Reset the border

    var labelText = '';
    var inputId = $(this).attr('id');
    var labelForInput = $('label[for="' + inputId + '"]');

    if (labelForInput.length) {
        labelText = labelForInput.text();
    }

    var $existingError = $(this).parent().next('.error'); // Check for existing error

    if ($(this).val() == null || $(this).val() == '' || $(this).val() == undefined) {
        var errorMessage = labelText + ' is required.';

        // Add the error only if it doesn't already exist
        if ($existingError.length === 0) {
            var $errorDiv = $('<div class="col-sm-12 error text-danger">' + errorMessage + '</div>');
            $(this).parent().after($errorDiv);

            // Scroll to the input field
            $(this)[0].scrollIntoView({ behavior: 'smooth', block: 'center' });

            setTimeout(function () {
                $errorDiv.remove();
            }, 5000);
        }

        $(this).addClass('is-invalid');
        return false;
    } else {
        // Remove the error if input is valid
        if ($existingError.length) {
            $existingError.remove();
        }
        $(this).addClass('is-valid');
        return true;
    }
};
$.fn.RequiredDropdown = function () {
    // Remove any existing validation classes and reset styling
    $(this).removeClass('is-invalid is-valid');
    $(this).css('border', '');

    var labelText = '';
    var inputId = $(this).attr('id');
    var labelForInput = $('label[for="' + inputId + '"]');

    if (labelForInput.length) {
        labelText = labelForInput.text();
    }

    var $formGroup = $(this).closest('.form-group');
    var $existingError = $formGroup.next('.error'); // Check for existing error message

    if (!$(this).val() || $(this).val() === '-1') {
        var errorMessage = 'Please select a ' + labelText;

        if ($existingError.length === 0) {
            var $errorDiv = $('<div class="col-sm-12 error text-danger">' + errorMessage + '</div>');
            $formGroup.after($errorDiv);

            $(this)[0].scrollIntoView({ behavior: 'smooth', block: 'center' });

            setTimeout(function () {
                $errorDiv.remove();
            }, 5000);
        }

        $(this).addClass('is-invalid');
        return false;
    } else {
        // Remove the error if valid
        if ($existingError.length) {
            $existingError.remove();
        }

        $(this).addClass('is-valid');
        return true;
    }
};
function QueryDropDownListContainer_Plain(item, fields = []) {
    if (!item.id) return item.text;

    let template = '<div style="padding: 5px; border-bottom: 0px solid #ddd;">';
    fields.forEach(field => {
        if (item[field]) {
            template += `<div><strong>${field.replace(/([A-Z])/g, ' $1')}: </strong> ${item[field]}</div>`;
        }
    });
    template += '</div>';

    return $(template);
}


/*----------------------------------** FUNCTION FOR::UTTILITY FOR DATA TABLES **----------------------------------------------------------------------------*/
function GetStatus(Status) {
    var Badge = ""; // Initialize Badge variable to an empty string
    switch (Status) {
        case 1:
            BadgeColor = "primary";
            Display = "Active Company";
            break;

        case 2:
            BadgeColor = "secondary";
            Display = "Inactive Company";
            break;

        case 3:
            BadgeColor = "danger";
            Display = "Deleted Company";
            break;

        case 4:
            BadgeColor = "primary";
            Display = "Active User";
            break;

        case 5:
            BadgeColor = "secondary";
            Display = "Inactive User";
            break;

        case 6:
            BadgeColor = "danger";
            Display = "Deleted User";
            break;

        case 7:
            BadgeColor = "primary";
            Display = "Active Right Setting";
            break;

        case 8:
            BadgeColor = "secondary";
            Display = "Inactive Right Setting";
            break;

        case 9:
            BadgeColor = "primary";
            Display = "Active User Right";
            break;

        case 10:
            BadgeColor = "secondary";
            Display = "Inactive User Right";
            break;

        case 11:
            BadgeColor = "primary";
            Display = "Active Branch";
            break;

        case 12:
            BadgeColor = "secondary";
            Display = "Inactive Branch";
            break;

        case 13:
            BadgeColor = "danger";
            Display = "Deleted Branch";
            break;

        case 14:
            BadgeColor = "primary";
            Display = "Active Branch Setting";
            break;

        case 15:
            BadgeColor = "secondary";
            Display = "Inactive Branch Setting";
            break;

        case 16:
            BadgeColor = "warning";
            Display = "Expired Branch Setting";
            break;

        case 17:
            BadgeColor = "danger";
            Display = "Deleted Branch Setting";
            break;

        case 18:
            BadgeColor = "primary";
            Display = "Active Academic Class";
            break;

        case 19:
            BadgeColor = "secondary";
            Display = "Inactive Academic Class";
            break;

        case 20:
            BadgeColor = "danger";
            Display = "Deleted Academic Class";
            break;

        case 21:
            BadgeColor = "primary";
            Display = "Active Academic Admission Session";
            break;

        case 22:
            BadgeColor = "secondary";
            Display = "Inactive Academic Admission Session";
            break;

        case 23:
            BadgeColor = "danger";
            Display = "Deleted Academic Admission Session";
            break;

        case 24:
            BadgeColor = "primary";
            Display = "Active Academic Subject";
            break;

        case 25:
            BadgeColor = "secondary";
            Display = "Inactive Academic Subject";
            break;

        case 26:
            BadgeColor = "danger";
            Display = "Deleted Academic Subject";
            break;

        case 27:
            BadgeColor = "primary";
            Display = "Active Academic Curriculum";
            break;

        case 28:
            BadgeColor = "secondary";
            Display = "Inactive Academic Curriculum";
            break;

        case 29:
            BadgeColor = "danger";
            Display = "Deleted Academic Curriculum";
            break;

        case 30:
            BadgeColor = "primary";
            Display = "Active Academic Curriculum Subject";
            break;

        case 31:
            BadgeColor = "secondary";
            Display = "Inactive Academic Curriculum Subject";
            break;

        case 32:
            BadgeColor = "danger";
            Display = "Deleted Academic Curriculum Subject";
            break;

        case 33:
            BadgeColor = "primary";
            Display = "Active Admission Session Class";
            break;

        case 34:
            BadgeColor = "secondary";
            Display = "Inactive Admission Session Class";
            break;

        case 35:
            BadgeColor = "danger";
            Display = "Deleted Admission Session Class";
            break;

        case 36:
            BadgeColor = "primary";
            Display = "Active Chart of Account";
            break;

        case 37:
            BadgeColor = "secondary";
            Display = "Inactive Chart of Account";
            break;

        case 38:
            BadgeColor = "danger";
            Display = "Deleted Chart of Account";
            break;

        case 39:
            BadgeColor = "primary";
            Display = "Active Discount Type";
            break;

        case 40:
            BadgeColor = "secondary";
            Display = "Inactive Discount Type";
            break;

        case 41:
            BadgeColor = "danger";
            Display = "Deleted Discount Type";
            break;

        case 42:
            BadgeColor = "primary";
            Display = "Active Fee Type";
            break;

        case 43:
            BadgeColor = "secondary";
            Display = "Inactive Fee Type";
            break;

        case 44:
            BadgeColor = "danger";
            Display = "Deleted Fee Type";
            break;

        case 45:
            BadgeColor = "primary";
            Display = "Active Fee Structure";
            break;

        case 47:
            BadgeColor = "secondary";
            Display = "Inactive Fee Structure";
            break;

        case 47:
            BadgeColor = "danger";
            Display = "Deleted Fee Structure";
            break;

        default:
            BadgeColor = "light";
            Display = "Unknown Status";
            break;
    }
    var Label = '<td> <span class="badge badge-' + BadgeColor + '">' + Display + '</span></td>';
    return Label;
}

//---- to be used
function DataTableGroupBy_Column_Detail(ApiWrapper, TableId, ColumnNames, Options = {}) {
    const Api = ApiWrapper.api();
    const VisibleColumnCount = Api.columns(':visible').count();
    const Rows = Api.rows({ page: 'current' }).nodes();
    let LastGroupValues = Array(ColumnNames.length).fill(null);

    Rows.each(Row => {
        const RowData = Api.row(Row).data();
        if (!RowData) return;

        const CurrentGroupValues = ColumnNames.map(Name => RowData[Name]);
        if (!CurrentGroupValues.some((Val, Idx) => Val !== LastGroupValues[Idx])) return;

        const GroupHeaders = ColumnNames.map((Name, Idx) => {
            const Style = GetColorForTableGroup(Idx);
            const Padding = Idx ? 'padding-left: 20px;' : '';
            return `<tr class="${Idx ? 'subgroup' : 'group'} group-header-row">
                <td colspan="${VisibleColumnCount}" style="background:${Style.backgroundColor};color:${Style.color};border-color:${Style.borderColor};${Padding}">
                    <b>${Name}: </b>${CurrentGroupValues[Idx]}
                </td></tr>`;
        }).join('');

        $(Row).before(GroupHeaders);
        LastGroupValues = CurrentGroupValues;
    });
}
function DataTableGroupBy_ColumnIndex(ApiWrapper, TableId, ColumnIndexes, Options = {}) {
    const Api = ApiWrapper.api();
    const VisibleColumnCount = Api.columns(':visible').count();
    const Rows = Api.rows({ page: 'current' }).nodes();
    let LastGroupValues = Array(ColumnIndexes.length).fill(null);
    Rows.each(Row => {
        const RowData = Api.row(Row).data();
        if (!RowData) return;
        const CurrentGroupValues = ColumnIndexes.map(Index => {
            const column = Api.column(Index);
            const columnDataProp = column.settings()[0].aoColumns[Index].data;
            if (columnDataProp === undefined || columnDataProp === null) {
                return RowData[Index];
            }
            return typeof columnDataProp === "function"
                ? columnDataProp(RowData)
                : RowData[columnDataProp];
        });
        if (!CurrentGroupValues.some((Val, Idx) => Val !== LastGroupValues[Idx])) return;
        const GroupHeaders = ColumnIndexes.map((ColIndex, Idx) => {
            const Style = GetColorForTableGroup(Idx);
            const Padding = Idx ? 'padding-left: 20px;' : '';
            const Label = Api.column(ColIndex).header().innerText;
            return `<tr class="${Idx ? 'subgroup' : 'group'} group-header-row">
                <td colspan="${VisibleColumnCount}" style="background:${Style.backgroundColor};color:${Style.color};border-color:${Style.borderColor};${Padding}">
                    <b>${Label}: </b>${CurrentGroupValues[Idx]}
                </td></tr>`;
        }).join('');
        $(Row).before(GroupHeaders);
        LastGroupValues = CurrentGroupValues;
    });
}

//----

function GetColorForTableGroup(index) {
    const colors = [
        { backgroundColor: '#4A235A', color: '#FFFFFF', borderColor: '#BB8FCE' }, // Group 1
        { backgroundColor: '#1F618D', color: '#FFFFFF', borderColor: '#85C1E9' }, // Group 2
        { backgroundColor: '#117A65', color: '#FFFFFF', borderColor: '#76D7C4' }, // Group 3
        // Add more if needed
    ];

    return colors[index] || { backgroundColor: '#D5D8DC', color: '#2C3E50', borderColor: '#AAB7B8' }; // Default
}


/*----------------------------------** FUNCTION FOR::BLOB OBJECT RESPONSE **------------------------------------------------------------------------------*/
function OpenReport(response, status, xhr) {
    var filename = "";
    var disposition = xhr.getResponseHeader('Content-Disposition');
    if (disposition && disposition.indexOf('attachment') !== -1) {
        var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
        var matches = filenameRegex.exec(disposition);
        if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
    }
    var type = xhr.getResponseHeader('Content-Type');
    var blob = new Blob([response], { type: type });
    if (typeof window.navigator.msSaveBlob !== 'undefined') {
        /*IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."*/
        window.navigator.msSaveBlob(blob, filename);
    } else {
        var URL = window.URL || window.webkitURL;
        var downloadUrl = URL.createObjectURL(blob);
        if (filename) {
            /*use HTML5 a[download] attribute to specify filename*/
            var a = document.createElement("a");
            /*safari doesn't support this yet*/
            if (typeof a.download === 'undefined') {
                window.location = downloadUrl;
            } else {
                a.href = downloadUrl;
                a.download = filename;
                document.body.appendChild(a);
                a.click();
            }
        } else {
            var windowVal = window.open(downloadUrl);/*openNewTabOrNewWindow(downloadUrl);*/
            //windowVal.document.write('<html><head><title>EXP-220601-0001</title></head><body height="100%" width="100%"><iframe src="' + downloadUrl + '" height="100%" width="100%"></iframe></body></html>');
            //if (windowVal.document) { }
            windowVal.opener = null;
            if (!windowVal) {
                mesgboxshow("error", "Please allow pop-ups for view report. <strong>How to allow pop-ups : <a target='_blank' href='https://support.mozilla.org/en-US/kb/pop-blocker-settings-exceptions-troubleshooting'><span>Click here<span></a></strong>");
            }
        }
        setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); /*cleanup*/
    }
}

/*----------------------------------** FUNCTION FOR::UI ELEMENTS **--------------------------------------------------------------------------------------*/
let messageBoxActive = false;
function stopLoading() {
    setTimeout(function () {
        $.unblockUI();
    }, 1000);
}
function startLoading() {
    $.blockUI({
        message: 'Loading Please Wait',
        overlayCSS: {
            backgroundColor: '#1b2024',
            opacity: 0.8,
            zIndex: 1200,
            cursor: 'wait'
        },
        css: {
            border: 0,
            color: '#fff',
            zIndex: 1201,
            padding: 0,
            backgroundColor: 'transparent'
        }
    });
}
function GetMessageBox(message, status) {
    // Prevent showing a new message box if one is already shown
    if (messageBoxActive) {
        return;
    }

    messageBoxActive = true; // Set flag to true when a message box is displayed

    const messageContainer = $('#messageContainer');
    let alertClass, iconClass;

    // Determine the alert class and icon class based on the status
    switch (status) {
        case 200:
            alertClass = 'alert-primary';
            iconClass = 'success';
            break;
        case 404:
            alertClass = 'alert-info';
            iconClass = 'error';
            break;
        case 505:
            alertClass = 'alert-danger';
            iconClass = 'info';
            break;
        default:
            alertClass = 'alert-secondary';
            iconClass = 'info';
            break;
    }

    // Show the SweetAlert box
    Swal.fire({
        text: message,
        icon: iconClass
    }).then(() => {
        messageBoxActive = false; // Reset the flag when the message box is closed
    });

    // Optional: To show a custom HTML alert, you can enable the below code
    // const alertHtml = '<div class="alert border-0 ' + alertClass + ' bg-gradient m-b-30 alert-dismissible fade show border-radius-none" role="alert"><strong style="color:white">' + message + '</strong><button type="button" class="close" data-dismiss="alert" aria-label="Close"><i class="' + iconClass + '"></i></button></div>';
    // messageContainer.html(alertHtml).fadeIn('slow').delay(7000).fadeOut();
}
function ErrorMessage(Message) {
    $.blockUI({
        message: Message,
        fadeIn: 800,
        timeout: 8000,
        overlayCSS: {
            backgroundColor: '#1b2024',
            opacity: 0.8,
            zIndex: 1200,
            cursor: 'wait'
        },
        css: {
            border: 0,
            color: '#fff',
            zIndex: 1201,
            padding: 0,
            backgroundColor: 'transparent'
        }
    });
}


/*----------------------------------** FUNCTION FOR::DATE & DATE PICKER **------------------------------------------------------------------------------*/
const INITIALIZE_DATE_PICKER = (selector, options = {}) => {
    flatpickr(selector, options);
};
INITIALIZE_DATE_PICKER('.DatePickerRange', { mode: "range", dateFormat: "Y-m-d" });
//INITIALIZE_DATE_PICKER('.DatePickerSimple', { dateFormat: "Y-m-d", enableTime: false, noCalendar: false });
INITIALIZE_DATE_PICKER('.DatePickerSimple_Range', { dateFormat: "Y-m-d", enableTime: false, noCalendar: false });
INITIALIZE_DATE_PICKER('.DatePickerMonthYear', { dateFormat: "Y-m", enableTime: false, noCalendar: false });
INITIALIZE_DATE_PICKER('.DatePickerYear', { dateFormat: "Y", enableTime: false, noCalendar: false });
INITIALIZE_DATE_PICKER('.DatePickerTimer', { enableTime: true, noCalendar: true, dateFormat: "H:i", defaultDate: "13:45" });

function GET_DATEPICKER_FORUPDATE_INPUTFIELD(ServerDate, DateFormat, InputFieldId) {
    const ParseDate = new Date(parseInt(ServerDate.replace('/Date(', '').replace(')/', '')));
    const picker = flatpickr('#' + InputFieldId, {
        dateFormat: DateFormat,     // Set the display date format
        enableTime: false,       // Disable time selection
        noCalendar: false,
        defaultDate: ParseDate
    });
}
function GET_DATEPICKER_FORUPDATE_INTOLIST(ServerDate, DateFormat, InputFieldId, CSSClass) {
    const ParseDate = new Date(parseInt(ServerDate.replace('/Date(', '').replace(')/', '')));
    const FormattedDate = GET_FORMATED_DATE_FOR_LIST(ParseDate, DateFormat);
    const INPUT_ELEMENT = `<input id="${InputFieldId}" type="text" class="form-control form-control-sm ${CSSClass}" placeholder="${DateFormat}" value="${FormattedDate}" />`;
    const observer = new MutationObserver(() => {
        const inputField = document.getElementById(InputFieldId);

        if (inputField) {
            flatpickr(`#${InputFieldId}`, {
                dateFormat: DateFormat,
                enableTime: false,
                noCalendar: false,
                defaultDate: ParseDate
            });
            observer.disconnect(); 
        }
    });
    observer.observe(document.body, {
        childList: true, 
        subtree: true   
    });

    return INPUT_ELEMENT;
}
function GET_FORMATED_DATE_FOR_LIST(date, format) {
    return flatpickr.formatDate(date, format);
}
function GET_INCREMENT_DATEPICKER_SIMPLE(InputSelector, StartDate, IncrementNoOfDays)
{
    // CONVERT THE START DATE(STRING FORMAT) INTO SYSTEM DATE FORMAT
    var StartDate_Formated = new Date(StartDate);
    if (isNaN(StartDate_Formated)) {
        console.error("Invalid date:", StartDate);
        return;
    }
    // INCREASE THE DATE BY IncrementNoOfDays PROVIDED
    StartDate_Formated.setDate(StartDate_Formated.getDate() + IncrementNoOfDays);
    // FORMATE THE DATE FORMAT FOR DatePickerSimple "Y-m-d"
    var Year = StartDate_Formated.getFullYear();
    var Month = ("0" + (StartDate_Formated.getMonth() + 1)).slice(-2);
    var Day = ("0" + StartDate_Formated.getDate()).slice(-2);
    var TriggerDate_Formated = `${Year}-${Month}-${Day}`;
    // SET VALUE TO INPUT FIELD
    $(InputSelector).val(TriggerDate_Formated);
    return;
}
function GET_TRIGGER_DATEPICKER_SIMPLE_INTERNATIONAL_TIME(ServerDate, selector) {
    let Parsed_Formated = new Date(parseInt(ServerDate.match(/\d+/)[0]));
    let Date_Formated = Parsed_Formated.toISOString().split('T')[0];
    $(selector).val(Date_Formated).trigger('change');
    if ($(selector)[0]._flatpickr) {
        $(selector)[0]._flatpickr.setDate(Date_Formated, true);
    }
}
function GET_TRIGGER_DATEPICKER_SIMPLE(ServerDate, selector) {
    const Parsed_Date = parseInt(ServerDate.match(/\d+/)[0]);
    const StandardDate = new Date(Parsed_Date);
    const Year = StandardDate.getFullYear();
    const Month = String(StandardDate.getMonth() + 1).padStart(2, '0');
    const Day = String(StandardDate.getDate()).padStart(2, '0');
    const Date_Formated = `${Year}-${Month}-${Day}`;

    $(selector).val(Date_Formated).trigger('change');
    if ($(selector)[0]._flatpickr) {
        $(selector)[0]._flatpickr.setDate(Date_Formated, true);
    }
}