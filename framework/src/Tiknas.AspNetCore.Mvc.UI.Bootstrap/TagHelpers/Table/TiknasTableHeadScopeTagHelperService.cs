using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table;

public class TiknasTableHeadScopeTagHelperService : TiknasTagHelperService<TiknasTableHeadScopeTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetScope(context, output);
    }

    protected virtual void SetScope(TagHelperContext context, TagHelperOutput output)
    {
        switch (TagHelper.Scope)
        {
            case TiknasThScope.Default:
                return;
            case TiknasThScope.Row:
                output.Attributes.Add("scope", "row");
                return;
            case TiknasThScope.Column:
                output.Attributes.Add("scope", "col");
                return;
        }
    }
}
