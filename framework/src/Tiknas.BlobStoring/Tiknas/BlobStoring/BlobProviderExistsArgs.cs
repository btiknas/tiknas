﻿using System.Threading;
using JetBrains.Annotations;

namespace Tiknas.BlobStoring;

public class BlobProviderExistsArgs : BlobProviderArgs
{
    public BlobProviderExistsArgs(
        [NotNull] string containerName,
        [NotNull] BlobContainerConfiguration configuration,
        [NotNull] string blobName,
        CancellationToken cancellationToken = default)
    : base(
        containerName,
        configuration,
        blobName,
        cancellationToken)
    {
    }
}
