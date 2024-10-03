namespace Tiknas.BlobStoring;

public class TiknasBlobStoringOptions
{
    public BlobContainerConfigurations Containers { get; }

    public TiknasBlobStoringOptions()
    {
        Containers = new BlobContainerConfigurations();
    }
}
