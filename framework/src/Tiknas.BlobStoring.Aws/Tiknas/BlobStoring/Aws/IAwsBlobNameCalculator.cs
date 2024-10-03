namespace Tiknas.BlobStoring.Aws;

public interface IAwsBlobNameCalculator
{
    string Calculate(BlobProviderArgs args);
}
