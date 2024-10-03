using System;
using System.Collections.Generic;
using System.Linq;
using Tiknas.Cli.ProjectBuilding.Building;
using Tiknas.Cli.ProjectBuilding.Files;

namespace Tiknas.Cli.ProjectBuilding.Templates;

public class UpdateNuGetConfigStep : ProjectBuildPipelineStep
{
    private readonly string _nugetConfigFilePath;

    public UpdateNuGetConfigStep(string nugetConfigFilePath)
    {
        _nugetConfigFilePath = nugetConfigFilePath;
    }

    public override void Execute(ProjectBuildContext context)
    {
        var file = context.Files.FirstOrDefault(f => f.Name == _nugetConfigFilePath);
        if (file == null)
        {
            return;
        }

        var apiKey = context.BuildArgs.ExtraProperties.GetOrDefault("api-key");
        if (apiKey.IsNullOrEmpty())
        {
            return;
        }

        const string placeHolder = "<!-- {TIKNAS_COMMERCIAL_NUGET_SOURCE} -->";
        var nugetSourceTag = $"<add key=\"TIKNAS Commercial NuGet Source\" value=\"https://nuget.tiknas.io/{apiKey}/v3/index.json\" />";

        file.ReplaceText(placeHolder, nugetSourceTag);
    }
}
