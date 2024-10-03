using Tiknas.DependencyInjection;
using Tiknas.MultiTenancy;

namespace Tiknas.AspNetCore.Components.WebAssembly;

[Dependency(ReplaceServices = true)]
public class WebAssemblyCurrentTenantAccessor : ICurrentTenantAccessor, ISingletonDependency
{
    public BasicTenantInfo? Current { get; set; }
}
