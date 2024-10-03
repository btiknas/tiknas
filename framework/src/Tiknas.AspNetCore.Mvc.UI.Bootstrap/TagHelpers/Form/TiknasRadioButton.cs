using System;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[AttributeUsage(AttributeTargets.Property)]
public class TiknasRadioButton : Attribute
{
    public bool Inline { get; set; } = false;

    public bool Disabled { get; set; } = false;
}
