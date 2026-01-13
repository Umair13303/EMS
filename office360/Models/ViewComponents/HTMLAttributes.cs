using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Models.ViewComponents
{
    public static class HTMLInputType
    {

        public const string CheckBox = "checkbox";
        public const string Color = "color";
        public const string Button = "button";
        public const string Date = "date";
        public const string DatePicker = "datepicker";
        public const string DateRangePicker = "DateRangePicker";
        public const string DateTimeLocal = "datetime-local";
        public const string Email = "email";
        public const string Month = "month";
        public const string Number = "number";
        public const string Password = "password";
        public const string Range = "range";
        public const string Radio = "radio";
        public const string Search = "search";
        public const string Switch = "switch";
        public const string Tel = "tel";
        public const string Text = "text";
        public const string Time = "time";
        public const string URL = "url";
        public const string Week = "week";

    }
    public static class ControlName
    {
        public static  string Portlet = "Portlet";
        public static string Input = "Input";
        public static string Label = "Label";
        public static string SectionHead = "SectionHead";
        public static string PortletHead = "PortletHead";
        public static string Button = "Button";
        public static string ReportViewer = "ReportViewer";
        /// <summary>
        /// User new DataTableAttributes with it
        /// </summary>
        public static string DataTable = "DataTable";
        public static string TextArea = "TextArea";
        public static string SelectPicker = "SelectPicker";
        /// <summary>
        /// Use new DropDownAttributes with it
        /// </summary>
        public static string DropDown = "DropDown";
    }
    public class LengthyClasses
    {
        public string form = "m-form m-form--state m-form--fit m-form--label-align-right";
        public string select = "form-control form-control-sm m-input m-input--air select-picker";
        public string select2 = "form-control form-control-sm m-input select2";
        public string button = "btn-wide btn-pill btn-shadow btn-hover-shine btn btn-primary";
        public string buttonElevationOutline = "btn btn-outline-brand btn-elevate btn-pill";
        public string buttonNoGradient = "btn m-btn m-btn--gradient-from-primary m-btn--gradient-to-info  m-btn--icon";
        public string ButtonLoaderRight = "m-loader m-loader--light m-loader--right";
        public string ButtonLoaderLeft = "m-loader m-loader--light m-loader--left";
        public string DataTable = "table table-striped table-bordered table-hover table-checkable display nowrap";
    }
    public static class IconClass
    {
        public const string Save = "fa fa-save";//"la la-arrow-right";
        public const string Add = "fa fa-plus";
        public const string Print = "fa fa-print";
        /*Waqas khan*/
        public const string Phone = "fa fa-phone";
        public const string Address = "fa-solid fa-address-card";
        public const string Envelope = "fa fa-envelope";
        public const string IdCard = "fa-id-card";
    }

    public static class UIState
    {
        public const string Default = "default";
        public const string Secondary = "secondary";
        public const string Primary = "primary";
        public const string Success = "success";
        public const string Warning = "warning";
        public const string Danger = "danger";
    }
    public static class Size
    {
        public const string Small = "sm";
        public const string Medium = "md";
        public const string Large = "lg";
    }

    public class HTMLAttributes
    {
        public const string GetAllAttributesNames = "area;Type;Id;Name;Value;ExtraClasses;Readonly;Required;Text;Min;Max;For;RadioListItem";

        public HTMLAttributes()
        {
            area = "";
            if (string.IsNullOrEmpty(Name))
            {
                Name = Id;
            }
            if (rows == null || rows == 0)
            {
                rows = 3;
            }
        }
        public string ControlName { get; set; }
        public string area { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrEmpty(value) ? Id : value;
        }
        public string Value { get; set; }
        public string ExtraClasses { get; set; }
        public string IconClass { get; set; }
        public bool Readonly { get; set; }
        public bool Disabled { get; set; }
        public bool Required { get; set; }
        public string Text { get; set; }
        public bool CheckTextForLabel()
        {
            return Text != null && new string[] { HTMLInputType.Radio, HTMLInputType.CheckBox, HTMLInputType.Switch }.Contains(Type) == false;
        }
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string For { get; set; }
        public int? rows { get; set; }
        public bool? Checked { get; set; }
        public string Placeholder { get; set; }
        public bool AutoComplete { get; set; }
        public string Width { get; set; }
        public string Style { get; set; }

        public IEnumerable<SelectListItem> RadioListItem { get; set; }


        //public bool GenerateLabel { get; set; }
        public string InputIconLeft { get; set; }
        public string InputIconRight { get; set; }
        public string GroupIconLeft { get; set; }
        public string GroupIconRight { get; set; }

        public HTMLAttributes GroupButtonLeft { get; set; }
        public HTMLAttributes GroupButtonRight { get; set; }

        public DropDownAttributes GroupDropDownLeft { get; set; }
        public DropDownAttributes GroupDropDownRight { get; set; }
        public DropDownAttributes DynamicDropDownGroup { get; set; }


        /// <summary>
        /// Used with Button
        /// </summary>
        public ButtonAttributes ButtonAttributes { get; set; }
        public NumberAttributes NumberAttributes { get; set; }

        public List<InputGroupAttribute> DynamicInputGroupLeft { get; set; }
        public List<InputGroupAttribute> DynamicInputGroupRight { get; set; }
        /// <summary>
        /// Is First Element Of Section // Removes <br> From `SectionHead`
        /// </summary>
        public bool First { get; set; }



    }

    public class NumberAttributes
    {
        public NumberAttributes()
        {
            IsNumber = IsLC;
            IsRate = IsFC;
        }
        public bool IsInt { get; set; }
        /// <summary>
        /// { Range = 10, Precision = 6 }
        /// </summary>
        public bool IsRate { get; set; }
        /// <summary>
        /// { Range = 10, Precision = 4 }
        /// </summary>
        public bool IsNumber { get; set; }
        public bool IsLC { get; set; }
        public bool IsFC { get; set; }
        public bool AllowNegative { get; set; }
    }

    public class InputGroupAttribute
    {
        public InputGroupAttribute() { }
        public InputGroupAttribute(string Text)
        {
            this.Text = new HTMLAttributes { Text = Text };
        }
        public InputGroupAttribute(string Text, string ExtraClass)
        {
            this.Text = new HTMLAttributes { Text = Text, ExtraClasses = ExtraClass };
        }

        /// <summary>
        /// First Priority
        /// </summary>
        public HTMLAttributes Button { get; set; }
        /// <summary>
        /// Second Priority
        /// </summary>
        public DropDownAttributes DropDown { get; set; }
        /// <summary>
        /// Third Priority
        /// </summary>
        public HTMLAttributes Text { get; set; }
        /// <summary>
        /// Fourth Priority
        /// </summary>
        public HTMLAttributes Input { get; set; }

    }

    public class SideOrientation
    {
        public const string Left = "Left";
        public const string Right = "Right";
    }

    public class DropDownAttributes
    {

        static public implicit operator DropDownAttributes(List<SelectListItem> value)
        {
            return new DropDownAttributes
            {
                SelectLists = ConvertSelectListItems(value.ToList())
            };
        }
        static public implicit operator DropDownAttributes(SelectListItem[] value)
        {
            return new DropDownAttributes
            {
                SelectLists = ConvertSelectListItems(value.ToList())
            };
        }
        public DropDownAttributes()
        {
            area = "";
        }
        public string ControlName { get; set; }
        public DropDownAttributes(HTMLAttributes Model)
        {
            if (Model.GroupDropDownLeft != null && Model.GroupDropDownLeft.SelectLists != null && Model.GroupDropDownLeft.SelectLists.Count() > 0 || (Model.GroupDropDownRight != null && Model.GroupDropDownRight.SelectLists != null && Model.GroupDropDownRight.SelectLists.Count() > 0))
            {
                NoSearch = true;
                InputGrouped = true;
                HTMLAttributes = new HTMLAttributes();
                if (Model.GroupDropDownLeft.SelectLists.Count() > 0)
                {
                    if (Model.GroupDropDownLeft.HTMLAttributes != null)
                    {
                        HTMLAttributes = Model.GroupDropDownLeft.HTMLAttributes;
                    }
                    SelectLists = Model.GroupDropDownLeft.SelectLists;
                }
                else if (Model.GroupDropDownRight != null && Model.GroupDropDownRight.SelectLists.Count() > 0)
                {
                    if (Model.GroupDropDownLeft.HTMLAttributes != null)
                    {
                        HTMLAttributes = Model.GroupDropDownLeft.HTMLAttributes;
                    }
                    SelectLists = Model.GroupDropDownRight.SelectLists;
                }
            }

        }
        public bool InputGrouped { get; set; }
        public bool NoSearch { get; set; }
        public string area { get; set; }
        public string ViewBag { get; set; }
        public string OptionalLabel { get; set; }
        public bool Multiple { get; set; }
        public bool DontInit { get; set; }
        public bool AddOther { get; set; }


        public HTMLAttributes HTMLAttributes { get; set; }

        public List<HTMLSelectListItem> SelectLists { get; set; }
        public static List<HTMLSelectListItem> ConvertSelectListItems(List<SelectListItem> selectListItems)
        {
            List<HTMLSelectListItem> HTMLSelectListItems = new List<HTMLSelectListItem>();
            foreach (var item in selectListItems)
            {
                HTMLSelectListItems.Add(new HTMLSelectListItem { SelectListItem = item });
            }
            return HTMLSelectListItems;
        }
    }
    /// <summary>
    /// Depriciated
    /// </summary>
    public class SelectPickerAttributes
    {

        public SelectPickerAttributes()
        {
            area = "";
        }
        public string area { get; set; }
        public string Id { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrEmpty(value) ? Id : value;
        }
        public IEnumerable<SelectPickerListItem> SelectPickerListItem { get; set; }
    }
    /// <summary>
    /// Depriciated
    /// </summary>
    public class SelectPickerListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool? Selected { get; set; }
        public string DataTokens { get; set; }
    }

    public class ButtonAttributes
    {
        public bool NoGradient { get; set; }
        public string UIState { get; set; }
        public string Icon { get; set; }
        public string Size { get; set; }
    }

    public class DataTableAttributes
    {
        public DataTableAttributes()
        {
            area = "";
        }
        public string area { get; set; }
        public string Text { get; set; }
        public bool DontInit { get; set; }
        public bool WithPortlet { get; set; }

        public string[] ColumnNames { get; set; }
        public HTMLAttributes HTMLAttributes { get; set; }
    }
}