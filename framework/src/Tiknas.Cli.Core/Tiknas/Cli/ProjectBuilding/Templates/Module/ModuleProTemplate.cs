using Tiknas.Cli.ProjectBuilding.Templates.Module;

namespace Tiknas.Cli.ProjectBuilding.Templates.MvcModule;

public class ModuleProTemplate : ModuleTemplateBase
{
    /// <summary>
    /// "module".
    /// </summary>
    public const string TemplateName = "module-pro";

    public ModuleProTemplate()
        : base(TemplateName)
    {
        DocumentUrl = "https://tiknas.io/docs/latest/solution-templates/application-module";
    }
}
