using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Tiknas.Content;
using Tiknas.Http.Modeling;

namespace Tiknas.AspNetCore.Mvc.Conventions;

public class TiknasConventionalControllerOptions
{
    public ConventionalControllerSettingList ConventionalControllerSettings { get; }

    public List<Type> FormBodyBindingIgnoredTypes { get; }

    /// <summary>
    /// Set true to use the old style URL path style.
    /// Default: false.
    /// </summary>
    public bool UseV3UrlStyle { get; set; }

    public string[] IgnoredUrlSuffixesInControllerNames { get; set; } = new[] { "Integration" };

    public TiknasConventionalControllerOptions()
    {
        ConventionalControllerSettings = new ConventionalControllerSettingList();

        FormBodyBindingIgnoredTypes = new List<Type>
            {
                typeof(IFormFile),
                typeof(IRemoteStreamContent)
            };
    }

    public TiknasConventionalControllerOptions Create(
        Assembly assembly,
        Action<ConventionalControllerSetting>? optionsAction = null)
    {
        var setting = new ConventionalControllerSetting(
            assembly,
            ModuleApiDescriptionModel.DefaultRootPath,
            ModuleApiDescriptionModel.DefaultRemoteServiceName
        );

        optionsAction?.Invoke(setting);
        setting.Initialize();
        ConventionalControllerSettings.Add(setting);
        return this;
    }
}
