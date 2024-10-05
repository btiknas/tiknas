using System;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Tiknas.TextTemplating.VirtualFiles;

namespace Tiknas.TextTemplating.Razor;

public class RazorLocalizedTemplateContentReaderFactory_Tests : LocalizedTemplateContentReaderFactory_Tests<RazorTextTemplatingTestModule>
{
    public RazorLocalizedTemplateContentReaderFactory_Tests()
    {
        LocalizedTemplateContentReaderFactory = new LocalizedTemplateContentReaderFactory(
            new PhysicalFileVirtualFileProvider(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),
                    "Tiknas", "TextTemplating", "Razor"))),
            GetRequiredService<ITiknasHostEnvironment>());

        // Define the expected content
        WelcomeEmailEnglishContent = 
            "@inherits Tiknas.TextTemplating.Razor.RazorTemplatePageBase<Tiknas.TextTemplating.Razor.RazorTemplateRendererProvider_Tests.WelcomeEmailModel>" +
            Environment.NewLine +
            "Welcome @Model.Name to the tiknas.de!";

        WelcomeEmailTurkishContent = 
            "@inherits Tiknas.TextTemplating.Razor.RazorTemplatePageBase<Tiknas.TextTemplating.Razor.RazorTemplateRendererProvider_Tests.WelcomeEmailModel>" +
            Environment.NewLine +
            "Merhaba @Model.Name, tiknas.de'ya hoşgeldiniz!";
    }
}
