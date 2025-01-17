﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Tiknas.Authorization.Permissions;
using Tiknas.Security.Claims;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.UI.Navigation;

public class MenuManager_Tests : TiknasIntegratedTest<TiknasUiNavigationTestModule>
{
    private readonly IMenuManager _menuManager;

    public MenuManager_Tests()
    {
        _menuManager = ServiceProvider.GetRequiredService<IMenuManager>();
    }

    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        var claims = new List<Claim>() {
                new Claim(TiknasClaimTypes.UserId, "1fcf46b2-28c3-48d0-8bac-fa53268a2775"),
            };

        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var principalAccessor = Substitute.For<ICurrentPrincipalAccessor>();
        principalAccessor.Principal.Returns(ci => claimsPrincipal);
        Thread.CurrentPrincipal = claimsPrincipal;
    }

    [Fact]
    public async Task Should_Get_Menu()
    {
        var mainMenu = await _menuManager.GetAsync(StandardMenus.Main);

        mainMenu.Name.ShouldBe(StandardMenus.Main);
        mainMenu.DisplayName.ShouldBe("Main Menu");
        mainMenu.Items.Count.ShouldBe(5);
        mainMenu.Items[0].Name.ShouldBe("Dashboard");
        mainMenu.Items[1].Name.ShouldBe(DefaultMenuNames.Application.Main.Administration);
        mainMenu.Items[1].Items[0].Name.ShouldBe("Administration.UserManagement");
        mainMenu.Items[1].Items[1].Name.ShouldBe("Administration.RoleManagement");
        mainMenu.Items[1].Items[2].Name.ShouldBe("Administration.DashboardSettings");
        mainMenu.Items[1].Items[3].Name.ShouldBe("Administration.SubMenu1"); //No need permission.
                                                                             // Administration.SubMenu1.1 and Administration.SubMenu1.2 are removed because of don't have permissions.
    }

    [Fact]
    public async Task GetMainMenuAsync_ShouldMergeMultipleMenus()
    {
        var mainMenu = await _menuManager.GetMainMenuAsync();

        mainMenu.Name.ShouldBe(StandardMenus.Main);

        mainMenu.Items.Count.ShouldBe(6);

        mainMenu.Items.ShouldContain(x => x.Name == "Products");
        mainMenu.Items.ShouldContain(x => x.Name == "Dashboard");
    }

    [Fact]
    public async Task GetMainMenuAsync_GroupMenuItems()
    {
        var mainMenu = await _menuManager.GetMainMenuAsync();

        mainMenu.Name.ShouldBe(StandardMenus.Main);
        mainMenu.Items.Count.ShouldBe(6);

        mainMenu.Items[2].GroupName.ShouldBe("Layouts");
        mainMenu.Items[3].GroupName.ShouldBe("Layouts");
        mainMenu.Items[4].GroupName.ShouldBe(null); // No group defined

        var layoutsGroup = mainMenu.GetMenuGroup("Layouts");
        layoutsGroup.Name.ShouldBe("Layouts");
        layoutsGroup.DisplayName.ShouldBe("Layouts");
    }

    /* Adds menu items:
     * - Administration
     *   - User Management
     *   - Role Management
     */
    public class TestMenuContributor1 : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            context.Menu.DisplayName = "Main Menu";

            var administration = context.Menu.GetAdministration();

            administration.AddItem(new ApplicationMenuItem("Administration.UserManagement", "User Management", url: "/admin/users").RequirePermissions("Administration.UserManagement"));
            administration.AddItem(new ApplicationMenuItem("Administration.RoleManagement", "Role Management", url: "/admin/roles").RequirePermissions("Administration.RoleManagement"));

            return Task.CompletedTask;
        }
    }

    /* Adds menu items:
     * - Dashboard
     * - Administration
     *   - Dashboard Settings
     */
    public class TestMenuContributor2 : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            context.Menu.Items.Insert(0, new ApplicationMenuItem("Dashboard", "Dashboard", url: "/dashboard").RequirePermissions("Dashboard"));

            var administration = context.Menu.GetAdministration();

            administration.AddItem(new ApplicationMenuItem("Administration.DashboardSettings", "Dashboard Settings", url: "/admin/settings/dashboard").RequirePermissions("Administration.DashboardSettings"));

            administration.AddItem(
                new ApplicationMenuItem("Administration.SubMenu1", "Sub menu 1", url: "/submenu1")
                    .AddItem(new ApplicationMenuItem("Administration.SubMenu1.1", "Sub menu 1.1", url: "/submenu1/submenu1_1").RequirePermissions("Administration.SubMenu1.1"))
                    .AddItem(new ApplicationMenuItem("Administration.SubMenu1.2", "Sub menu 1.2", url: "/submenu1/submenu1_2").RequirePermissions("Administration.SubMenu1.2"))
            );

            return Task.CompletedTask;
        }
    }

    /* Adds menu items:
     * - Products
     *   - AspNetZero
     *   - TIKNAS
     */
    public class TestMenuContributor3 : IMenuContributor
    {
        public const string MenuName = "MenuThree";
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name != MenuName)
            {
                return Task.CompletedTask;
            }

            var products = new ApplicationMenuItem("Products", "Products", "/products");
            context.Menu.Items.Add(products);

            products.AddItem(new ApplicationMenuItem("AspNetZero", "AspNetZero", url: "/products/aspnetzero"));

            products.AddItem(new ApplicationMenuItem("TIKNAS", "TIKNAS", url: "/products/tiknas"));

            return Task.CompletedTask;
        }
    }

    /* Adds group and menu items:
     * - Layouts
     *   - Toolbars
     *   - Page Header
     */
    public class TestMenuContributor4 : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            context.Menu.AddGroup(new ApplicationMenuGroup("Layouts", "Layouts"));

            context.Menu.AddItem(new ApplicationMenuItem("Toolbars", "Toolbars", url: "/layouts/toolbars", groupName: "Layouts"));
            context.Menu.AddItem(new ApplicationMenuItem("PageHeader", "Page Header", url: "/layouts/page-header", groupName: "Layouts"));

            context.Menu.AddItem(new ApplicationMenuItem("Branding", "Branding", url: "/layouts/branding", groupName: "NotDefinedGroup"));

            return Task.CompletedTask;
        }
    }
}
