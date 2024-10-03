namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

public class TiknasModalHeaderTagHelper : TiknasTagHelper<TiknasModalHeaderTagHelper, TiknasModalHeaderTagHelperService>
{
    public string Title { get; set; } = default!;

    public TiknasModalHeaderTagHelper(TiknasModalHeaderTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
