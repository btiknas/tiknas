using System.Threading.Tasks;
using Tiknas.ApiVersioning;
using Tiknas.Application.Services;

namespace Tiknas.AspNetCore.Mvc.Versioning.App.v2;

public class TodoAppService : ApplicationService, ITodoAppService
{
    private readonly IRequestedApiVersion _requestedApiVersion;

    public TodoAppService(IRequestedApiVersion requestedApiVersion)
    {
        _requestedApiVersion = requestedApiVersion;
    }

    public Task<string> GetAsync(int id)
    {
        return Task.FromResult(id + "-" + GetVersionOrNone());
    }

    private string GetVersionOrNone()
    {
        return _requestedApiVersion.Current ?? "NONE";
    }
}
