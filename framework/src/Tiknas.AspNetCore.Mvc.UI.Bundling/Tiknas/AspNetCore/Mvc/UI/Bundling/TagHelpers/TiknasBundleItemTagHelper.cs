using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public abstract class TiknasBundleItemTagHelper<TTagHelper, TTagHelperService> : TiknasTagHelper<TTagHelper, TTagHelperService>, IBundleItemTagHelper
    where TTagHelper : TiknasTagHelper<TTagHelper, TTagHelperService>, IBundleItemTagHelper
    where TTagHelperService : TiknasBundleItemTagHelperService<TTagHelper, TTagHelperService>
{
    /// <summary>
    /// A file path.
    /// </summary>
    public string? Src { get; set; }

    /// <summary>
    /// A bundle contributor type.
    /// </summary>
    public Type? Type { get; set; }

    protected TiknasBundleItemTagHelper(TTagHelperService service)
        : base(service)
    {
    }

    public string GetNameOrNull()
    {
        if (Type != null)
        {
            return Type.FullName!;
        }

        if (Src != null)
        {
            return Src
                .RemovePreFix("/")
                .RemovePostFix(StringComparison.OrdinalIgnoreCase, "." + GetFileExtension())
                .Replace("/", ".");
        }

        throw new TiknasException("tiknas-script tag helper requires to set either src or type!");
    }

    public BundleTagHelperItem CreateBundleTagHelperItem()
    {
        if (Type != null)
        {
            return new BundleTagHelperContributorTypeItem(Type);
        }

        if (Src != null)
        {
            return new BundleTagHelperFileItem(new BundleFile(Src));
        }

        throw new TiknasException("tiknas-script tag helper requires to set either src or type!");
    }

    protected abstract string GetFileExtension();
}
