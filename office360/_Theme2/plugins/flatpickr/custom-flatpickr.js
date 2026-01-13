
var Date_Masking_Format_Basic = flatpickr(document.getElementsByClassName('Date_Masking_Format_Basic'), {
    dateFormat: "d-m-Y",
    enableTime: false,
    noCalendar: false,

});
var Date_Masking_Format_Month = flatpickr(document.getElementsByClassName('Date_Masking_Format_Month'), {
    dateFormat: "m-Y",
    enableTime: false,
    noCalendar: false,
});
var Date_Masking_Format_Month = flatpickr(document.getElementsByClassName('Date_Masking_Format_Year'), {
    dateFormat: "Y",
    enableTime: false,
    noCalendar: false,
});


var f3 = flatpickr(document.getElementsByClassName('Range_Date_Masking_Format_Basic'), {
    mode: "range",
    dateFormat: "d-m-Y",
});
var f4 = flatpickr(document.getElementsByClassName('timeFlatpickr'), {
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i",
    defaultDate: "13:45"
});


