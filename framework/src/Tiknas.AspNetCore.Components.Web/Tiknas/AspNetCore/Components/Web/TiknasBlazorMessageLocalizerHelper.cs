﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Extensions.Localization;

namespace Tiknas.AspNetCore.Components.Web;

public class TiknasBlazorMessageLocalizerHelper<T>
{
    private readonly IStringLocalizer<T> stringLocalizer;

    public TiknasBlazorMessageLocalizerHelper(IStringLocalizer<T> stringLocalizer)
    {
        this.stringLocalizer = stringLocalizer;
    }

    public string Localize(string message, IEnumerable<string>? arguments = null)
    {
        try
        {
            var argumentsList = arguments?.ToList();
            return argumentsList?.Count > 0
                ? stringLocalizer[message, LocalizeMessageArguments(argumentsList).ToArray()]
                : stringLocalizer[message];
        }
        catch
        {
            return stringLocalizer[message];
        }
    }

    private IEnumerable<object> LocalizeMessageArguments(List<string> arguments)
    {
        foreach (var argument in arguments)
        {
            // first try to localize with "DisplayName:{Name}"
            var localization = stringLocalizer[$"DisplayName:{argument}"];

            if (localization.ResourceNotFound)
            {
                // then try to localize with just "{Name}"
                localization = stringLocalizer[argument];
            }

            yield return localization;
        }
    }
}
