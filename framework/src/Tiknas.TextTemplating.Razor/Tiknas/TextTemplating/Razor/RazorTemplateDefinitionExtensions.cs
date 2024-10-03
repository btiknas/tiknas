using JetBrains.Annotations;

namespace Tiknas.TextTemplating.Razor;

public static class RazorTemplateDefinitionExtensions
{
    public static TemplateDefinition WithRazorEngine([NotNull] this TemplateDefinition templateDefinition)
    {
        return templateDefinition.WithRenderEngine(RazorTemplateRenderingEngine.EngineName);
    }
}
