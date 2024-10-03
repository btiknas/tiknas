using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shouldly;
using System;
using System.Reflection;
using Tiknas.DependencyInjection;
using Xunit;

namespace Tiknas.AspNetCore.Mvc.Conventions;

public class TiknasServiceConvention_Tests : AspNetCoreMvcTestBase
{
    private readonly IConventionalRouteBuilder _conventionalRouteBuilder;
    private readonly IOptions<TiknasAspNetCoreMvcOptions> _options;

    public TiknasServiceConvention_Tests()
    {
        _conventionalRouteBuilder = GetRequiredService<IConventionalRouteBuilder>();
        _options = GetRequiredService<IOptions<TiknasAspNetCoreMvcOptions>>();
    }

    [Fact]
    public void Should_Not_Remove_Derived_Controller_If_Not_Expose_Service()
    {
        // Arrange
        var applicationModel = new ApplicationModel();
        var baseControllerModel = new ControllerModel(typeof(BaseController).GetTypeInfo(), Array.Empty<object>())
        {
            Application = applicationModel
        };
        applicationModel.Controllers.Add(baseControllerModel);

        var derivedControllerModel = new ControllerModel(typeof(DerivedController).GetTypeInfo(), Array.Empty<object>())
        {
            Application = applicationModel
        };
        applicationModel.Controllers.Add(derivedControllerModel);

        var tiknasServiceConvention = new TiknasServiceConvention(_options, _conventionalRouteBuilder);

        // Act
        tiknasServiceConvention.Apply(applicationModel);

        // Assert
        applicationModel.Controllers.ShouldContain(baseControllerModel);
        applicationModel.Controllers.ShouldContain(derivedControllerModel);
    }

    [Fact]
    public void Should_Remove_Exposed_Controller_If_Expose_Self()
    {
        // Arrange
        var applicationModel = new ApplicationModel();
        var baseControllerModel = new ControllerModel(typeof(BaseController).GetTypeInfo(), Array.Empty<object>())
        {
            Application = applicationModel
        };
        applicationModel.Controllers.Add(baseControllerModel);

        var derivedControllerModel = new ControllerModel(typeof(ExposeServiceIncludeSelfDerivedController).GetTypeInfo(), Array.Empty<object>())
        {
            Application = applicationModel
        };
        applicationModel.Controllers.Add(derivedControllerModel);

        var tiknasServiceConvention = new TiknasServiceConvention(_options, _conventionalRouteBuilder);

        // Act
        tiknasServiceConvention.Apply(applicationModel);

        // Assert
        applicationModel.Controllers.ShouldNotContain(baseControllerModel);
        applicationModel.Controllers.ShouldContain(derivedControllerModel);
    }

    [Fact]
    public void Should_Not_Remove_Derived_Controller_If_No_Base_Controller_Model()
    {
        // Arrange
        var applicationModel = new ApplicationModel();
        var derivedControllerModel = new ControllerModel(typeof(ExposeServiceDerivedController).GetTypeInfo(), Array.Empty<object>())
        {
            Application = applicationModel
        };
        applicationModel.Controllers.Add(derivedControllerModel);

        var tiknasServiceConvention = new TiknasServiceConvention(_options, _conventionalRouteBuilder);

        // Act
        tiknasServiceConvention.Apply(applicationModel);

        // Assert
        applicationModel.Controllers.ShouldContain(derivedControllerModel);
    }

    [Fact]
    public void Should_Remove_Derived_Controller_If_Expose_Service()
    {
        // Arrange
        var applicationModel = new ApplicationModel();
        var baseControllerModel = new ControllerModel(typeof(BaseController).GetTypeInfo(), Array.Empty<object>())
        {
            Application = applicationModel
        };
        applicationModel.Controllers.Add(baseControllerModel);

        var derivedControllerModel = new ControllerModel(typeof(ExposeServiceDerivedController).GetTypeInfo(), Array.Empty<object>())
        {
            Application = applicationModel
        };
        applicationModel.Controllers.Add(derivedControllerModel);

        var tiknasServiceConvention = new TiknasServiceConvention(_options, _conventionalRouteBuilder);

        // Act
        tiknasServiceConvention.Apply(applicationModel);

        // Assert
        applicationModel.Controllers.ShouldContain(baseControllerModel);
        applicationModel.Controllers.ShouldNotContain(derivedControllerModel);
    }
}

public class BaseController : Controller
{
}

public class DerivedController : BaseController
{
}

[ExposeServices(typeof(BaseController))]
public class ExposeServiceDerivedController : BaseController
{
}

[ExposeServices(typeof(BaseController), IncludeSelf = true)]
public class ExposeServiceIncludeSelfDerivedController : BaseController
{
}
