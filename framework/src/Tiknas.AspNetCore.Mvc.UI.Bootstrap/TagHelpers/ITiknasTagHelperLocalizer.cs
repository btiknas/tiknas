using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

public interface ITiknasTagHelperLocalizer : ITransientDependency
{
    string GetLocalizedText(string text, ModelExplorer explorer);

    IStringLocalizer? GetLocalizerOrNull(ModelExplorer explorer);

    IStringLocalizer? GetLocalizerOrNull(Assembly assembly);
}
