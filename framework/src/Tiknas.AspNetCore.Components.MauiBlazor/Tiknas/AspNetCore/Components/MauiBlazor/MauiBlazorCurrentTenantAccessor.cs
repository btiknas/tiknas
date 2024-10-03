using Tiknas.DependencyInjection;
using Tiknas.MultiTenancy;

namespace Tiknas.AspNetCore.Components.MauiBlazor;

[Dependency(ReplaceServices = true)]
public class MauiBlazorCurrentTenantAccessor : ICurrentTenantAccessor, ISingletonDependency
{
    public BasicTenantInfo? Current { get; set; }
}
