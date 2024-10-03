using System;
using Tiknas.Cli.Args;
using Tiknas.Cli.Utils;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.ProjectModification;

public class AngularThemeConfigurer : ITransientDependency
{
    private readonly ICmdHelper _cmdHelper;

    public AngularThemeConfigurer(ICmdHelper cmdHelper)
    {
        _cmdHelper = cmdHelper;
    }

    public void Configure(AngularThemeConfigurationArgs args)
    {
        if (args.ProjectName.IsNullOrEmpty() || args.AngularFolderPath.IsNullOrEmpty())
        {
            return;
        }
        
        var command = "npx ng g @tiknas/ng.schematics:change-theme " +
                      $"--name {(int)args.Theme} " +
                      $"--target-project {args.ProjectName}";
        
        _cmdHelper.RunCmd(command, workingDirectory: args.AngularFolderPath);
    }
}