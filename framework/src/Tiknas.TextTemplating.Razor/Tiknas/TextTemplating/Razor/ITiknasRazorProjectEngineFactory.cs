using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;

namespace Tiknas.TextTemplating.Razor;

public interface ITiknasRazorProjectEngineFactory
{
    Task<RazorProjectEngine> CreateAsync(Action<RazorProjectEngineBuilder>? configure = null);
}
