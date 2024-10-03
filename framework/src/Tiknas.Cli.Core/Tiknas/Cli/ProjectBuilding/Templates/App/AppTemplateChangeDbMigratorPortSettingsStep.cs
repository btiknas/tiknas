using Tiknas.Cli.ProjectBuilding.Building;
using Tiknas.Cli.ProjectBuilding.Files;

namespace Tiknas.Cli.ProjectBuilding.Templates.App;

public class AppTemplateChangeDbMigratorPortSettingsStep : ProjectBuildPipelineStep
{
    public string AuthServerPort { get; }

    /// <param name="authServerPort"></param>
    public AppTemplateChangeDbMigratorPortSettingsStep(
        string authServerPort)
    {
        AuthServerPort = authServerPort;
    }

    public override void Execute(ProjectBuildContext context)
    {
        context
            .GetFile("/aspnet-core/src/MyCompanyName.MyProjectName.DbMigrator/appsettings.json")
            .ReplaceText("44305", AuthServerPort);
    }
}
