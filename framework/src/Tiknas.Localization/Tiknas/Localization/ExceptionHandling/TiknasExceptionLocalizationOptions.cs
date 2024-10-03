using System;
using System.Collections.Generic;

namespace Tiknas.Localization.ExceptionHandling;

public class TiknasExceptionLocalizationOptions
{
    public Dictionary<string, Type> ErrorCodeNamespaceMappings { get; }

    public TiknasExceptionLocalizationOptions()
    {
        ErrorCodeNamespaceMappings = new Dictionary<string, Type>();
    }

    public void MapCodeNamespace(string errorCodeNamespace, Type type)
    {
        ErrorCodeNamespaceMappings[errorCodeNamespace] = type;
    }
}
