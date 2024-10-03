using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

public class TiknasApplicationConfigurationOptions
{
    public List<IApplicationConfigurationContributor> Contributors { get; }

    public TiknasApplicationConfigurationOptions()
    {
        Contributors = new List<IApplicationConfigurationContributor>();
    }
}
