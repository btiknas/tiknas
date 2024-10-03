using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class TiknasCardBodyTagHelperService : TiknasTagHelperService<TiknasCardBodyTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("card-body");

        ProcessTitle(output);
        ProcessSubtitle(output);
    }

    protected virtual void ProcessTitle(TagHelperOutput output)
    {
        if (!TagHelper.Title.IsNullOrWhiteSpace())
        {
            var cardTitle = new TagBuilder(TiknasCardTitleTagHelper.DefaultHeading.ToHtmlTag());
            cardTitle.AddCssClass("card-title");
            cardTitle.InnerHtml.AppendHtml(TagHelper.Title!);
            output.PreContent.AppendHtml(cardTitle);
        }
    }

    protected virtual void ProcessSubtitle(TagHelperOutput output)
    {
        if (!TagHelper.Subtitle.IsNullOrWhiteSpace())
        {
            var cardSubtitle = new TagBuilder(TiknasCardSubtitleTagHelper.DefaultHeading.ToHtmlTag());
            cardSubtitle.AddCssClass("card-subtitle mb-2");
            cardSubtitle.InnerHtml.AppendHtml(TagHelper.Subtitle!);
            output.PreContent.AppendHtml(cardSubtitle);
        }
    }
}
