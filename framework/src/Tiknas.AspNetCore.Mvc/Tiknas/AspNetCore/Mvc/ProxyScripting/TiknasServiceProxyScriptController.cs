using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tiknas.Auditing;
using Tiknas.Http;
using Tiknas.Http.ProxyScripting;
using Tiknas.Minify.Scripts;

namespace Tiknas.AspNetCore.Mvc.ProxyScripting;

[Area("Tiknas")]
[Route("Tiknas/ServiceProxyScript")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class TiknasServiceProxyScriptController : TiknasController
{
    protected readonly IProxyScriptManager ProxyScriptManager;
    protected readonly TiknasAspNetCoreMvcOptions Options;
    protected readonly IJavascriptMinifier JavascriptMinifier;

    public TiknasServiceProxyScriptController(IProxyScriptManager proxyScriptManager,
        IOptions<TiknasAspNetCoreMvcOptions> options,
        IJavascriptMinifier javascriptMinifier)
    {
        ProxyScriptManager = proxyScriptManager;
        Options = options.Value;
        JavascriptMinifier = javascriptMinifier;
    }

    [HttpGet]
    [Produces(MimeTypes.Application.Javascript, MimeTypes.Text.Plain)]
    public virtual ActionResult GetAll(ServiceProxyGenerationModel model)
    {
        model.Normalize();

        var script = ProxyScriptManager.GetScript(model.CreateOptions());

        return Content(
            Options.MinifyGeneratedScript == true
                ? JavascriptMinifier.Minify(script)
                : script,
            MimeTypes.Application.Javascript
        );
    }
}
