using Aliyun.OSS;

namespace Tiknas.BlobStoring.Aliyun;

public interface IOssClientFactory
{
    IOss Create(AliyunBlobProviderConfiguration args);
}
