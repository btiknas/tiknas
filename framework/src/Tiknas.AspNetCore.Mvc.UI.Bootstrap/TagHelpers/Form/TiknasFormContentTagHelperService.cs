using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

public class TiknasFormContentTagHelperService : TiknasTagHelperService<TiknasFormContentTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Clear();
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Content.SetContent(TiknasFormContentPlaceHolder);
    }
}
