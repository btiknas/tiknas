using System.IO;
using Microsoft.Extensions.FileProviders;
using Tiknas.TextTemplating.VirtualFiles;

namespace Tiknas.TextTemplating.Scriban;

public class ScribanLocalizedTemplateContentReaderFactory_Tests : LocalizedTemplateContentReaderFactory_Tests<ScribanTextTemplatingTestModule>
{
    public ScribanLocalizedTemplateContentReaderFactory_Tests()
    {
        LocalizedTemplateContentReaderFactory = new LocalizedTemplateContentReaderFactory(
            new PhysicalFileVirtualFileProvider(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),
                    "Tiknas", "TextTemplating", "Scriban"))),
            GetRequiredService<ITiknasHostEnvironment>());

        WelcomeEmailEnglishContent = "Welcome {{model.name}} to the tiknas.de!";
        WelcomeEmailTurkishContent = "Merhaba {{model.name}}, tiknas.de'ya hoşgeldiniz!";
    }
}
