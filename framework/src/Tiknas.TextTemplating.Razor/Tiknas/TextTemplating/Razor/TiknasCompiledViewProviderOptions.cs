using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Tiknas.TextTemplating.Razor;

public class TiknasCompiledViewProviderOptions
{
    public Dictionary<string, List<PortableExecutableReference>> TemplateReferences { get; }

    public TiknasCompiledViewProviderOptions()
    {
        TemplateReferences = new Dictionary<string, List<PortableExecutableReference>>();
    }
}
