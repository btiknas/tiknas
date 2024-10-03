using System.IO;

namespace Tiknas.Cli.ProjectModification;

public static class BlazorProjectTypeChecker
{
    public static bool IsBlazorServerProject(string blazorProjectPath)
    {
        var blazorProjectCsprojContent = File.ReadAllText(blazorProjectPath);

        return !blazorProjectCsprojContent.Contains("Microsoft.NET.Sdk.BlazorWebAssembly") &&
               blazorProjectCsprojContent.Contains("Tiknas.AspNetCore.Components.Server");
    }
}
