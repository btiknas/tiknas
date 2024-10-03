using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

public class PageToolbarManager : IPageToolbarManager, ITransientDependency
{
    protected TiknasPageToolbarOptions Options { get; }
    protected IServiceScopeFactory ServiceScopeFactory { get; }

    public PageToolbarManager(
        IOptions<TiknasPageToolbarOptions> options,
        IServiceScopeFactory serviceScopeFactory)
    {
        Options = options.Value;
        ServiceScopeFactory = serviceScopeFactory;
    }

    public virtual async Task<PageToolbarItem[]> GetItemsAsync(string pageName)
    {
        var toolbar = Options.Toolbars.GetOrDefault(pageName);
        if (toolbar == null || !toolbar.Contributors.Any())
        {
            return Array.Empty<PageToolbarItem>();
        }

        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var context = new PageToolbarContributionContext(pageName, scope.ServiceProvider);

            foreach (var contributor in toolbar.Contributors)
            {
                await contributor.ContributeAsync(context);
            }

            return context.Items.OrderBy(i => i.Order).ToArray();
        }
    }
}
