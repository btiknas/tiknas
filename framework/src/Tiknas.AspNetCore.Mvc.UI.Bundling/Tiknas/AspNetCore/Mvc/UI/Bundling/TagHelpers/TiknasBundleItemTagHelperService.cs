using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public abstract class TiknasBundleItemTagHelperService<TTagHelper, TService> : TiknasTagHelperService<TTagHelper>
    where TTagHelper : TiknasTagHelper<TTagHelper, TService>, IBundleItemTagHelper
    where TService : class, ITiknasTagHelperService<TTagHelper>
{
    protected TiknasTagHelperResourceService ResourceService { get; }

    protected TiknasBundleItemTagHelperService(TiknasTagHelperResourceService resourceService)
    {
        ResourceService = resourceService;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var tagHelperItems = context.Items.GetOrDefault(TiknasTagHelperConsts.ContextBundleItemListKey) as List<BundleTagHelperItem>;
        if (tagHelperItems != null)
        {
            output.SuppressOutput();
            tagHelperItems.Add(TagHelper.CreateBundleTagHelperItem());
        }
        else
        {
            await ResourceService.ProcessAsync(
                TagHelper.ViewContext,
                TagHelper,
                context,
                output,
                new List<BundleTagHelperItem>
                {
                        TagHelper.CreateBundleTagHelperItem()
                },
                TagHelper.GetNameOrNull()
            );
        }
    }
}
