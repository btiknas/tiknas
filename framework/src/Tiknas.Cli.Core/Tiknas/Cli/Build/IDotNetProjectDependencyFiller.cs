using System.Collections.Generic;

namespace Tiknas.Cli.Build;

public interface IDotNetProjectDependencyFiller
{
    void Fill(List<DotNetProjectInfo> projects);
}
