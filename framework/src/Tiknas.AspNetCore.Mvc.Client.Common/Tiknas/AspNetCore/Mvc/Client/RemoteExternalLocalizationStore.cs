﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations;
using Tiknas.DependencyInjection;
using Tiknas.Localization;
using Tiknas.Localization.External;

namespace Tiknas.AspNetCore.Mvc.Client;

public class RemoteExternalLocalizationStore : IExternalLocalizationStore, ITransientDependency
{
    protected ICachedApplicationConfigurationClient ConfigurationClient { get; }
    protected TiknasLocalizationOptions LocalizationOptions { get; }

    public RemoteExternalLocalizationStore(
        ICachedApplicationConfigurationClient configurationClient,
        IOptions<TiknasLocalizationOptions> localizationOptions)
    {
        ConfigurationClient = configurationClient;
        LocalizationOptions = localizationOptions.Value;
    }
    
    public virtual LocalizationResourceBase? GetResourceOrNull(string resourceName)
    {
        var configurationDto = ConfigurationClient.Get();
        return CreateLocalizationResourceFromConfigurationOrNull(resourceName, configurationDto);
    }

    public virtual async Task<LocalizationResourceBase?> GetResourceOrNullAsync(string resourceName)
    {
        var configurationDto = await ConfigurationClient.GetAsync();
        return CreateLocalizationResourceFromConfigurationOrNull(resourceName, configurationDto);
    }

    public virtual async Task<string[]> GetResourceNamesAsync()
    {
        var configurationDto = await ConfigurationClient.GetAsync();
        return configurationDto
            .Localization
            .Resources
            .Keys
            .Where(x => !LocalizationOptions.Resources.ContainsKey(x))
            .ToArray();
;    }

    public virtual async Task<LocalizationResourceBase[]> GetResourcesAsync()
    {
        var configurationDto = await ConfigurationClient.GetAsync();
        var resources = new List<LocalizationResourceBase>();
        
        foreach (var resource in configurationDto.Localization.Resources)
        {
            if (LocalizationOptions.Resources.ContainsKey(resource.Key))
            {
                continue;
            }
            
            resources.Add(CreateNonTypedLocalizationResource(resource.Key, resource.Value));
        }

        return resources.ToArray();
    }
    
    protected virtual LocalizationResourceBase? CreateLocalizationResourceFromConfigurationOrNull(
        string resourceName,
        ApplicationConfigurationDto configurationDto)
    {
        var resourceDto = configurationDto.Localization.Resources.GetOrDefault(resourceName);

        if (resourceDto == null)
        {
            return null;
        }

        return CreateNonTypedLocalizationResource(resourceName, resourceDto);
    }

    protected virtual NonTypedLocalizationResource CreateNonTypedLocalizationResource(
        string resourceName,
        ApplicationLocalizationResourceDto resourceDto)
    {
        return new NonTypedLocalizationResource(resourceName)
            .AddBaseResources(resourceDto.BaseResources);
    }
}