﻿using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public class TiknasLinkButtonTagHelperService : TiknasButtonTagHelperServiceBase<TiknasLinkButtonTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);
        AddType(context, output);
        AddRole(context, output);
    }

    protected virtual void AddType(TagHelperContext context, TagHelperOutput output)
    {
        if (!output.Attributes.ContainsName("type") &&
            output.TagName.Equals("input", StringComparison.InvariantCultureIgnoreCase))
        {
            output.Attributes.Add("type", "button");
        }
    }

    protected virtual void AddRole(TagHelperContext context, TagHelperOutput output)
    {
        if (!output.Attributes.ContainsName("role"))
        {
            output.Attributes.Add("role", "button");
        }
    }
}
