using System.Threading.Tasks;

namespace Tiknas.TextTemplating.VirtualFiles;

public interface ILocalizedTemplateContentReaderFactory
{
    Task<ILocalizedTemplateContentReader> CreateAsync(TemplateDefinition templateDefinition);
}
