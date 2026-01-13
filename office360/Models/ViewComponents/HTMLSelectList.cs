using office360.Models.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Models.ViewComponents
{

    public class HTMLSelectList
    {
    }

    public class HTMLSelectListItem
    {
        public SelectListItem SelectListItem { get; set; }
        public string Code { get; set; }

        public string AnotherCode { get; set; }
    }
}
namespace office360.Models.ViewModel.Helpers
{
    public static class HtmlHelpers1
    {
        public static MvcHtmlString CustomLabel(this HtmlHelper htmlHelper, HTMLAttributes attributes)
        {
            var labelTag = new TagBuilder("label");
            labelTag.MergeAttribute("for", attributes.For);
            labelTag.SetInnerText(attributes.Text);
            labelTag.AddCssClass(ControlName.Label);
            return MvcHtmlString.Create(labelTag.ToString());
        }

        public static MvcHtmlString CustomInput(this HtmlHelper htmlHelper, HTMLAttributes attributes)
        {
            var inputTag = new TagBuilder("input");
            inputTag.MergeAttribute("type", attributes.Type);
            inputTag.MergeAttribute("id", attributes.Id);
            inputTag.MergeAttribute("name", attributes.Name);
            inputTag.MergeAttribute("placeholder", attributes.Placeholder);
            inputTag.AddCssClass(ControlName.Input);
            if (!string.IsNullOrEmpty(attributes.ExtraClasses))
            {
                inputTag.AddCssClass(attributes.ExtraClasses);
            }
            return MvcHtmlString.Create(inputTag.ToString(TagRenderMode.SelfClosing));
        }
    }
}