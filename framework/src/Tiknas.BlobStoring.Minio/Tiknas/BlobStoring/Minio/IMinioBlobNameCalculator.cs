namespace Tiknas.BlobStoring.Minio;

public interface IMinioBlobNameCalculator
{
    string Calculate(BlobProviderArgs args);
}
