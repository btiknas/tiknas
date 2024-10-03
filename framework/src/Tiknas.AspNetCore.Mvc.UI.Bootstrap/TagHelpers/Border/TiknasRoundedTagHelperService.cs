using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Border;

public class TiknasRoundedTagHelperService : TiknasTagHelperService<TiknasRoundedTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var roundedClass = "rounded";

        if (TagHelper.TiknasRounded != TiknasRoundedType.Default)
        {
            roundedClass += "-" + TagHelper.TiknasRounded.ToString().ToLowerInvariant().Replace("_", "");
        }

        output.Attributes.AddClass(roundedClass);
    }
}
