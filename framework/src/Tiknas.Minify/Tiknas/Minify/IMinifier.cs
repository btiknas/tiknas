using JetBrains.Annotations;

namespace Tiknas.Minify;

public interface IMinifier
{
    string Minify(
        string source,
        string? fileName = null,
        string? originalFileName = null);
}
