﻿namespace Tiknas.BlobStoring.Azure;

public interface IAzureBlobNameCalculator
{
    string Calculate(BlobProviderArgs args);
}
