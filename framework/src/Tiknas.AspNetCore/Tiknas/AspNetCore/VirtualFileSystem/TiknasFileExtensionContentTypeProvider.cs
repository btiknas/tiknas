using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.VirtualFileSystem;

public class TiknasFileExtensionContentTypeProvider : IContentTypeProvider, ITransientDependency
{
    protected TiknasAspNetCoreContentOptions Options { get; }

    public TiknasFileExtensionContentTypeProvider(IOptions<TiknasAspNetCoreContentOptions> tiknasAspNetCoreContentOptions)
    {
        Options = tiknasAspNetCoreContentOptions.Value;
    }

    public bool TryGetContentType(string subpath, out string contentType)
    {
        var extension = GetExtension(subpath);
        if (extension == null)
        {
            contentType = null!;
            return false;
        }

        return Options.ContentTypeMaps.TryGetValue(extension, out contentType!);
    }

    protected virtual string? GetExtension(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        var index = path.LastIndexOf('.');
        return index < 0 ? null : path.Substring(index);
    }
}
