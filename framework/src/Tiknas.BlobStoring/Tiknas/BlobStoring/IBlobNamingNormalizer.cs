﻿namespace Tiknas.BlobStoring;

public interface IBlobNamingNormalizer
{
    string NormalizeContainerName(string containerName);

    string NormalizeBlobName(string blobName);
}
