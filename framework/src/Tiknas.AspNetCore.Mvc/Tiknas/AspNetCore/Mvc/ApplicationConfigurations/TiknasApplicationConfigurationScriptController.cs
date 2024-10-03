using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Mvc.AntiForgery;
using Tiknas.Auditing;
using Tiknas.Http;
using Tiknas.Json;
using Tiknas.Minify.Scripts;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

[Area("Tiknas")]
[Route("Tiknas/ApplicationConfigurationScript")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class TiknasApplicationConfigurationScriptController : TiknasController
{
    protected readonly TiknasApplicationConfigurationAppService ConfigurationAppService;
    protected readonly IJsonSerializer JsonSerializer;
    protected readonly TiknasAspNetCoreMvcOptions Options;
    protected readonly IJavascriptMinifier JavascriptMinifier;
    protected readonly ITiknasAntiForgeryManager AntiForgeryManager;

    public TiknasApplicationConfigurationScriptController(
        TiknasApplicationConfigurationAppService configurationAppService,
        IJsonSerializer jsonSerializer,
        IOptions<TiknasAspNetCoreMvcOptions> options,
        IJavascriptMinifier javascriptMinifier,
        ITiknasAntiForgeryManager antiForgeryManager)
    {
        ConfigurationAppService = configurationAppService;
        JsonSerializer = jsonSerializer;
        Options = options.Value;
        JavascriptMinifier = javascriptMinifier;
        AntiForgeryManager = antiForgeryManager;
    }

    [HttpGet]
    [Produces(MimeTypes.Application.Javascript, MimeTypes.Text.Plain)]
    public virtual async Task<ActionResult> Get()
    {
        var script = CreateTiknasExtendScript(
            await ConfigurationAppService.GetAsync(
                new ApplicationConfigurationRequestOptions {
                    IncludeLocalizationResources = false
                }
            )
        );

        AntiForgeryManager.SetCookie();

        return Content(
            Options.MinifyGeneratedScript == true
                ? JavascriptMinifier.Minify(script)
                : script,
            MimeTypes.Application.Javascript
        );
    }

    protected virtual string CreateTiknasExtendScript(ApplicationConfigurationDto config)
    {
        var script = new StringBuilder();

        script.AppendLine("(function(){");
        script.AppendLine();
        script.AppendLine($"$.extend(true, tiknas, {JsonSerializer.Serialize(config, indented: true)})");
        script.AppendLine();
        script.AppendLine("tiknas.event.trigger('tiknas.configurationInitialized');");
        script.AppendLine();
        script.Append("})();");

        return script.ToString();
    }
}
