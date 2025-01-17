﻿using Localization.Resources.TiknasUi;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using System;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public class TiknasButtonTagHelperService : TiknasButtonTagHelperServiceBase<TiknasButtonTagHelper>
{
    protected const string DataBusyTextAttributeName = "data-busy-text";
    protected const string DataBusyTextIsHtmlAttributeName = "data-busy-text-is-html";

    protected IStringLocalizer<TiknasUiResource> L { get; }

    public TiknasButtonTagHelperService(IStringLocalizer<TiknasUiResource> localizer)
    {
        L = localizer;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);
        output.TagName = "button";
        AddType(context, output);
        AddBusyText(context, output);
        AddBusyTextIsHtml(context, output);
    }

    protected virtual void AddType(TagHelperContext context, TagHelperOutput output)
    {
        if (output.Attributes.ContainsName("type"))
        {
            return;
        }

        output.Attributes.Add("type", "button");
    }

    protected virtual void AddBusyText(TagHelperContext context, TagHelperOutput output)
    {
        var busyText = TagHelper.BusyText ?? L["ProcessingWithThreeDot"];
        if (busyText.IsNullOrWhiteSpace())
        {
            return;
        }

        output.Attributes.SetAttribute(DataBusyTextAttributeName, busyText);
    }

    protected virtual void AddBusyTextIsHtml(TagHelperContext context, TagHelperOutput output)
    {
        if (!TagHelper.BusyTextIsHtml)
        {
            return;
        }

        output.Attributes.SetAttribute(DataBusyTextIsHtmlAttributeName, TagHelper.BusyTextIsHtml.ToString().ToLower());
    }
}
