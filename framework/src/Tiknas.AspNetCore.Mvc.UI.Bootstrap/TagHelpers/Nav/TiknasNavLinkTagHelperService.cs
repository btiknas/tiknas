﻿using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class TiknasNavLinkTagHelperService : TiknasTagHelperService<TiknasNavLinkTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        output.TagMode = TagMode.StartTagAndEndTag;
        SetClasses(context, output);
    }

    protected virtual void SetClasses(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("nav-link");

        SetDisabledClass(context, output);
        SetActiveClass(context, output);

        output.Attributes.RemoveAll("tiknas-nav-link");
    }

    protected virtual void SetDisabledClass(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Disabled ?? false)
        {
            output.Attributes.AddClass("disabled");
        }
    }

    protected virtual void SetActiveClass(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Active ?? false)
        {
            output.Attributes.AddClass("active");
        }
    }
}
