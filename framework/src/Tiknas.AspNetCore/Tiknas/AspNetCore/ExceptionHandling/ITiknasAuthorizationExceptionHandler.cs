using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tiknas.Authorization;

namespace Tiknas.AspNetCore.ExceptionHandling;

public interface ITiknasAuthorizationExceptionHandler
{
    Task HandleAsync(TiknasAuthorizationException exception, HttpContext httpContext);
}
