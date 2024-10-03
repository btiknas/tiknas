using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.DataModel.Args;
using Tiknas.Modularity;
using Tiknas.Threading;

namespace Tiknas.BlobStoring.Minio;

[DependsOn(
    typeof(TiknasBlobStoringMinioModule),
    typeof(TiknasBlobStoringTestModule)
)]
public class TiknasBlobStoringMinioTestCommonModule : TiknasModule
{

}

[DependsOn(
    typeof(TiknasBlobStoringMinioTestCommonModule)
)]
public class TiknasBlobStoringMinioTestModule : TiknasModule
{
    private const string UserSecretsId = "9f0d2c00-80c1-435b-bfab-2c39c8249091";

    private string _endPoint;
    private string _accessKey;
    private string _secretKey;

    private readonly string _randomContainerName = "tiknas-minio-test-container-" + Guid.NewGuid().ToString("N");

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.ReplaceConfiguration(ConfigurationHelper.BuildConfiguration(builderAction: builder =>
        {
            builder.AddUserSecrets(UserSecretsId);
        }));

        var configuration = context.Services.GetConfiguration();
        _endPoint = configuration["Minio:EndPoint"];
        _accessKey = configuration["Minio:AccessKey"];
        _secretKey = configuration["Minio:SecretKey"];

        Configure<TiknasBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureAll((containerName, containerConfiguration) =>
            {
                containerConfiguration.UseMinio(minio =>
                {
                    minio.EndPoint = _endPoint;
                    minio.AccessKey = _accessKey;
                    minio.SecretKey = _secretKey;
                    minio.WithSSL = false;
                    minio.BucketName = _randomContainerName;
                    minio.CreateBucketIfNotExists = true;
                });
            });
        });
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationShutdownAsync(context));
    }

    public async override Task OnApplicationShutdownAsync(ApplicationShutdownContext context)
    {
        var minioClient = new MinioClient().WithEndpoint(_endPoint).WithCredentials(_accessKey, _secretKey).Build();
        if (await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_randomContainerName)))
        {
            var objects = await minioClient.ListObjectsAsync(new ListObjectsArgs().WithBucket(_randomContainerName)
                .WithPrefix(null).WithRecursive(true)).ToList();

            foreach (var item in objects)
            {
                await minioClient.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(_randomContainerName)
                    .WithObject(item.Key));
            }

            await minioClient.RemoveBucketAsync(new RemoveBucketArgs().WithBucket(_randomContainerName));
        }
    }
}
