using Microsoft.AspNetCore.Mvc;
using Tiknas.Http.Modeling;

namespace Tiknas.AspNetCore.Mvc.ApiExploring;

[Area("tiknas")]
[RemoteService(Name = "tiknas")]
[Route("api/tiknas/api-definition")]
public class TiknasApiDefinitionController : TiknasController, IRemoteService
{
    protected readonly IApiDescriptionModelProvider ModelProvider;

    public TiknasApiDefinitionController(IApiDescriptionModelProvider modelProvider)
    {
        ModelProvider = modelProvider;
    }

    [HttpGet]
    public virtual ApplicationApiDescriptionModel Get(ApplicationApiDescriptionModelRequestDto model)
    {
        return ModelProvider.CreateApiModel(model);
    }
}
