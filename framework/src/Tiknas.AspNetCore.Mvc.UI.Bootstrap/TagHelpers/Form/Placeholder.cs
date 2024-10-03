using System;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[AttributeUsage(AttributeTargets.Property)]
public class Placeholder : Attribute
{
    public string Value { get; set; }

    public Placeholder(string value)
    {
        Value = value;
    }
}
