﻿using System.Threading.Tasks;

namespace Tiknas.Cli.Auth;

public interface IAuthService
{
    Task<LoginInfo> GetLoginInfoAsync();

    Task LoginAsync(string userName, string password, string organizationName = null);

    Task LogoutAsync();

    Task<bool> CheckMultipleOrganizationsAsync(string username);
}
