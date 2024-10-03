using System;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public enum TiknasButtonSize
{
    Default,
    Small,
    Medium,
    Large,
    [Obsolete("https://getbootstrap.com/docs/5.0/components/buttons/#block-buttons")]
    Block,
    [Obsolete("https://getbootstrap.com/docs/5.0/components/buttons/#block-buttons")]
    Block_Small,
    [Obsolete("https://getbootstrap.com/docs/5.0/components/buttons/#block-buttons")]
    Block_Medium,
    [Obsolete("https://getbootstrap.com/docs/5.0/components/buttons/#block-buttons")]
    Block_Large,
}
