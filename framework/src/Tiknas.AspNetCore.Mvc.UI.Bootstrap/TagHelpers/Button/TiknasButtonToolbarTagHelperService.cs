﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public class TiknasButtonToolbarTagHelperService : TiknasTagHelperService<TiknasButtonToolbarTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("btn-toolbar");
        output.Attributes.Add("role", "toolbar");
    }
}
