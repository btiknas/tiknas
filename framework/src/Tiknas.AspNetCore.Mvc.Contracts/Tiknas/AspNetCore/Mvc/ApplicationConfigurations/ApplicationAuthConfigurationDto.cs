﻿using System;
using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

[Serializable]
public class ApplicationAuthConfigurationDto
{
    public Dictionary<string, bool> GrantedPolicies { get; set; }

    public ApplicationAuthConfigurationDto()
    {
        GrantedPolicies = new Dictionary<string, bool>();
    }
}
