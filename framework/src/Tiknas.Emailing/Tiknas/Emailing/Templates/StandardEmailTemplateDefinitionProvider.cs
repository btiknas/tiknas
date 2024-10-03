using Tiknas.Emailing.Localization;
using Tiknas.Localization;
using Tiknas.TextTemplating;

namespace Tiknas.Emailing.Templates;

public class StandardEmailTemplateDefinitionProvider : TemplateDefinitionProvider
{
    public override void Define(ITemplateDefinitionContext context)
    {
        context.Add(
            new TemplateDefinition(
                StandardEmailTemplates.Layout,
                displayName: LocalizableString.Create<EmailingResource>("TextTemplate:StandardEmailTemplates.Layout"),
                isLayout: true
            ).WithVirtualFilePath("/Tiknas/Emailing/Templates/Layout.tpl", true)
        );

        context.Add(
            new TemplateDefinition(
                StandardEmailTemplates.Message,
                displayName: LocalizableString.Create<EmailingResource>("TextTemplate:StandardEmailTemplates.Message"),
                layout: StandardEmailTemplates.Layout
            ).WithVirtualFilePath("/Tiknas/Emailing/Templates/Message.tpl", true)
        );
    }
}
