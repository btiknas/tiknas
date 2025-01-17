﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Tiknas.TextTemplating.Scriban;

public class ScribanTemplateRendererProvider_Tests : TiknasTextTemplatingTestBase<ScribanTextTemplatingTestModule>
{
    private readonly ITemplateRenderer _templateRenderer;

    public ScribanTemplateRendererProvider_Tests()
    {
        _templateRenderer = GetRequiredService<ITemplateRenderer>();
    }

    [Fact]
    public async Task Should_Get_Rendered_Localized_Template_Content_With_Different_Cultures()
    {
        (await _templateRenderer.RenderAsync(
            TestTemplates.WelcomeEmail,
            model: new {
                name = "John"
            },
            cultureName: "en"
        )).ShouldBe("Welcome John to the tiknas.de!");

        (await _templateRenderer.RenderAsync(
            TestTemplates.WelcomeEmail,
            model: new {
                name = "John"
            },
            cultureName: "tr"
        )).ShouldBe("Merhaba John, tiknas.de'ya hoşgeldiniz!");

        //"en-US" fallbacks to "en" since "en-US" doesn't exists and "en" is the fallback culture
        (await _templateRenderer.RenderAsync(
            TestTemplates.WelcomeEmail,
            model: new {
                name = "John"
            },
            cultureName: "en-US"
        )).ShouldBe("Welcome John to the tiknas.de!");

        //"fr" fallbacks to "en" since "fr" doesn't exists and "en" is the default culture
        (await _templateRenderer.RenderAsync(
            TestTemplates.WelcomeEmail,
            model: new {
                Name = "John" //Intentionally written as PascalCase since Scriban supports it
                },
            cultureName: "fr"
        )).ShouldBe("Welcome John to the tiknas.de!");
    }

    [Fact]
    public async Task Should_Get_Rendered_Localized_Template_Content_With_Stronly_Typed_Model()
    {
        (await _templateRenderer.RenderAsync(
            TestTemplates.WelcomeEmail,
            model: new WelcomeEmailModel("John"),
            cultureName: "en"
        )).ShouldBe("Welcome John to the tiknas.de!");
    }

    [Fact]
    public async Task Should_Get_Rendered_Localized_Template_Content_With_Dictionary_Model()
    {
        (await _templateRenderer.RenderAsync(
            TestTemplates.WelcomeEmail,
            model: new Dictionary<string, object>() { { "name", "John" } },
            cultureName: "en"
        )).ShouldBe("Welcome John to the tiknas.de!");
    }

    [Fact]
    public async Task Should_Get_Rendered_Inline_Localized_Template()
    {
        (await _templateRenderer.RenderAsync(
            TestTemplates.ForgotPasswordEmail,
            new ForgotPasswordEmailModel("John"),
            cultureName: "en"
        )).ShouldBe("*BEGIN*Hello John, how are you?. Please click to the following link to get an email to reset your password!*END*");

        (await _templateRenderer.RenderAsync(
            TestTemplates.ForgotPasswordEmail,
            model: new Dictionary<string, object>() { { "name", "John" } },
            cultureName: "tr"
        )).ShouldBe("*BEGIN*Merhaba John, nasılsın?. Please click to the following link to get an email to reset your password!*END*");
    }

    [Fact]
    public async Task Should_Get_Localized_Numbers()
    {
        (await _templateRenderer.RenderAsync(
            TestTemplates.ShowDecimalNumber,
            new Dictionary<string, decimal>(new List<KeyValuePair<string, decimal>> { new("amount", 123.45M) }),
            cultureName: "en"
        )).ShouldBe("*BEGIN*123.45*END*");

        (await _templateRenderer.RenderAsync(
            TestTemplates.ShowDecimalNumber,
            new Dictionary<string, decimal>(new List<KeyValuePair<string, decimal>> { new("amount", 123.45M) }),
            cultureName: "de"
        )).ShouldBe("*BEGIN*123,45*END*");
    }

    private class WelcomeEmailModel
    {
        public string Name { get; set; }

        public WelcomeEmailModel()
        {

        }

        public WelcomeEmailModel(string name)
        {
            Name = name;
        }
    }

    private class ForgotPasswordEmailModel
    {
        public string Name { get; set; }

        public ForgotPasswordEmailModel()
        {

        }

        public ForgotPasswordEmailModel(string name)
        {
            Name = name;
        }
    }
}
