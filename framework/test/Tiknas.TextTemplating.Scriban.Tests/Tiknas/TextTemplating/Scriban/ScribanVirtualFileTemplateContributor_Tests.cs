using Tiknas.TextTemplating.VirtualFiles;

namespace Tiknas.TextTemplating.Scriban;

public class ScribanVirtualFileTemplateContributor_Tests : VirtualFileTemplateContributor_Tests<ScribanTextTemplatingTestModule>
{
    public ScribanVirtualFileTemplateContributor_Tests()
    {
        WelcomeEmailEnglishContent = "Welcome {{model.name}} to the tiknas.de!";
        WelcomeEmailTurkishContent = "Merhaba {{model.name}}, tiknas.de'ya hoşgeldiniz!";
        ForgotPasswordEmailEnglishContent = "{{L \"HelloText\" model.name}}, {{L \"HowAreYou\" }}. Please click to the following link to get an email to reset your password!";
    }
}
