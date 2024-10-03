using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ListGroup;

public class TiknasListGroupItemTagHelperService : TiknasTagHelperService<TiknasListGroupItemTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        MakeLinkIfHrefIsSet();
        SetTagNameAndAttributes(context, output);
    }

    protected virtual void SetTagNameAndAttributes(TagHelperContext context, TagHelperOutput output)
    {
        SetCommonTagNameAndAttributes(context, output);

        if (TagHelper.TagType == TiknasListItemTagType.Default)
        {
            output.TagName = "li";
        }
        else if (TagHelper.TagType == TiknasListItemTagType.Link)
        {
            output.TagName = "a";
            output.Attributes.AddClass("list-group-item-action");
            output.Attributes.Add("href", TagHelper.Href);
        }
        else if (TagHelper.TagType == TiknasListItemTagType.Button)
        {
            output.TagName = "button";
            output.Attributes.AddClass("list-group-item-action");
        }

    }

    protected virtual void SetCommonTagNameAndAttributes(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("list-group-item");

        if (TagHelper.Active ?? false)
        {
            output.Attributes.AddClass("active");
        }

        if (TagHelper.Disabled ?? false)
        {
            output.Attributes.AddClass("disabled");
        }

        if (TagHelper.Type != TiknasListItemType.Default)
        {
            output.Attributes.AddClass("list-group-item-" + TagHelper.Type.ToString().ToLowerInvariant());
        }
    }

    protected virtual void MakeLinkIfHrefIsSet()
    {
        if (!string.IsNullOrWhiteSpace(TagHelper.Href))
        {
            TagHelper.TagType = TiknasListItemTagType.Link;
        }
    }
}
