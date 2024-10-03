using System;
using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

[Serializable]
public class ApplicationLocalizationDto
{
    public Dictionary<string, ApplicationLocalizationResourceDto> Resources { get; set; }

    public CurrentCultureDto CurrentCulture { get; set; }

    public ApplicationLocalizationDto()
    {
        Resources = new Dictionary<string, ApplicationLocalizationResourceDto>();
        CurrentCulture = new CurrentCultureDto();
    }
}
