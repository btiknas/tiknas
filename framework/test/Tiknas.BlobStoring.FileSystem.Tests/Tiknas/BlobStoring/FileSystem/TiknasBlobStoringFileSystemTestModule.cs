using System;
using System.IO;
using Tiknas.IO;
using Tiknas.Modularity;

namespace Tiknas.BlobStoring.FileSystem;

[DependsOn(
    typeof(TiknasBlobStoringFileSystemModule),
    typeof(TiknasBlobStoringTestModule)
    )]
public class TiknasBlobStoringFileSystemTestModule : TiknasModule
{
    private readonly string _testDirectoryPath;

    public TiknasBlobStoringFileSystemTestModule()
    {
        _testDirectoryPath = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid().ToString("N")
        );
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureAll((containerName, containerConfiguration) =>
            {
                containerConfiguration.UseFileSystem(fileSystem =>
                {
                    fileSystem.BasePath = _testDirectoryPath;
                });
            });
        });
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        DirectoryHelper.DeleteIfExists(_testDirectoryPath, true);
    }
}
