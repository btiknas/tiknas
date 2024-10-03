namespace Tiknas.BlobStoring.Aliyun;

public interface IAliyunBlobNameCalculator
{
    string Calculate(BlobProviderArgs args);
}
