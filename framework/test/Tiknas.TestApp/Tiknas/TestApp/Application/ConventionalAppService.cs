using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tiknas.Application.Services;
using Tiknas.DependencyInjection;
using Tiknas.Uow;

namespace Tiknas.TestApp.Application;

public class ConventionalAppService : IApplicationService, ITransientDependency
{
    [Authorize]
    [UnitOfWork]
    public virtual Task GetAsync()
    {
        return Task.CompletedTask;
    }
}
