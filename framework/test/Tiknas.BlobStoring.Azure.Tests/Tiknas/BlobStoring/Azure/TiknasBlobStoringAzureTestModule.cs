using System;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;

namespace Tiknas.BlobStoring.Azure;

/// <summary>
/// This module will not try to connect to azure.
/// </summary>
[DependsOn(
    typeof(TiknasBlobStoringAzureModule),
    typeof(TiknasBlobStoringTestModule)
)]
public class TiknasBlobStoringAzureTestCommonModule : TiknasModule
{

}

[DependsOn(
    typeof(TiknasBlobStoringAzureTestCommonModule)
)]
public class TiknasBlobStoringAzureTestModule : TiknasModule
{
    private const string UserSecretsId = "9f0d2c00-80c1-435b-bfab-2c39c8249091";

    private string _connectionString;

    private readonly string _randomContainerName = "tiknas-azure-test-container-" + Guid.NewGuid().ToString("N");

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.ReplaceConfiguration(ConfigurationHelper.BuildConfiguration(builderAction: builder =>
        {
            builder.AddUserSecrets(UserSecretsId);
        }));

        var configuration = context.Services.GetConfiguration();
        _connectionString = configuration["Azure:ConnectionString"];

        Configure<TiknasBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureAll((containerName, containerConfiguration) =>
            {
                containerConfiguration.UseAzure(azure =>
                {
                    azure.ConnectionString = _connectionString;
                    azure.ContainerName = _randomContainerName;
                    azure.CreateContainerIfNotExists = true;
                });
            });
        });
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);
        blobServiceClient.GetBlobContainerClient(_randomContainerName).DeleteIfExists();
    }
}
