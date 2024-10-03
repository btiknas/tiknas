using Shouldly;
using Tiknas.BlobStoring.Fakes;
using Tiknas.BlobStoring.TestObjects;
using Tiknas.DynamicProxy;
using Xunit;

namespace Tiknas.BlobStoring;

public class BlobProviderSelector_Tests : TiknasBlobStoringTestBase
{
    private readonly IBlobProviderSelector _selector;

    public BlobProviderSelector_Tests()
    {
        _selector = GetRequiredService<IBlobProviderSelector>();
    }

    [Fact]
    public void Should_Select_Default_Provider_If_Not_Configured()
    {
        _selector.Get<TestContainer3>().ShouldBeAssignableTo<FakeBlobProvider1>();
    }

    [Fact]
    public void Should_Select_Configured_Provider()
    {
        _selector.Get<TestContainer1>().ShouldBeAssignableTo<FakeBlobProvider1>();
        _selector.Get<TestContainer2>().ShouldBeAssignableTo<FakeBlobProvider2>();
    }
}
