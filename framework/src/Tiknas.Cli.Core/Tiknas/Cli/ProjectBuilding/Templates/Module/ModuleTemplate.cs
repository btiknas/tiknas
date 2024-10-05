using Tiknas.Cli.ProjectBuilding.Templates.Module;

namespace Tiknas.Cli.ProjectBuilding.Templates.MvcModule;

public class ModuleTemplate : ModuleTemplateBase
{
    /// <summary>
    /// "module".
    /// </summary>
    public const string TemplateName = "module";

    public ModuleTemplate()
        : base(TemplateName)
    {
        DocumentUrl = "https://tiknas.de/docs/latest/solution-templates/application-module";
    }
}
