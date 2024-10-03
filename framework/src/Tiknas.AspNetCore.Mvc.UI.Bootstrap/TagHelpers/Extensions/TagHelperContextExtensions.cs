using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Extensions;

public static class TagHelperContextExtensions
{
    public static T GetValue<T>(this TagHelperContext context, string key)
    {
        if (!context.Items.ContainsKey(key))
        {
            return default(T)!;
        }

        return (T)context.Items[key];
    }
}
