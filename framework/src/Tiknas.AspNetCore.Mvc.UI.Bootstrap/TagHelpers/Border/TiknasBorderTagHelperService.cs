﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Border;

public class TiknasBorderTagHelperService : TiknasTagHelperService<TiknasBorderTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var borderAttributeAsString = TagHelper.TiknasBorder.ToString().ToLowerInvariant();

        var borderClass = "border" + GetBorderDirection(context, output, borderAttributeAsString) + GetExtensionIfBorderIsSubtractive(context, output, borderAttributeAsString);

        output.Attributes.AddClass(borderClass);

        SetBorderType(context, output, borderAttributeAsString);
    }

    protected virtual string GetBorderDirection(TagHelperContext context, TagHelperOutput output, string borderAttributeAsString)
    {
        if (borderAttributeAsString.Contains("top"))
        {
            return "-top";
        }
        if (borderAttributeAsString.Contains("right"))
        {
            return "-right";
        }
        if (borderAttributeAsString.Contains("left"))
        {
            return "-left";
        }
        if (borderAttributeAsString.Contains("bottom"))
        {
            return "-bottom";
        }

        return "";
    }

    protected virtual string GetExtensionIfBorderIsSubtractive(TagHelperContext context, TagHelperOutput output, string borderAttributeAsString)
    {
        if (borderAttributeAsString.Contains("_0"))
        {
            return "-0";
        }

        return "";
    }

    protected virtual void SetBorderType(TagHelperContext context, TagHelperOutput output, string borderAttributeAsString)
    {
        if (borderAttributeAsString.Contains("primary"))
        {
            output.Attributes.AddClass("border-primary");
        }
        if (borderAttributeAsString.Contains("secondary"))
        {
            output.Attributes.AddClass("border-secondary");
        }
        if (borderAttributeAsString.Contains("success"))
        {
            output.Attributes.AddClass("border-success");
        }
        if (borderAttributeAsString.Contains("danger"))
        {
            output.Attributes.AddClass("border-danger");
        }
        if (borderAttributeAsString.Contains("warning"))
        {
            output.Attributes.AddClass("border-warning");
        }
        if (borderAttributeAsString.Contains("info"))
        {
            output.Attributes.AddClass("border-info");
        }
        if (borderAttributeAsString.Contains("light"))
        {
            output.Attributes.AddClass("border-light");
        }
        if (borderAttributeAsString.Contains("dark"))
        {
            output.Attributes.AddClass("border-dark");
        }
        if (borderAttributeAsString.Contains("white"))
        {
            output.Attributes.AddClass("border-white");
        }
    }
}
