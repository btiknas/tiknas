using System;
using System.Threading.Tasks;
using Tiknas.Application.Services;
using Tiknas.AspNetCore.Mvc.MultiTenancy;
using Tiknas.MultiTenancy;

namespace Pages.Tiknas.MultiTenancy;

public class TiknasTenantAppService : ApplicationService, ITiknasTenantAppService
{
    protected ITenantStore TenantStore { get; }
    protected ITenantNormalizer TenantNormalizer { get; }

    public TiknasTenantAppService(ITenantStore tenantStore, ITenantNormalizer tenantNormalizer)
    {
        TenantStore = tenantStore;
        TenantNormalizer = tenantNormalizer;
    }

    public virtual async Task<FindTenantResultDto> FindTenantByNameAsync(string name)
    {
        var tenant = await TenantStore.FindAsync(TenantNormalizer.NormalizeName(name)!);

        if (tenant == null)
        {
            return new FindTenantResultDto { Success = false };
        }

        return new FindTenantResultDto
        {
            Success = true,
            TenantId = tenant.Id,
            Name = tenant.Name,
            NormalizedName = tenant.NormalizedName,
            IsActive = tenant.IsActive
        };
    }

    public virtual async Task<FindTenantResultDto> FindTenantByIdAsync(Guid id)
    {
        var tenant = await TenantStore.FindAsync(id);

        if (tenant == null)
        {
            return new FindTenantResultDto { Success = false };
        }

        return new FindTenantResultDto
        {
            Success = true,
            TenantId = tenant.Id,
            Name = tenant.Name,
            NormalizedName = tenant.NormalizedName,
            IsActive = tenant.IsActive
        };
    }
}
