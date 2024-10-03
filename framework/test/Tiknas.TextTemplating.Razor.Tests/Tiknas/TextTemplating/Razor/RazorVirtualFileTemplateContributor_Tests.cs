using System;
using Tiknas.TextTemplating.VirtualFiles;

namespace Tiknas.TextTemplating.Razor;

public class RazorVirtualFileTemplateContributor_Tests : VirtualFileTemplateContributor_Tests<RazorTextTemplatingTestModule>
{
    public RazorVirtualFileTemplateContributor_Tests()
    {
        WelcomeEmailEnglishContent = "@inherits Tiknas.TextTemplating.Razor.RazorTemplatePageBase<Tiknas.TextTemplating.Razor.RazorTemplateRendererProvider_Tests.WelcomeEmailModel>" +
                                     Environment.NewLine +
                                     "Welcome @Model.Name to the tiknas.io!";

        WelcomeEmailTurkishContent = "@inherits Tiknas.TextTemplating.Razor.RazorTemplatePageBase<Tiknas.TextTemplating.Razor.RazorTemplateRendererProvider_Tests.WelcomeEmailModel>" +
                                     Environment.NewLine +
                                     "Merhaba @Model.Name, tiknas.io'ya hoşgeldiniz!";

        ForgotPasswordEmailEnglishContent = "@inherits Tiknas.TextTemplating.Razor.RazorTemplatePageBase<Tiknas.TextTemplating.Razor.RazorTemplateRendererProvider_Tests.ForgotPasswordEmailModel>" +
                                            Environment.NewLine +
                                            "@{" +
                                            Environment.NewLine +
                                            "    var url = @\"https://tiknas.io/Account/ResetPassword\";" +
                                            Environment.NewLine +
                                            "}" +
                                            Environment.NewLine +
                                            "@Localizer[\"HelloText\", Model.Name], @Localizer[\"HowAreYou\"]. Please click to the following link to get an email to reset your password!<a target=\"_blank\" href=\"@url\">Reset your password</a>";
    }
}
