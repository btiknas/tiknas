using System;
using System.Threading.Tasks;
using Tiknas.Http.Modeling;

namespace Tiknas.Http.Client.DynamicProxying;

public interface IApiDescriptionCache
{
    Task<ApplicationApiDescriptionModel> GetAsync(
        string baseUrl,
        Func<Task<ApplicationApiDescriptionModel>> factory
    );
}
