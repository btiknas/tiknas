namespace Tiknas.BlobStoring.FileSystem;

public interface IBlobFilePathCalculator
{
    string Calculate(BlobProviderArgs args);
}
