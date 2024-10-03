using System.Reflection;

namespace Tiknas.Modularity;

public interface IAdditionalModuleAssemblyProvider
{
    Assembly[] GetAssemblies();
}