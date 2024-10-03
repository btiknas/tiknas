using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace Tiknas.AspNetCore.Mvc.Localization;

public class TiknasMvcDataAnnotationsLocalizationOptions
{
    public IDictionary<Assembly, Type> AssemblyResources { get; }

    public TiknasMvcDataAnnotationsLocalizationOptions()
    {
        AssemblyResources = new Dictionary<Assembly, Type>();
    }

    public void AddAssemblyResource(
        [NotNull] Type resourceType,
        params Assembly[] assemblies)
    {
        if (assemblies.IsNullOrEmpty())
        {
            assemblies = new[] { resourceType.Assembly };
        }

        foreach (var assembly in assemblies)
        {
            AssemblyResources[assembly] = resourceType;
        }
    }
}
