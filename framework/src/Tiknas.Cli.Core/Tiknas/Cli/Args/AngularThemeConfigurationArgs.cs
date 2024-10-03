using Tiknas.Cli.ProjectBuilding.Building;

namespace Tiknas.Cli.Args;

public class AngularThemeConfigurationArgs 
{
    public Theme Theme { get; }

    public string ProjectName { get; }

    public string AngularFolderPath { get; }

    public AngularThemeConfigurationArgs(Theme theme, string projectName, string angularFolderPath)
    {
        Theme = theme;
        ProjectName = projectName;
        AngularFolderPath = angularFolderPath;
    }
}