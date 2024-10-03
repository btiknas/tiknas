using System;
using System.Collections.Generic;
using Tiknas.Collections;

namespace Tiknas.TextTemplating;

public class TiknasTextTemplatingOptions
{
    public ITypeList<ITemplateDefinitionProvider> DefinitionProviders { get; }
    public ITypeList<ITemplateContentContributor> ContentContributors { get; }
    public IDictionary<string, Type> RenderingEngines { get; }

    public string? DefaultRenderingEngine { get; set; }

    public HashSet<string> DeletedTemplates { get; }

    public TiknasTextTemplatingOptions()
    {
        DefinitionProviders = new TypeList<ITemplateDefinitionProvider>();
        ContentContributors = new TypeList<ITemplateContentContributor>();
        RenderingEngines = new Dictionary<string, Type>();
        DeletedTemplates = new HashSet<string>();
    }
}
