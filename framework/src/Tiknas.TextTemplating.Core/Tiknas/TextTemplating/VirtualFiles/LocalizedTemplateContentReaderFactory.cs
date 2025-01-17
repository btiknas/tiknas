﻿using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;
using Tiknas.Threading;
using Tiknas.VirtualFileSystem;

namespace Tiknas.TextTemplating.VirtualFiles;

public class LocalizedTemplateContentReaderFactory : ILocalizedTemplateContentReaderFactory, ISingletonDependency
{
    protected IVirtualFileProvider VirtualFileProvider { get; }
    protected ConcurrentDictionary<string, ILocalizedTemplateContentReader> ReaderCache { get; }
    protected SemaphoreSlim SyncObj;
    protected ITiknasHostEnvironment TiknasHostEnvironment { get; }

    public LocalizedTemplateContentReaderFactory(IVirtualFileProvider virtualFileProvider, ITiknasHostEnvironment tiknasHostEnvironment)
    {
        VirtualFileProvider = virtualFileProvider;
        TiknasHostEnvironment = tiknasHostEnvironment;
        ReaderCache = new ConcurrentDictionary<string, ILocalizedTemplateContentReader>();
        SyncObj = new SemaphoreSlim(1, 1);
    }

    public virtual async Task<ILocalizedTemplateContentReader> CreateAsync(TemplateDefinition templateDefinition)
    {
        if (TiknasHostEnvironment.IsDevelopment())
        {
            return await CreateInternalAsync(templateDefinition);
        }

        if (ReaderCache.TryGetValue(templateDefinition.Name, out var reader))
        {
            return reader;
        }

        using (await SyncObj.LockAsync())
        {
            if (ReaderCache.TryGetValue(templateDefinition.Name, out reader))
            {
                return reader;
            }

            reader = await CreateInternalAsync(templateDefinition);
            ReaderCache[templateDefinition.Name] = reader;
            return reader;
        }
    }

    protected virtual async Task<ILocalizedTemplateContentReader> CreateInternalAsync(
        TemplateDefinition templateDefinition)
    {
        var virtualPath = templateDefinition.GetVirtualFilePathOrNull();
        if (virtualPath == null)
        {
            return NullLocalizedTemplateContentReader.Instance;
        }

        var fileInfo = VirtualFileProvider.GetFileInfo(virtualPath);
        if (!fileInfo.Exists)
        {
            var directoryContents = VirtualFileProvider.GetDirectoryContents(virtualPath);
            if (!directoryContents.Exists)
            {
                throw new TiknasException("Could not find a file/folder at the location: " + virtualPath);
            }

            fileInfo = new VirtualDirectoryFileInfo(virtualPath, virtualPath, DateTimeOffset.UtcNow);
        }

        if (fileInfo.IsDirectory)
        {
            //TODO: Configure file extensions.
            var folderReader = new VirtualFolderLocalizedTemplateContentReader(new[] { ".tpl", ".cshtml" });
            await folderReader.ReadContentsAsync(VirtualFileProvider, virtualPath);
            return folderReader;
        }
        else //File
        {
            var singleFileReader = new FileInfoLocalizedTemplateContentReader();
            await singleFileReader.ReadContentsAsync(fileInfo);
            return singleFileReader;
        }
    }
}
