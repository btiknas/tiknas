using Shouldly;
using Tiknas.BlobStoring.TestObjects;
using Xunit;

namespace Tiknas.BlobStoring;

public class BlobContainer_Injection_Tests : TiknasBlobStoringTestBase
{
    [Fact]
    public void Should_Inject_DefaultContainer_For_Non_Generic_Interface()
    {
        GetRequiredService<IBlobContainer>()
            .ShouldBeOfType<BlobContainer<DefaultContainer>>();
    }

    [Fact]
    public void Should_Inject_Specified_Container_For_Generic_Interface()
    {
        GetRequiredService<IBlobContainer<DefaultContainer>>()
            .ShouldBeOfType<BlobContainer<DefaultContainer>>();

        GetRequiredService<IBlobContainer<TestContainer1>>()
            .ShouldBeOfType<BlobContainer<TestContainer1>>();

        GetRequiredService<IBlobContainer<TestContainer2>>()
            .ShouldBeOfType<BlobContainer<TestContainer2>>();

        GetRequiredService<IBlobContainer<TestContainer3>>()
            .ShouldBeOfType<BlobContainer<TestContainer3>>();
    }
}
