using System.Collections.Generic;

namespace Tiknas.Cli.ProjectModification;

public class ModuleWithMastersInfo : ModuleInfo
{
    public List<ModuleWithMastersInfo> MasterModuleInfos { get; set; }
}
