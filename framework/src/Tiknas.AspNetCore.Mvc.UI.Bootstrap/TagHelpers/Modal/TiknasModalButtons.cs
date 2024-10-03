using System;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

[Flags]
public enum TiknasModalButtons
{
    None = 0,
    Save = 1,
    Cancel = 2,
    Close = 4
}
