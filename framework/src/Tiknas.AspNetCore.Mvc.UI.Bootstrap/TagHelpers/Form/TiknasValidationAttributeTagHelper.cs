using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[HtmlTargetElement(Attributes = "asp-validation-for")]
[HtmlTargetElement(Attributes = "asp-validation-summary")]
public class TiknasValidationAttributeTagHelper : TiknasTagHelper<TiknasValidationAttributeTagHelper, TiknasValidationAttributeTagHelperService>, ITransientDependency
{
    public TiknasValidationAttributeTagHelper(TiknasValidationAttributeTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
