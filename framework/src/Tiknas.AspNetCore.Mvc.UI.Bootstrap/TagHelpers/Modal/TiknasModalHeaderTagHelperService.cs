using Localization.Resources.TiknasUi;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

public class TiknasModalHeaderTagHelperService : TiknasTagHelperService<TiknasModalHeaderTagHelper>
{
    protected IStringLocalizer<TiknasUiResource> L { get; }

    public TiknasModalHeaderTagHelperService(IStringLocalizer<TiknasUiResource> localizer)
    {
        L = localizer;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("modal-header");
        output.PreContent.SetHtmlContent(CreatePreContent());
        output.PostContent.SetHtmlContent(CreatePostContent());
    }

    protected virtual string CreatePreContent()
    {
        var title = new TagBuilder("h5");
        title.AddCssClass("modal-title");
        title.InnerHtml.AppendHtml(TagHelper.Title);

        return title.ToHtmlString();
    }

    protected virtual string CreatePostContent()
    {
        var button = new TagBuilder("button");
        button.AddCssClass("btn-close");
        button.Attributes.Add("type", "button");
        button.Attributes.Add("data-bs-dismiss", "modal");
        button.Attributes.Add("aria-label", L["Close"].Value);

        return button.ToHtmlString();
    }
}
