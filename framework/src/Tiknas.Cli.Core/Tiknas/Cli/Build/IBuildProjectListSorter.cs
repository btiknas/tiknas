using System.Collections.Generic;

namespace Tiknas.Cli.Build;

public interface IBuildProjectListSorter
{
    List<DotNetProjectInfo> SortByDependencies(
        List<DotNetProjectInfo> source,
        IEqualityComparer<DotNetProjectInfo> comparer = null);
}
