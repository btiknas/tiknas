using Microsoft.AspNetCore.Cors.Infrastructure;
using Tiknas.Http;
namespace Microsoft.AspNetCore.Cors;

public static class TiknasCorsPolicyBuilderExtensions
{
    public static CorsPolicyBuilder WithTiknasExposedHeaders(this CorsPolicyBuilder corsPolicyBuilder)
    {
        return corsPolicyBuilder.WithExposedHeaders(TiknasHttpConsts.TiknasErrorFormat).WithExposedHeaders(TiknasHttpConsts.TiknasTenantResolveError);
    }
}
