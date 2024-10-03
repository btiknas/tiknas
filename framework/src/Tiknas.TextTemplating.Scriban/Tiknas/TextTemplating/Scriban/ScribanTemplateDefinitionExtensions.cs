using JetBrains.Annotations;

namespace Tiknas.TextTemplating.Scriban;

public static class ScribanTemplateDefinitionExtensions
{
    public static TemplateDefinition WithScribanEngine([NotNull] this TemplateDefinition templateDefinition)
    {
        return templateDefinition.WithRenderEngine(ScribanTemplateRenderingEngine.EngineName);
    }
}
