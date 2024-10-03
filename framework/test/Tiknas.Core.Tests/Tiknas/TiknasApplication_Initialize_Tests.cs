using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using Shouldly;
using Tiknas.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.Modularity.PlugIns;
using Xunit;
using IConfiguration = Castle.Core.Configuration.IConfiguration;

namespace Tiknas;

public class TiknasApplication_Initialize_Tests
{
    [Fact]
    public async Task Should_Initialize_Single_Module_Async()
    {
        using (var application = await TiknasApplicationFactory.CreateAsync<IndependentEmptyModule>())
        {
            //Assert
            var module = application.Services.GetSingletonInstance<IndependentEmptyModule>();

            module.PreConfigureServicesAsyncIsCalled.ShouldBeTrue();
            module.PreConfigureServicesIsCalled.ShouldBeTrue();

            module.ConfigureServicesAsyncIsCalled.ShouldBeTrue();
            module.ConfigureServicesIsCalled.ShouldBeTrue();

            module.PostConfigureServicesAsyncIsCalled.ShouldBeTrue();
            module.PostConfigureServicesIsCalled.ShouldBeTrue();

            //Act
            await application.InitializeAsync();

            //Assert
            application.ServiceProvider.GetRequiredService<IndependentEmptyModule>().ShouldBeSameAs(module);
            module.OnApplicationInitializeAsyncIsCalled.ShouldBeTrue();
            module.OnApplicationInitializeIsCalled.ShouldBeTrue();
            //Act
            await application.ShutdownAsync();

            //Assert
            module.OnApplicationShutdownAsyncIsCalled.ShouldBeTrue();
            module.OnApplicationShutdownIsCalled.ShouldBeTrue();
        }
    }

    [Fact]
    public void Should_Initialize_Single_Module()
    {
        using (var application = TiknasApplicationFactory.Create<IndependentEmptyModule>())
        {
            //Assert
            var module = application.Services.GetSingletonInstance<IndependentEmptyModule>();
            module.PreConfigureServicesIsCalled.ShouldBeTrue();
            module.ConfigureServicesIsCalled.ShouldBeTrue();
            module.PostConfigureServicesIsCalled.ShouldBeTrue();

            //Act
            application.Initialize();

            //Assert
            application.ServiceProvider.GetRequiredService<IndependentEmptyModule>().ShouldBeSameAs(module);
            module.OnApplicationInitializeIsCalled.ShouldBeTrue();

            //Act
            application.Shutdown();

            //Assert
            module.OnApplicationShutdownIsCalled.ShouldBeTrue();
        }
    }

    [Fact]
    public async Task Should_Initialize_PlugIn_Async()
    {
        using (var application = await TiknasApplicationFactory.CreateAsync<IndependentEmptyModule>(options =>
               {
                   options.PlugInSources.AddTypes(typeof(IndependentEmptyPlugInModule));
               }))
        {
            //Assert
            var plugInModule = application.Services.GetSingletonInstance<IndependentEmptyPlugInModule>();

            plugInModule.PreConfigureServicesAsyncIsCalled.ShouldBeTrue();
            plugInModule.PreConfigureServicesIsCalled.ShouldBeTrue();

            plugInModule.ConfigureServicesAsyncIsCalled.ShouldBeTrue();
            plugInModule.ConfigureServicesIsCalled.ShouldBeTrue();

            plugInModule.PostConfigureServicesAsyncIsCalled.ShouldBeTrue();
            plugInModule.PostConfigureServicesIsCalled.ShouldBeTrue();

            //Act
            await application.InitializeAsync();

            //Assert
            application.ServiceProvider.GetRequiredService<IndependentEmptyPlugInModule>().ShouldBeSameAs(plugInModule);

            plugInModule.OnPreApplicationInitializationAsyncIsCalled.ShouldBeTrue();
            plugInModule.OnPreApplicationInitializationIsCalled.ShouldBeTrue();

            plugInModule.OnApplicationInitializeAsyncIsCalled.ShouldBeTrue();
            plugInModule.OnApplicationInitializeIsCalled.ShouldBeTrue();

            plugInModule.OnPostApplicationInitializationAsyncIsCalled.ShouldBeTrue();
            plugInModule.OnPostApplicationInitializationIsCalled.ShouldBeTrue();

            //Act
            await application.ShutdownAsync();

            //Assert
            plugInModule.OnApplicationShutdownAsyncIsCalled.ShouldBeTrue();
            plugInModule.OnApplicationShutdownIsCalled.ShouldBeTrue();
        }
    }

    [Fact]
    public void Should_Initialize_PlugIn()
    {
        using (var application = TiknasApplicationFactory.Create<IndependentEmptyModule>(options =>
        {
            options.PlugInSources.AddTypes(typeof(IndependentEmptyPlugInModule));
        }))
        {
            //Assert
            var plugInModule = application.Services.GetSingletonInstance<IndependentEmptyPlugInModule>();
            plugInModule.PreConfigureServicesIsCalled.ShouldBeTrue();
            plugInModule.ConfigureServicesIsCalled.ShouldBeTrue();
            plugInModule.PostConfigureServicesIsCalled.ShouldBeTrue();

            //Act
            application.Initialize();

            //Assert
            application.ServiceProvider.GetRequiredService<IndependentEmptyPlugInModule>().ShouldBeSameAs(plugInModule);
            plugInModule.OnPreApplicationInitializationIsCalled.ShouldBeTrue();
            plugInModule.OnApplicationInitializeIsCalled.ShouldBeTrue();
            plugInModule.OnPostApplicationInitializationIsCalled.ShouldBeTrue();

            //Act
            application.Shutdown();

            //Assert
            plugInModule.OnApplicationShutdownIsCalled.ShouldBeTrue();
        }
    }

    [Fact]
    public void Should_Set_And_Get_ApplicationName_And_InstanceId()
    {
        var applicationName = "MyApplication";

        using (var application = TiknasApplicationFactory.Create<IndependentEmptyModule>(options =>
               {
                   options.ApplicationName = applicationName;
               }))
        {
            application.ApplicationName.ShouldBe(applicationName);
            application.Services.GetApplicationName().ShouldBe(applicationName);

            application.Initialize();

            var appInfo = application.ServiceProvider.GetRequiredService<IApplicationInfoAccessor>();
            appInfo.ApplicationName.ShouldBe(applicationName);
            appInfo.InstanceId.ShouldNotBeNullOrEmpty();
        }

        using (var application = TiknasApplicationFactory.Create<IndependentEmptyModule>(options =>
               {
                   options.Services.ReplaceConfiguration(new ConfigurationBuilder()
                       .AddInMemoryCollection(new Dictionary<string, string> {{"ApplicationName", applicationName}})
                       .Build());
               }))
        {

            application.ApplicationName.ShouldBe(applicationName);
            application.Services.GetApplicationName().ShouldBe(applicationName);

            application.Initialize();

            application.ServiceProvider
                .GetRequiredService<IApplicationInfoAccessor>()
                .ApplicationName
                .ShouldBe(applicationName);
        }

        applicationName = Assembly.GetEntryAssembly()?.GetName().Name;
        using (var application = TiknasApplicationFactory.Create<IndependentEmptyModule>())
        {
            application.ApplicationName.ShouldBe(applicationName);
            application.Services.GetApplicationName().ShouldBe(applicationName);

            application.Initialize();

            application.ServiceProvider
                .GetRequiredService<IApplicationInfoAccessor>()
                .ApplicationName
                .ShouldBe(applicationName);
        }
    }

    [Fact]
    public void Should_Set_And_Get_Environment()
    {
        // Default environment is Production
        using (var application = TiknasApplicationFactory.Create<IndependentEmptyModule>())
        {
            var tiknasHostEnvironment = application.Services.GetSingletonInstance<ITiknasHostEnvironment>();
            tiknasHostEnvironment.EnvironmentName.ShouldBe(Environments.Production);

            application.Initialize();

            tiknasHostEnvironment = application.ServiceProvider.GetRequiredService<ITiknasHostEnvironment>();
            tiknasHostEnvironment.EnvironmentName.ShouldBe(Environments.Production);
        }

        // Set environment
        using (var application = TiknasApplicationFactory.Create<IndependentEmptyModule>(options =>
               {
                   options.Environment = Environments.Staging;
               }))
        {
            var tiknasHostEnvironment = application.Services.GetSingletonInstance<ITiknasHostEnvironment>();
            tiknasHostEnvironment.EnvironmentName.ShouldBe(Environments.Staging);

            application.Initialize();

            tiknasHostEnvironment = application.ServiceProvider.GetRequiredService<ITiknasHostEnvironment>();
            tiknasHostEnvironment.EnvironmentName.ShouldBe(Environments.Staging);
        }
    }

    [Fact]
    public async Task Should_Resolve_Root_Service_Provider()
    {
        using (var application = await TiknasApplicationFactory.CreateAsync<IndependentEmptyModule>())
        {
            await application.InitializeAsync();

            application
                .ServiceProvider
                .GetRequiredService<IRootServiceProvider>()
                .ShouldNotBeNull();
        }
    }
}
