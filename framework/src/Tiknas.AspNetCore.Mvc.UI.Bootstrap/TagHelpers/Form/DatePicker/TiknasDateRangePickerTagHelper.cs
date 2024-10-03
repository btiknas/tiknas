using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form.DatePicker;

[HtmlTargetElement("tiknas-date-range-picker", TagStructure = TagStructure.NormalOrSelfClosing)]
public class TiknasDateRangePickerTagHelper : TiknasDatePickerBaseTagHelper<TiknasDateRangePickerTagHelper>
{
    public ModelExpression? AspForStart { get; set; }

    public ModelExpression? AspForEnd { get; set; }

    public TiknasDateRangePickerTagHelper(TiknasDateRangePickerTagHelperService tagHelperService) :
        base(tagHelperService)
    {
    }
}