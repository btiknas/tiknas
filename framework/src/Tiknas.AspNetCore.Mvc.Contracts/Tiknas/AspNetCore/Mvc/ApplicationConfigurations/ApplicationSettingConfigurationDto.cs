using System;
using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

[Serializable]
public class ApplicationSettingConfigurationDto
{
    public Dictionary<string, string?> Values { get; set; }

    public ApplicationSettingConfigurationDto()
    {
        Values = new Dictionary<string, string?>();
    }
}
