﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.Imaging;

public interface IImageResizerContributor
{
    Task<ImageResizeResult<Stream>> TryResizeAsync(
        Stream stream, 
        ImageResizeArgs resizeArgs, 
        string? mimeType = null, 
        CancellationToken cancellationToken = default);
    
    Task<ImageResizeResult<byte[]>> TryResizeAsync(
        byte[] bytes, 
        ImageResizeArgs resizeArgs, 
        string? mimeType = null, 
        CancellationToken cancellationToken = default);
}