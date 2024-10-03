using Tiknas.Cli.ProjectBuilding.Building.Steps;
using Tiknas.Cli.ProjectBuilding.Templates;

namespace Tiknas.Cli.ProjectBuilding.Building;

public static class NpmPackageProjectBuildPipelineBuilder
{
    public static ProjectBuildPipeline Build(ProjectBuildContext context)
    {
        var pipeline = new ProjectBuildPipeline(context);

        pipeline.Steps.Add(new FileEntryListReadStep());
        pipeline.Steps.Add(new CreateProjectResultZipStep());

        return pipeline;
    }
}
