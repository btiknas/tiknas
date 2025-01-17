using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.TextTemplating;

public interface IStaticTemplateDefinitionStore
{
    Task<TemplateDefinition> GetAsync([NotNull] string name);

    Task<IReadOnlyList<TemplateDefinition>> GetAllAsync();

    Task<TemplateDefinition?> GetOrNullAsync(string name);
}
