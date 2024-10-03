using System;
using Tiknas.Modularity;

namespace Tiknas.BlobStoring.FileSystem;

[DependsOn(
    typeof(TiknasBlobStoringModule)
    )]
public class TiknasBlobStoringFileSystemModule : TiknasModule
{

}
