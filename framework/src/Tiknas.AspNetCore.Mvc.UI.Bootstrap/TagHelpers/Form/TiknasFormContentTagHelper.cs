using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[HtmlTargetElement("tiknas-form-content", TagStructure = TagStructure.WithoutEndTag)]
public class TiknasFormContentTagHelper : TiknasTagHelper<TiknasFormContentTagHelper, TiknasFormContentTagHelperService>, ITransientDependency
{
    public TiknasFormContentTagHelper(TiknasFormContentTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
