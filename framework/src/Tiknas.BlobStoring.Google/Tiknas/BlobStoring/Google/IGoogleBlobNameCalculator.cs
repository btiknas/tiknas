namespace Tiknas.BlobStoring.Google;

public interface IGoogleBlobNameCalculator
{
    string Calculate(BlobProviderArgs args);
}