﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class TiknasDropdownItemTagHelperService : TiknasTagHelperService<TiknasDropdownItemTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        output.Attributes.AddClass("dropdown-item");
        output.TagMode = TagMode.StartTagAndEndTag;

        SetActiveClassIfActive(context, output);
        SetDisabledClassIfDisabled(context, output);
    }

    protected virtual void SetActiveClassIfActive(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Active ?? false)
        {
            output.Attributes.AddClass("active");
        }
    }

    protected virtual void SetDisabledClassIfDisabled(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Disabled ?? false)
        {
            output.Attributes.AddClass("disabled");
        }
    }
}
