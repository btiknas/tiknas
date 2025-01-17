using System;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Tiknas.Caching;
using Tiknas.DependencyInjection;
using Tiknas.VirtualFileSystem;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling;

[Dependency(ReplaceServices = true)]
public class CachedBundleDynamicFileProvider : DynamicFileProvider
{
    protected IDistributedCache<InMemoryFileInfoCacheItem> Cache { get; }
    protected IOptions<TiknasBundlingOptions> BundlingOptions { get; }

    public CachedBundleDynamicFileProvider(
        IDistributedCache<InMemoryFileInfoCacheItem> cache,
        IOptions<TiknasBundlingOptions> bundlingOptions)
    {
        Cache = cache;
        BundlingOptions = bundlingOptions;
    }

    public override IFileInfo GetFileInfo(string? subpath)
    {
        var fileInfo = base.GetFileInfo(subpath);

        if (!subpath.IsNullOrWhiteSpace() && fileInfo is NotFoundFileInfo &&
            subpath.Contains(BundlingOptions.Value.BundleFolderName, StringComparison.OrdinalIgnoreCase))
        {
            var filePath = NormalizePath(subpath);
            var cacheItem = Cache.Get(filePath);
            if (cacheItem == null)
            {
                return fileInfo;
            }

            fileInfo = new InMemoryFileInfo(filePath, cacheItem.FileContent, cacheItem.Name);
            DynamicFiles.AddOrUpdate(filePath, fileInfo, (key, value) => fileInfo);
        }

        return fileInfo;
    }

    public override void AddOrUpdate(IFileInfo fileInfo)
    {
        var filePath = fileInfo.GetVirtualOrPhysicalPathOrNull();
        Cache.GetOrAdd(filePath!, () => new InMemoryFileInfoCacheItem(filePath!, fileInfo.ReadBytes(), fileInfo.Name));
        base.AddOrUpdate(fileInfo);
    }

    public override bool Delete(string filePath)
    {
        Cache.Remove(filePath);
        return base.Delete(filePath);
    }
}
