using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations;
using Tiknas.Auditing;
using Tiknas.Http;
using Tiknas.Json;
using Tiknas.Localization;
using Tiknas.Minify.Scripts;

namespace Tiknas.AspNetCore.Mvc.Localization;

[Area("Tiknas")]
[Route("Tiknas/ApplicationLocalizationScript")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class TiknasApplicationLocalizationScriptController : TiknasController
{
    protected TiknasApplicationLocalizationAppService LocalizationAppService { get; }
    protected TiknasAspNetCoreMvcOptions Options { get; }
    protected IJsonSerializer JsonSerializer { get; }
    protected IJavascriptMinifier JavascriptMinifier { get; }

    public TiknasApplicationLocalizationScriptController(
        TiknasApplicationLocalizationAppService localizationAppService,
        IOptions<TiknasAspNetCoreMvcOptions> options,
        IJsonSerializer jsonSerializer,
        IJavascriptMinifier javascriptMinifier)
    {
        LocalizationAppService = localizationAppService;
        JsonSerializer = jsonSerializer;
        JavascriptMinifier = javascriptMinifier;
        Options = options.Value;
    }

    [HttpGet]
    [Produces(MimeTypes.Application.Javascript, MimeTypes.Text.Plain)]
    public virtual async Task<ActionResult> GetAsync(ApplicationLocalizationRequestDto input)
    {
        var script = CreateScript(
            await LocalizationAppService.GetAsync(input)
        );

        return Content(
            Options.MinifyGeneratedScript == true
                ? JavascriptMinifier.Minify(script)
                : script,
            MimeTypes.Application.Javascript
        );
    }

    protected virtual string CreateScript(ApplicationLocalizationDto localizationDto)
    {
        var script = new StringBuilder();

        script.AppendLine("(function(){");
        script.AppendLine();
        script.AppendLine(
            $"$.extend(true, tiknas.localization, {JsonSerializer.Serialize(localizationDto, indented: true)})");
        script.AppendLine();
        script.Append("})();");

        return script.ToString();
    }
}
