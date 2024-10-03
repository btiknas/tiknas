using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tiknas.Authorization;

namespace Tiknas.AspNetCore.Mvc.ExceptionHandling;

[Route("api/exception-test")]
public class ExceptionTestController : TiknasController
{
    [HttpGet]
    [Route("UserFriendlyException1")]
    public void UserFriendlyException1()
    {
        throw new UserFriendlyException("This is a sample exception!");
    }

    [HttpGet]
    [Route("UserFriendlyException2")]
    public ActionResult UserFriendlyException2()
    {
        throw new UserFriendlyException("This is a sample exception!");
    }

    [HttpGet]
    [Route("TiknasAuthorizationException")]
    public void TiknasAuthorizationException()
    {
        throw new TiknasAuthorizationException("This is a sample exception!");
    }

    [HttpGet]
    [Route("ExceptionOnUowSaveChange")]
    public Task<string> ExceptionOnUowSaveChangeAsync()
    {
        return Task.FromResult("OK");
    }
}
