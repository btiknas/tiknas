﻿using Microsoft.Extensions.FileProviders;

namespace Tiknas.VirtualFileSystem;

public interface IDynamicFileProvider : IFileProvider
{
    void AddOrUpdate(IFileInfo fileInfo);

    bool Delete(string filePath);
}
