using System;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;

[Serializable]
public class ExtensionPropertyGlobalFeaturePolicyDto
{
    public string[] Features { get; set; } = default!;

    public bool RequiresAll { get; set; } = default!;
}
