using System.Collections.Generic;

namespace Tiknas.Cli.Build;

public interface IChangedProjectFinder
{
    List<DotNetProjectInfo> FindByRepository(DotNetProjectBuildConfig buildConfig);
}
