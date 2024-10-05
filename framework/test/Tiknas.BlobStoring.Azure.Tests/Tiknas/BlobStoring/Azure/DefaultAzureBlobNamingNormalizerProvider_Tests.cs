using Shouldly;
using Xunit;

namespace Tiknas.BlobStoring.Azure;

public class DefaultAzureBlobNamingNormalizerProvider_Tests : TiknasBlobStoringAzureTestCommonBase
{
    private readonly IBlobNamingNormalizer _blobNamingNormalizer;

    public DefaultAzureBlobNamingNormalizerProvider_Tests()
    {
        _blobNamingNormalizer = GetRequiredService<IBlobNamingNormalizer>();
    }

    [Fact]
    public void NormalizeContainerName_Lowercase()
    {
        var filename = "ThisIsMyContainerName";
        filename = _blobNamingNormalizer.NormalizeContainerName(filename);
        filename.ShouldBe("thisismycontainername");
    }

    [Fact]
    public void NormalizeContainerName_Only_Letters_Numbers_Dash()
    {
        var filename = ",./this-i,./s-my-c,./ont,./ai+*/.=!@#$n^&*er-name.+/";
        filename = _blobNamingNormalizer.NormalizeContainerName(filename);
        filename.ShouldBe("this-is-my-container-name");
    }

    [Fact]
    public void NormalizeContainerName_Dash()
    {
        var filename = "-this--is----my-container----name-";
        filename = _blobNamingNormalizer.NormalizeContainerName(filename);
        filename.ShouldBe("this-is-my-container-name");
    }

    [Fact]
    public void NormalizeContainerName_Min_Length()
    {
        var filename = "a";
        filename = _blobNamingNormalizer.NormalizeContainerName(filename);
        filename.Length.ShouldBeGreaterThanOrEqualTo(3);
    }


    [Fact]
    public void NormalizeContainerName_Max_Length()
    {
        var filename = "tiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknastiknas";
        filename = _blobNamingNormalizer.NormalizeContainerName(filename);
        filename.Length.ShouldBeLessThanOrEqualTo(63);
    }

    [Fact]
    public void NormalizeContainerName_Max_Length_Dash()
    {
        var filename = "-this-is-my-container-name-tiknastiknastiknastiknas-a-b-p-a--b-p-";
        filename = _blobNamingNormalizer.NormalizeContainerName(filename);
        filename.ShouldBe("this-is-my-container-name-tiknastiknastiknastiknas-a-b-p-a-b");
    }
}
