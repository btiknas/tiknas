﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Blazorise.DataGrid;
using Blazorise.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Tiknas.AspNetCore.Components.Web.Extensibility.TableColumns;

namespace Tiknas.BlazoriseUI.Components;

public partial class TiknasExtensibleDataGrid<TItem> : ComponentBase
{
    protected const string DataFieldAttributeName = "Data";

    protected Dictionary<string, DataGridEntityActionsColumn<TItem>> ActionColumns =
        new Dictionary<string, DataGridEntityActionsColumn<TItem>>();

    protected Regex ExtensionPropertiesRegex = new Regex(@"ExtraProperties\[(.*?)\]");

    [Parameter] public IEnumerable<TItem> Data { get; set; } = default!;

    [Parameter] public EventCallback<DataGridReadDataEventArgs<TItem>> ReadData { get; set; }

    [Parameter] public int? TotalItems { get; set; }

    [Parameter] public bool ShowPager { get; set; }

    [Parameter] public int PageSize { get; set; }

    [Parameter] public IEnumerable<TableColumn> Columns { get; set; } = default!;

    [Parameter] public int CurrentPage { get; set; } = 1;

    [Parameter] public string? Class { get; set; }

    [Parameter] public bool Responsive { get; set; }
    
    [Parameter] public bool AutoGenerateColumns { get; set; }

    [Inject]
    public IStringLocalizerFactory StringLocalizerFactory { get; set; } = default!;

    protected virtual RenderFragment RenderCustomTableColumnComponent(Type type, object data)
    {
        return (builder) =>
        {
            builder.OpenComponent(type);
            builder.AddAttribute(0, DataFieldAttributeName, data);
            builder.CloseComponent();
        };
    }

    protected virtual string GetConvertedFieldValue(TItem item, TableColumn columnDefinition)
    {
        var convertedValue = columnDefinition.ValueConverter!.Invoke(item!);
        if (!columnDefinition.DisplayFormat.IsNullOrEmpty())
        {
            return string.Format(columnDefinition.DisplayFormatProvider, columnDefinition.DisplayFormat!,
                convertedValue);
        }

        return convertedValue;
    }
}
