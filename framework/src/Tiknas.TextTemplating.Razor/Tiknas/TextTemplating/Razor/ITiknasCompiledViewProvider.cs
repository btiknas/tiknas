using System.Reflection;
using System.Threading.Tasks;

namespace Tiknas.TextTemplating.Razor;

public interface ITiknasCompiledViewProvider
{
    Task<Assembly> GetAssemblyAsync(TemplateDefinition templateDefinition);
}
