namespace Tiknas.VirtualFileSystem;

public class TiknasVirtualFileSystemOptions
{
    public VirtualFileSetList FileSets { get; }

    public TiknasVirtualFileSystemOptions()
    {
        FileSets = new VirtualFileSetList();
    }
}
