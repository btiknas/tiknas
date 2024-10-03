using System;
using System.Threading.Tasks;
using Tiknas.Application.Services;

namespace Tiknas.AspNetCore.Mvc.MultiTenancy;

public interface ITiknasTenantAppService : IApplicationService
{
    Task<FindTenantResultDto> FindTenantByNameAsync(string name);

    Task<FindTenantResultDto> FindTenantByIdAsync(Guid id);
}
