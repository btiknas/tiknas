using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc.UI.RazorPages;

namespace Tiknas.AspNetCore.Mvc.Authorization;

[Authorize]
public class AuthTestPage : TiknasPageModel
{
    public static Guid FakeUserId { get; } = Guid.NewGuid();

    public ActionResult OnGet()
    {
        return Content("OK");
    }
}
