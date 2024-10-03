using System.Collections.Generic;

namespace Tiknas.Cli.Build;

public interface IDotNetProjectBuilder
{
    List<string> BuildProjects(List<DotNetProjectInfo> projects, string arguments);

    void BuildSolution(string slnPath, string arguments);
}
