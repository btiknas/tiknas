﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.TextTemplating;

public interface ITemplateDefinitionManager
{
    [ItemNotNull]
    Task<TemplateDefinition> GetAsync([NotNull] string name);

    [ItemNotNull]
    Task<IReadOnlyList<TemplateDefinition>> GetAllAsync();

    Task<TemplateDefinition?> GetOrNullAsync(string name);
}
