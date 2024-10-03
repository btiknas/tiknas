using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public class TiknasButtonGroupTagHelperService : TiknasTagHelperService<TiknasButtonGroupTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        AddButtonGroupClass(context, output);
        AddSizeClass(context, output);
        AddAttributes(context, output);

        output.TagName = "div";
    }

    protected virtual void AddSizeClass(TagHelperContext context, TagHelperOutput output)
    {
        switch (TagHelper.Size)
        {
            case TiknasButtonGroupSize.Default:
                break;
            case TiknasButtonGroupSize.Small:
                output.Attributes.AddClass("btn-group-sm");
                break;
            case TiknasButtonGroupSize.Medium:
                output.Attributes.AddClass("btn-group-md");
                break;
            case TiknasButtonGroupSize.Large:
                output.Attributes.AddClass("btn-group-lg");
                break;
        }
    }

    protected virtual void AddButtonGroupClass(TagHelperContext context, TagHelperOutput output)
    {
        switch (TagHelper.Direction)
        {
            case TiknasButtonGroupDirection.Horizontal:
                output.Attributes.AddClass("btn-group");
                break;
            case TiknasButtonGroupDirection.Vertical:
                output.Attributes.AddClass("btn-group-vertical");
                break;
            default:
                output.Attributes.AddClass("btn-group");
                break;
        }
    }

    protected virtual void AddAttributes(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("role", "group");
    }
}
