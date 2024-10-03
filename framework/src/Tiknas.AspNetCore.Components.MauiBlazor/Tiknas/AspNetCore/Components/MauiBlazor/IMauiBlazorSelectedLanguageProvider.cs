using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.MauiBlazor;

public interface IMauiBlazorSelectedLanguageProvider
{
    Task<string?> GetSelectedLanguageAsync();
}