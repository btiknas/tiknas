using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public abstract class TiknasBundleTagHelperService<TTagHelper, TService> : TiknasTagHelperService<TTagHelper>
    where TTagHelper : TiknasTagHelper<TTagHelper, TService>, IBundleTagHelper
    where TService : class, ITiknasTagHelperService<TTagHelper>
{
    protected TiknasTagHelperResourceService ResourceService { get; }

    protected TiknasBundleTagHelperService(TiknasTagHelperResourceService resourceService)
    {
        ResourceService = resourceService;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await ResourceService.ProcessAsync(
            TagHelper.ViewContext,
            TagHelper,
            context,
            output,
            await GetBundleItems(context, output),
            TagHelper.GetNameOrNull()
        );
    }

    protected virtual async Task<List<BundleTagHelperItem>> GetBundleItems(TagHelperContext context, TagHelperOutput output)
    {
        var bundleItems = new List<BundleTagHelperItem>();
        context.Items[TiknasTagHelperConsts.ContextBundleItemListKey] = bundleItems;
        await output.GetChildContentAsync(); //TODO: Is there a way of executing children without getting content?
        return bundleItems;
    }
}
