﻿using System.Linq;
using System.Threading.Tasks;
using Blazorise.DataGrid;
using Localization.Resources.TiknasUi;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Tiknas.BlazoriseUI.Components;

public partial class DataGridEntityActionsColumn<TItem> : DataGridColumn<TItem>
{
    [Inject]
    public IStringLocalizer<TiknasUiResource> UiLocalizer { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await SetDefaultValuesAsync();
    }

    protected virtual ValueTask SetDefaultValuesAsync()
    {
        Caption = UiLocalizer["Actions"];
        Width = "150px";
        Sortable = false;
        Field = typeof(TItem).GetProperties().First().Name;
        return ValueTask.CompletedTask;
    }
}
