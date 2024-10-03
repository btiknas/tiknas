using Tiknas.Localization;

namespace Tiknas.ExceptionHandling;

public interface ILocalizeErrorMessage
{
    string LocalizeMessage(LocalizationContext context);
}
