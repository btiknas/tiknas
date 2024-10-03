using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tiknas.Autofac;
using Tiknas.BlobStoring.Fakes;
using Tiknas.BlobStoring.TestObjects;
using Tiknas.Modularity;

namespace Tiknas.BlobStoring;

[DependsOn(
    typeof(TiknasBlobStoringModule),
    typeof(TiknasTestBaseModule),
    typeof(TiknasAutofacModule)
    )]
public class TiknasBlobStoringTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IBlobProvider>(Substitute.For<FakeBlobProvider1>());
        context.Services.AddSingleton<IBlobProvider>(Substitute.For<FakeBlobProvider2>());

        Configure<TiknasBlobStoringOptions>(options =>
        {
            options.Containers
                .ConfigureDefault(container =>
                {
                    container.SetConfiguration("TestConfigDefault", "TestValueDefault");
                    container.ProviderType = typeof(FakeBlobProvider1);
                })
                .Configure<TestContainer1>(container =>
                {
                    container.SetConfiguration("TestConfig1", "TestValue1");
                    container.ProviderType = typeof(FakeBlobProvider1);
                })
                .Configure<TestContainer2>(container =>
                {
                    container.SetConfiguration("TestConfig2", "TestValue2");
                    container.ProviderType = typeof(FakeBlobProvider2);
                });
        });
    }
}
