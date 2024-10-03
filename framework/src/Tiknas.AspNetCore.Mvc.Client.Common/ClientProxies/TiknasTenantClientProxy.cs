// This file is part of TiknasTenantClientProxy, you can customize it here
// ReSharper disable once CheckNamespace

using Tiknas;
using Tiknas.DependencyInjection;

namespace Pages.Tiknas.MultiTenancy.ClientProxies;

[RemoteService(false)]
[DisableConventionalRegistration]
public partial class TiknasTenantClientProxy
{
}

