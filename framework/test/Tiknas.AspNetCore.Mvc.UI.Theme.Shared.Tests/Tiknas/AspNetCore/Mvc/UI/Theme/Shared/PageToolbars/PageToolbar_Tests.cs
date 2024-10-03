using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shouldly;
using Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Pages.Shared.Components.TiknasPageToolbar.Button;
using Tiknas.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;
using Tiknas.Localization;
using Xunit;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Tests.Tiknas.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

public class PageToolbar_Tests : TiknasAspNetCoreMvcUiThemeSharedTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<TiknasPageToolbarOptions>(options =>
        {
            options.Configure("TestPage1", toolbar =>
            {
                toolbar.Contributors.Add(new MyToolbarContributor());
                toolbar.Contributors.Add(new MyToolbarContributor2());
                toolbar.AddComponent<MyPageComponent5>();
                toolbar.AddButton(new FixedLocalizableString("My button"), order: -1);
            });
        });
    }

    [Fact]
    public void TiknasPageToolbarOptions_Should_Contain_Contributors()
    {
        var options = GetRequiredService<IOptions<TiknasPageToolbarOptions>>().Value;
        options.Toolbars.Count.ShouldBe(1);
        options.Toolbars.ShouldContainKey("TestPage1");
        options.Toolbars["TestPage1"].Contributors.Count.ShouldBe(4);
    }

    [Fact]
    public async Task PageToolbarManager_Should_Return_ToolbarItems()
    {
        var pageToolbarManager = GetRequiredService<IPageToolbarManager>();
        var items = await pageToolbarManager.GetItemsAsync("TestPage1");
        items.Length.ShouldBe(5);
        items[0].ComponentType.ShouldBe(typeof(TiknasPageToolbarButtonViewComponent));
        items[1].ComponentType.ShouldBe(typeof(MyPageComponent2));
        items[2].ComponentType.ShouldBe(typeof(MyPageComponent3));
        items[3].ComponentType.ShouldBe(typeof(MyPageComponent4));
        items[4].ComponentType.ShouldBe(typeof(MyPageComponent5));
    }

    public class MyToolbarContributor : IPageToolbarContributor
    {
        public Task ContributeAsync(PageToolbarContributionContext context)
        {
            context.Items.Add(new PageToolbarItem(typeof(MyPageComponent1)));
            context.Items.Add(new PageToolbarItem(typeof(MyPageComponent2)));
            return Task.CompletedTask;
        }
    }

    public class MyToolbarContributor2 : IPageToolbarContributor
    {
        public Task ContributeAsync(PageToolbarContributionContext context)
        {
            context.Items.RemoveAll(i => i.ComponentType == typeof(MyPageComponent1));
            context.Items.Add(new PageToolbarItem(typeof(MyPageComponent3)));
            context.Items.Add(new PageToolbarItem(typeof(MyPageComponent4)));
            return Task.CompletedTask;
        }
    }

    public class MyPageComponent1 : TiknasViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            return Content("MyPageComponent1");
        }
    }

    public class MyPageComponent2 : TiknasViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            return Content("MyPageComponent2");
        }
    }

    public class MyPageComponent3 : TiknasViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            return Content("MyPageComponent3");
        }
    }

    public class MyPageComponent4 : TiknasViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            return Content("MyPageComponent4");
        }
    }

    public class MyPageComponent5 : TiknasViewComponent
    {
        public IViewComponentResult InvokeAsync()
        {
            return Content("MyPageComponent4");
        }
    }
}
