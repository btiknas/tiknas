using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tiknas;
using Tiknas.AspNetCore.MultiTenancy;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Tiknas.AspNetCore.Mvc.UI.MultiTenancy.Localization;
using Tiknas.AspNetCore.Mvc.UI.RazorPages;
using Tiknas.MultiTenancy;

namespace Pages.Tiknas.MultiTenancy;

public class TenantSwitchModalModel : TiknasPageModel
{
    [BindProperty]
    public TenantInfoModel Input { get; set; } = default!;

    protected ITenantStore TenantStore { get; }
    protected ITenantNormalizer TenantNormalizer { get; }
    protected TiknasAspNetCoreMultiTenancyOptions Options { get; }

    public TenantSwitchModalModel(
        ITenantStore tenantStore,
        ITenantNormalizer tenantNormalizer,
        IOptions<TiknasAspNetCoreMultiTenancyOptions> options)
    {
        TenantStore = tenantStore;
        TenantNormalizer = tenantNormalizer;
        Options = options.Value;
        LocalizationResourceType = typeof(TiknasUiMultiTenancyResource);
    }

    public virtual async Task OnGetAsync()
    {
        Input = new TenantInfoModel();

        if (CurrentTenant.IsAvailable)
        {
            var tenant = await TenantStore.FindAsync(CurrentTenant.GetId());
            Input.Name = tenant?.Name;
        }
    }

    public virtual async Task OnPostAsync()
    {
        Guid? tenantId = null;
        if (!Input.Name.IsNullOrEmpty())
        {
            var tenant = await TenantStore.FindAsync(TenantNormalizer.NormalizeName(Input.Name!)!);
            if (tenant == null)
            {
                throw new UserFriendlyException(L["GivenTenantIsNotExist", Input.Name!]);
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L["GivenTenantIsNotAvailable", Input.Name!]);
            }

            tenantId = tenant.Id;
        }

        TiknasMultiTenancyCookieHelper.SetTenantCookie(HttpContext, tenantId, Options.TenantKey);
    }

    public class TenantInfoModel
    {
        [InputInfoText("SwitchTenantHint")]
        public string? Name { get; set; }
    }
}
