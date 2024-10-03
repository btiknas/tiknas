using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.RequestLocalization;
using Tiknas.Auditing;
using Tiknas.Localization;

namespace Tiknas.AspNetCore.Mvc.Localization;

[Area("Tiknas")]
[Route("Tiknas/Languages/[action]")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class TiknasLanguagesController : TiknasController
{
    protected IQueryStringCultureReplacement QueryStringCultureReplacement { get; }

    public TiknasLanguagesController(IQueryStringCultureReplacement queryStringCultureReplacement)
    {
        QueryStringCultureReplacement = queryStringCultureReplacement;
    }

    [HttpGet]
    public virtual async Task<IActionResult> Switch(string culture, string uiCulture = "", string returnUrl = "")
    {
        if (!CultureHelper.IsValidCultureCode(culture))
        {
            throw new TiknasException("The selected culture is not valid! Make sure you enter a valid culture code.");
        }

        if (!CultureHelper.IsValidCultureCode(uiCulture))
        {
            throw new TiknasException("The selected uiCulture is not valid! Make sure you enter a valid culture code.");
        }

        TiknasRequestCultureCookieHelper.SetCultureCookie(
            HttpContext,
            new RequestCulture(culture, uiCulture)
        );

        HttpContext.Items[TiknasRequestLocalizationMiddleware.HttpContextItemName] = true;

        var context = new QueryStringCultureReplacementContext(HttpContext, new RequestCulture(culture, uiCulture), returnUrl);
        await QueryStringCultureReplacement.ReplaceAsync(context);

        if (!string.IsNullOrWhiteSpace(context.ReturnUrl))
        {
            return Redirect(GetRedirectUrl(context.ReturnUrl));
        }

        return Redirect("~/");
    }

    protected virtual string GetRedirectUrl(string returnUrl)
    {
        if (returnUrl.IsNullOrEmpty())
        {
            return "~/";
        }

        if (Url.IsLocalUrl(returnUrl))
        {
            return returnUrl;
        }

        return "~/";
    }
}
