using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form.DatePicker;

[HtmlTargetElement("tiknas-date-picker", TagStructure = TagStructure.NormalOrSelfClosing)]
public class TiknasDatePickerTagHelper : TiknasDatePickerBaseTagHelper<TiknasDatePickerTagHelper>
{
    public ModelExpression? AspFor { get; set; }
    
    public TiknasDatePickerTagHelper(TiknasDatePickerTagHelperService service) : base(service)
    {
    }
}