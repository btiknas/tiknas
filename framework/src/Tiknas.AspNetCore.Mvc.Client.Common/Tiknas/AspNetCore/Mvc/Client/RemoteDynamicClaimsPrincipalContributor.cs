using Tiknas.DependencyInjection;
using Tiknas.Security.Claims;

namespace Tiknas.AspNetCore.Mvc.Client;

[DisableConventionalRegistration]
public class RemoteDynamicClaimsPrincipalContributor : RemoteDynamicClaimsPrincipalContributorBase<RemoteDynamicClaimsPrincipalContributor, RemoteDynamicClaimsPrincipalContributorCache>
{

}
