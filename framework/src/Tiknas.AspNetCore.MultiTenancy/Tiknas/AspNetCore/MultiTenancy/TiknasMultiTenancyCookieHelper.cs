﻿using System;
using Microsoft.AspNetCore.Http;

namespace Tiknas.AspNetCore.MultiTenancy;

public static class TiknasMultiTenancyCookieHelper
{
    public static void SetTenantCookie(
        HttpContext context,
        Guid? tenantId,
        string tenantKey)
    {
        if (tenantId != null)
        {
            context.Response.Cookies.Append(
                tenantKey,
                tenantId.ToString()!,
                new CookieOptions
                {
                    Path = "/",
                    HttpOnly = false,
                    IsEssential = true,
                    Expires = DateTimeOffset.Now.AddYears(10)
                }
            );
        }
        else
        {
            context.Response.Cookies.Delete(tenantKey);
        }
    }
}
