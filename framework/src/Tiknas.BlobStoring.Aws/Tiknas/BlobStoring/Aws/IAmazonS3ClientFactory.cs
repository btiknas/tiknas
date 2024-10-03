using System.Threading.Tasks;
using Amazon.S3;

namespace Tiknas.BlobStoring.Aws;

public interface IAmazonS3ClientFactory
{
    Task<AmazonS3Client> GetAmazonS3Client(AwsBlobProviderConfiguration configuration);
}
