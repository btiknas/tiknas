﻿using System.Security.Claims;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.Authorization.Permissions;

public interface IPermissionChecker
{
    Task<bool> IsGrantedAsync([NotNull] string name);

    Task<bool> IsGrantedAsync(ClaimsPrincipal? claimsPrincipal, [NotNull] string name);

    Task<MultiplePermissionGrantResult> IsGrantedAsync([NotNull] string[] names);

    Task<MultiplePermissionGrantResult> IsGrantedAsync(ClaimsPrincipal? claimsPrincipal, [NotNull] string[] names);
}
