class CSS_CLASS {
    static DATATABLE_COLUMN_DDLADOPTER = "DATATABLE_COLUMN_DDLADOPTER";
}
class INPUT_MASKING_CLASS {
    static Phone_Number = "PhoneNumber";
    static Mobile_Number = "MobileNumber";
    static Email_Address = "EmailAddress";
    static CNIC_Number = "CNICNumber";
    static NTN_Number = "NTNNumber";
    static Number_Only = "NumberOnly";
    static Date_Picker_Simple = "DatePickerSimple";
    static Date_Picker_Month_Year = "DatePickerMonthYear";
    static Date_Picker_Year = "DatePickerYear";
    static Date_Picker_Range = "DatePickerRange";
    static Date_Picker_Timer = "DatePickerTimer";
}
class MASKING {
    static PHONENO = "(099) 999-9999";
    static MOBILENO = "(0999) 999-9999";
    static CNIC = "99999-9999999-9";
    static NTN = "9999999-9";
    static DATE = "9999-99-99";
    static EMAIL = "*{1,20}@*{1,20}.*{2,4}";
}
class DYNAMIC_DATATABLE_OPERATION {
    constructor(Id,Description) {
        this.Id = Id;
        this.Description = Description;
    }
}


Object.freeze(CSS_CLASS);
Object.freeze(INPUT_MASKING_CLASS);
Object.freeze(MASKING);
