﻿using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Extensions;

public static class ModelExplorerExtensions
{
    public static T? GetAttribute<T>(this ModelExplorer property) where T : Attribute
    {
        return property?.Metadata?.ContainerType?.GetTypeInfo()?.GetProperty(property.Metadata.PropertyName!)?.GetCustomAttribute<T>();
    }

    public static int GetDisplayOrder(this ModelExplorer explorer)
    {
        return GetAttribute<DisplayOrder>(explorer)?.Number ?? DisplayOrder.Default;
    }
}
