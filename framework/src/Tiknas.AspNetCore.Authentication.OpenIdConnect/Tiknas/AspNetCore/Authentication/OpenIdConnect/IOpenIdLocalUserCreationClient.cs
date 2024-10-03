using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Tiknas.AspNetCore.Authentication.OpenIdConnect;

public interface IOpenIdLocalUserCreationClient
{
    Task CreateOrUpdateAsync(TokenValidatedContext tokenValidatedContext);
}