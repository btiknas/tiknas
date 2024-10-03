using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[HtmlTargetElement("tiknas-radio")]
public class TiknasRadioInputTagHelper : TiknasTagHelper<TiknasRadioInputTagHelper, TiknasRadioInputTagHelperService>
{
    public ModelExpression AspFor { get; set; } = default!;

    public string? Label { get; set; }

    public bool? Inline { get; set; }

    public bool? Disabled { get; set; }

    public IEnumerable<SelectListItem>? AspItems { get; set; }

    public TiknasRadioInputTagHelper(TiknasRadioInputTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
