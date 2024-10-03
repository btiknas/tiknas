using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tiknas;
using Tiknas.AspNetCore;
using Tiknas.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc.MultiTenancy;

namespace Pages.Tiknas.MultiTenancy;

[Area("tiknas")]
[RemoteService(Name = "tiknas")]
[Route("api/tiknas/multi-tenancy")]
public class TiknasTenantController : TiknasControllerBase, ITiknasTenantAppService
{
    private readonly ITiknasTenantAppService _tiknasTenantAppService;

    public TiknasTenantController(ITiknasTenantAppService tiknasTenantAppService)
    {
        _tiknasTenantAppService = tiknasTenantAppService;
    }

    [HttpGet]
    [Route("tenants/by-name/{name}")]
    public virtual async Task<FindTenantResultDto> FindTenantByNameAsync(string name)
    {
        return await _tiknasTenantAppService.FindTenantByNameAsync(name);
    }

    [HttpGet]
    [Route("tenants/by-id/{id}")]
    public virtual async Task<FindTenantResultDto> FindTenantByIdAsync(Guid id)
    {
        return await _tiknasTenantAppService.FindTenantByIdAsync(id);
    }
}
