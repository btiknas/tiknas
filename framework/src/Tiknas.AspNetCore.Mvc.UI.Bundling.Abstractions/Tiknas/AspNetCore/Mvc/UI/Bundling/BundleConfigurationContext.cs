using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling;

public class BundleConfigurationContext : IBundleConfigurationContext
{
    public List<BundleFile> Files { get; }

    public IFileProvider FileProvider { get; }

    public IServiceProvider ServiceProvider { get; }

    public ITiknasLazyServiceProvider LazyServiceProvider { get; }

    public BundleConfigurationContext(IServiceProvider serviceProvider, IFileProvider fileProvider)
    {
        Files = new List<BundleFile>();
        ServiceProvider = serviceProvider;
        LazyServiceProvider = ServiceProvider.GetRequiredService<ITiknasLazyServiceProvider>();
        FileProvider = fileProvider;
    }
}
