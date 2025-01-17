﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JetBrains.Annotations;
using Tiknas.Localization;

namespace Tiknas.AspNetCore.Mvc.UI.Widgets;

public class WidgetDefinitionCollection
{
    private readonly Dictionary<string, WidgetDefinition> _widgetsByName;
    private readonly Dictionary<Type, WidgetDefinition> _widgetsByType;

    public WidgetDefinitionCollection()
    {
        _widgetsByName = new Dictionary<string, WidgetDefinition>();
        _widgetsByType = new Dictionary<Type, WidgetDefinition>();
    }

    public void Add(WidgetDefinition widget)
    {
        var existingWidget = _widgetsByName.GetOrDefault(widget.Name);
        if (existingWidget != null)
        {
            _widgetsByType[existingWidget.ViewComponentType] = widget;
        }

        _widgetsByName[widget.Name] = widget;
        _widgetsByType[widget.ViewComponentType] = widget;
    }

    public WidgetDefinition Add<TViewComponent>(
        ILocalizableString? displayName = null)
    {
        return Add(typeof(TViewComponent), displayName);
    }

    public WidgetDefinition Add(
        [NotNull] Type viewComponentType,
        ILocalizableString? displayName = null)
    {
        var widget = new WidgetDefinition(viewComponentType, displayName);
        Add(widget);
        return widget;
    }

    public WidgetDefinition? Find(string name)
    {
        return _widgetsByName.GetOrDefault(name);
    }

    public WidgetDefinition? Find<TViewComponent>()
    {
        return Find(typeof(TViewComponent));
    }

    public WidgetDefinition? Find(Type viewComponentType)
    {
        return _widgetsByType.GetOrDefault(viewComponentType);
    }

    public IReadOnlyCollection<WidgetDefinition> GetAll()
    {
        return _widgetsByName.Values.ToImmutableArray();
    }
}
