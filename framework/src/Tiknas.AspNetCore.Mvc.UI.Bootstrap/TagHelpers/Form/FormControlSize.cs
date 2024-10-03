using System;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[AttributeUsage(AttributeTargets.Property)]
public class FormControlSize : Attribute
{
    public TiknasFormControlSize Size { get; set; }

    public FormControlSize(TiknasFormControlSize size)
    {
        Size = size;
    }
}
