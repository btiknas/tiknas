using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Tiknas.TextTemplating.Razor;

public class TiknasRazorTemplateCSharpCompilerOptions
{
    public List<PortableExecutableReference> References { get; }

    public TiknasRazorTemplateCSharpCompilerOptions()
    {
        References = new List<PortableExecutableReference>();
    }
}
