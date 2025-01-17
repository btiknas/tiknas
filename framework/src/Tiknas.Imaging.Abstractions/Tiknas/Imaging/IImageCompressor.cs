﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.Imaging;

public interface IImageCompressor
{
    Task<ImageCompressResult<Stream>> CompressAsync(
        Stream stream,
        string? mimeType = null,
        CancellationToken cancellationToken = default
    );

    Task<ImageCompressResult<byte[]>> CompressAsync(
        byte[] bytes,
        string? mimeType = null,
        CancellationToken cancellationToken = default
    );
}