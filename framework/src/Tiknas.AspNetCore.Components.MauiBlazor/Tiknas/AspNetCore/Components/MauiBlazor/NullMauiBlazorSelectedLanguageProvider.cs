using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.MauiBlazor;

public class NullMauiBlazorSelectedLanguageProvider : IMauiBlazorSelectedLanguageProvider, ITransientDependency
{
    public Task<string?> GetSelectedLanguageAsync()
    {
        return Task.FromResult((string?)null);
    }
}