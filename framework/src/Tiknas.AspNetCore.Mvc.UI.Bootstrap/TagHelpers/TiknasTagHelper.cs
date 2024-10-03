using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.DependencyInjection;
using Tiknas.Threading;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

public abstract class TiknasTagHelper : TagHelper, ITransientDependency
{

}

public abstract class TiknasTagHelper<TTagHelper, TService> : TiknasTagHelper
    where TTagHelper : TiknasTagHelper<TTagHelper, TService>
    where TService : class, ITiknasTagHelperService<TTagHelper>
{
    protected TService Service { get; }

    public override int Order => Service.Order;

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; } = default!;

    protected TiknasTagHelper(TService service)
    {
        Service = service;
        Service.As<TiknasTagHelperService<TTagHelper>>().TagHelper = (TTagHelper)this;
    }

    public override void Init(TagHelperContext context)
    {
        Service.Init(context);
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        Service.Process(context, output);
    }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        return Service.ProcessAsync(context, output);
    }
}
