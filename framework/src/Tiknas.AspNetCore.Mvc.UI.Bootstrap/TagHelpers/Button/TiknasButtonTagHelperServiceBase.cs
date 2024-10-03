using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public abstract class TiknasButtonTagHelperServiceBase<TTagHelper> : TiknasTagHelperService<TTagHelper>
    where TTagHelper : TagHelper, IButtonTagHelperBase
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        NormalizeTagMode(context, output);
        AddClasses(context, output);
        AddIcon(context, output);
        AddText(context, output);
        AddDisabled(context, output);
    }

    protected virtual void NormalizeTagMode(TagHelperContext context, TagHelperOutput output)
    {
        output.TagMode = TagMode.StartTagAndEndTag;
    }

    protected virtual void AddClasses(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("btn");

        if (TagHelper.ButtonType != TiknasButtonType.Default)
        {
            output.Attributes.AddClass("btn-" + TagHelper.ButtonType.ToString().ToLowerInvariant().Replace("_", "-"));
        }

        if (TagHelper.Size != TiknasButtonSize.Default)
        {
            output.Attributes.AddClass(TagHelper.Size.ToClassName());
        }
    }

    protected virtual void AddIcon(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Icon.IsNullOrWhiteSpace())
        {
            return;
        }

        var icon = new TagBuilder("i");
        icon.AddCssClass(GetIconClass(context, output));
        output.Content.AppendHtml(icon);
        output.Content.Append(" ");
    }

    protected virtual string GetIconClass(TagHelperContext context, TagHelperOutput output)
    {
        switch (TagHelper.IconType)
        {
            case FontIconType.FontAwesome:
                return "fa fa-" + TagHelper.Icon;
            default:
                return TagHelper.Icon!;
        }
    }

    protected virtual void AddText(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Text.IsNullOrWhiteSpace())
        {
            return;
        }

        var span = new TagBuilder("span");
        span.InnerHtml.AppendHtml(TagHelper.Text!);
        output.Content.AppendHtml(span);
    }

    protected virtual void AddDisabled(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Disabled ?? false)
        {
            output.Attributes.Add("disabled", "disabled");
        }
    }
}
