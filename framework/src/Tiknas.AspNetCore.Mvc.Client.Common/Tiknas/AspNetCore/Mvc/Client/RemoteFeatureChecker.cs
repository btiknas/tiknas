﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Tiknas.Features;

namespace Tiknas.AspNetCore.Mvc.Client;

public class RemoteFeatureChecker : FeatureCheckerBase
{
    protected ICachedApplicationConfigurationClient ConfigurationClient { get; }

    public RemoteFeatureChecker(ICachedApplicationConfigurationClient configurationClient)
    {
        ConfigurationClient = configurationClient;
    }

    public override async Task<string?> GetOrNullAsync(string name)
    {
        var configuration = await ConfigurationClient.GetAsync();
        return configuration.Features.Values.GetOrDefault(name);
    }
}
