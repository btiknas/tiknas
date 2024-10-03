using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Mvc.UI.RazorPages;
using Tiknas.Auditing;

namespace Tiknas.AspNetCore.Mvc.Auditing;

public class AuditTestPage : TiknasPageModel
{
    private readonly TiknasAuditingOptions _options;

    public AuditTestPage(IOptions<TiknasAuditingOptions> options)
    {
        _options = options.Value;
    }

    public void OnGet()
    {

    }

    public IActionResult OnGetAuditSuccessForGetRequests()
    {
        return new OkResult();
    }

    public IActionResult OnGetAuditFailForGetRequests()
    {
        throw new UserFriendlyException("Exception occurred!");
    }

    public ObjectResult OnGetAuditFailForGetRequestsReturningObject()
    {
        throw new UserFriendlyException("Exception occurred!");
    }

    public IActionResult OnGetAuditActivateFailed([FromServices] TiknasAuditingOptions options)
    {
        return new OkResult();
    }
}
