using JetBrains.Annotations;

namespace Tiknas.TextTemplating.VirtualFiles;

public interface ILocalizedTemplateContentReader
{
    public string? GetContentOrNull(string? culture);
}
