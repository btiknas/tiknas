﻿using System.Threading;
using JetBrains.Annotations;

namespace Tiknas.BlobStoring;

public class BlobProviderDeleteArgs : BlobProviderArgs
{
    public BlobProviderDeleteArgs(
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
