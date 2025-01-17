﻿using System.Collections.Generic;

namespace Tiknas.Http.Client;

public class RemoteServiceConfiguration : Dictionary<string, string?>
{
    /// <summary>
    /// Base Url.
    /// </summary>
    public string BaseUrl {
        get => this.GetOrDefault(nameof(BaseUrl))!;
        set => this[nameof(BaseUrl)] = value;
    }

    /// <summary>
    /// Version.
    /// </summary>
    public string? Version {
        get => this.GetOrDefault(nameof(Version));
        set => this[nameof(Version)] = value;
    }

    public RemoteServiceConfiguration()
    {

    }

    public RemoteServiceConfiguration(string baseUrl, string? version = null)
    {
        this[nameof(BaseUrl)] = baseUrl;
        this[nameof(Version)] = version;
    }
    
    public RemoteServiceConfiguration(RemoteServiceConfiguration configuration)
    {
        foreach (var keyValuePair in configuration)
        {
            this.Add(keyValuePair.Key, keyValuePair.Value);
        }
    }
}
