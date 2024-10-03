using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Mvc.UI.Theming;
using Tiknas.Authorization.Permissions;
using Tiknas.DependencyInjection;
using Tiknas.SimpleStateChecking;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

public class ToolbarManager : IToolbarManager, ITransientDependency
{
    protected IThemeManager ThemeManager { get; }
    protected TiknasToolbarOptions Options { get; }
    protected IServiceProvider ServiceProvider { get; }
    protected ISimpleStateCheckerManager<ToolbarItem> SimpleStateCheckerManager { get; }

    public ToolbarManager(
        IOptions<TiknasToolbarOptions> options,
        IServiceProvider serviceProvider,
        IThemeManager themeManager,
        ISimpleStateCheckerManager<ToolbarItem> simpleStateCheckerManager)
    {
        ThemeManager = themeManager;
        SimpleStateCheckerManager = simpleStateCheckerManager;
        ServiceProvider = serviceProvider;
        Options = options.Value;
    }

    public async Task<Toolbar> GetAsync(string name)
    {
        var toolbar = new Toolbar(name);

        using (var scope = ServiceProvider.CreateScope())
        {
            using (RequirePermissionsSimpleBatchStateChecker<ToolbarItem>.Use(new RequirePermissionsSimpleBatchStateChecker<ToolbarItem>()))
            {
                var context = new ToolbarConfigurationContext(ThemeManager.CurrentTheme, toolbar, scope.ServiceProvider);

                foreach (var contributor in Options.Contributors)
                {
                    await contributor.ConfigureToolbarAsync(context);
                }

                await CheckPermissionsAsync(scope.ServiceProvider, toolbar);
            }
        }

        return toolbar;
    }

    protected virtual async Task CheckPermissionsAsync(IServiceProvider serviceProvider, Toolbar toolbar)
    {
        foreach (var item in toolbar.Items.Where(x => !x.RequiredPermissionName.IsNullOrWhiteSpace()))
        {
            item.RequirePermissions(item.RequiredPermissionName!);
        }

        var checkPermissionsToolbarItems = toolbar.Items.Where(x => x.StateCheckers.Any()).ToArray();
        if (checkPermissionsToolbarItems.Any())
        {
            var result = await SimpleStateCheckerManager.IsEnabledAsync(checkPermissionsToolbarItems);

            var toBeDeleted = new HashSet<ToolbarItem>();
            foreach (var item in checkPermissionsToolbarItems)
            {
                if (!result[item])
                {
                    toBeDeleted.Add(item);
                }
            }

            toolbar.Items.RemoveAll(toBeDeleted.Contains);
        }
    }
}
