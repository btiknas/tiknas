﻿using System.IO;
using System.Threading.Tasks;

namespace Tiknas.BlobStoring;

public interface IBlobProvider
{
    Task SaveAsync(BlobProviderSaveArgs args);

    Task<bool> DeleteAsync(BlobProviderDeleteArgs args);

    Task<bool> ExistsAsync(BlobProviderExistsArgs args);

    Task<Stream?> GetOrNullAsync(BlobProviderGetArgs args);
}
