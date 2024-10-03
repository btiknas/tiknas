using Microsoft.Extensions.Localization;

namespace Tiknas.Localization;

public interface ITemplateLocalizer
{
    string Localize(IStringLocalizer localizer, string text);
}
