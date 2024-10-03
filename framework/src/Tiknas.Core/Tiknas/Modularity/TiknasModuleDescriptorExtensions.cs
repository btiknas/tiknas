using System;
using System.Linq;
using System.Reflection;

namespace Tiknas.Modularity;

public static class TiknasModuleDescriptorExtensions
{
    public static Assembly[] GetAdditionalAssemblies(this ITiknasModuleDescriptor module)
    {
        return module.AllAssemblies.Length <= 1
            ? Array.Empty<Assembly>()
            : module.AllAssemblies.Where(x => x != module.Assembly).ToArray();
    }
}