using Tiknas.TestBase;

namespace Tiknas.BlobStoring.FileSystem;

public abstract class TiknasBlobStoringFileSystemTestBase : TiknasIntegratedTest<TiknasBlobStoringFileSystemTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
