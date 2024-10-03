using JetBrains.Annotations;

namespace Tiknas.BlobStoring;

public interface IBlobProviderSelector
{
    [NotNull]
    IBlobProvider Get([NotNull] string containerName);
}
