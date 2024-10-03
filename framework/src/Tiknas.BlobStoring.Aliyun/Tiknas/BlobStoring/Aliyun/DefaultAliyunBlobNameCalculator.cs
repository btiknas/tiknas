using Tiknas.DependencyInjection;
using Tiknas.MultiTenancy;

namespace Tiknas.BlobStoring.Aliyun;

public class DefaultAliyunBlobNameCalculator : IAliyunBlobNameCalculator, ITransientDependency
{
    protected ICurrentTenant CurrentTenant { get; }

    public DefaultAliyunBlobNameCalculator(ICurrentTenant currentTenant)
    {
        CurrentTenant = currentTenant;
    }

    public virtual string Calculate(BlobProviderArgs args)
    {
        return CurrentTenant.Id == null
            ? $"host/{args.BlobName}"
            : $"tenants/{CurrentTenant.Id.Value:D}/{args.BlobName}";
    }
}
