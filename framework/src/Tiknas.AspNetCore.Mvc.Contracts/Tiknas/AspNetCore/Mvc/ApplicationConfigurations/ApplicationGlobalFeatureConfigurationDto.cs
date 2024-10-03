using System;
using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

[Serializable]
public class ApplicationGlobalFeatureConfigurationDto
{
    public HashSet<string> EnabledFeatures { get; set; }

    public ApplicationGlobalFeatureConfigurationDto()
    {
        EnabledFeatures = new HashSet<string>();
    }
}
