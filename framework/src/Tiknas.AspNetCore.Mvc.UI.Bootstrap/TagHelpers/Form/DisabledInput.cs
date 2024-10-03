using System;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[AttributeUsage(AttributeTargets.Property)]
public class DisabledInput : Attribute
{
    public DisabledInput()
    {
    }
}
