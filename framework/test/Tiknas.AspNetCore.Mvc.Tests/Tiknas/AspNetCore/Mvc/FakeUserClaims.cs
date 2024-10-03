using System.Collections.Generic;
using System.Security.Claims;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc;

public class FakeUserClaims : ISingletonDependency
{
    public List<Claim> Claims { get; } = new List<Claim>();
}
