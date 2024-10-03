using System.Threading.Tasks;
using Tiknas.Application.Services;

namespace Tiknas.AspNetCore.Mvc.Versioning.App.v1;

public interface ITodoAppService : IApplicationService
{
    Task<string> GetAsync(int id);
}
