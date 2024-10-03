using Tiknas.Cli.ProjectBuilding.Building.Steps;
using Tiknas.Cli.ProjectBuilding.Templates;

namespace Tiknas.Cli.ProjectBuilding.Building;

public static class ModuleProjectBuildPipelineBuilder
{
    public static ProjectBuildPipeline Build(ProjectBuildContext context)
    {
        var pipeline = new ProjectBuildPipeline(context);

        pipeline.Steps.Add(new FileEntryListReadStep());
        pipeline.Steps.Add(new ProjectReferenceReplaceStep());
        pipeline.Steps.Add(new ReplaceCommonPropsStep());
        pipeline.Steps.Add(new MakeProxyJsonFileEmbeddedStep());
        pipeline.Steps.Add(new ReplaceConfigureAwaitPropsStep());

        if (context.Module.IsPro)
        {
            pipeline.Steps.Add(new CreateAppSettingsSecretsStep());
            pipeline.Steps.Add(new LicenseCodeReplaceStep());
        }
        
        pipeline.Steps.Add(new UpdateNuGetConfigStep("/NuGet.Config"));
        pipeline.Steps.Add(new CreateProjectResultZipStep());

        return pipeline;
    }
}
