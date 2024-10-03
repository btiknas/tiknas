using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Tiknas.AspNetCore.Components.Web;
using Tiknas.Data;
using Tiknas.ObjectExtending;

namespace Tiknas.BlazoriseUI.Components.ObjectExtending;

public partial class ExtensionProperties<TEntityType, TResourceType> : ComponentBase
    where TEntityType : IHasExtraProperties
{
    [Inject]
    public IStringLocalizerFactory StringLocalizerFactory { get; set; } = default!;

    [Parameter]
    public TiknasBlazorMessageLocalizerHelper<TResourceType> LH { get; set; } = default!;

    [Parameter]
    public TEntityType Entity { get; set; } = default!;

    [Inject]
    public IServiceProvider ServiceProvider { get; set; } = default!;

    [Parameter]
    public ExtensionPropertyModalType? ModalType { get; set; }

    public ImmutableList<ObjectExtensionPropertyInfo> Properties { get; set; } = ImmutableList<ObjectExtensionPropertyInfo>.Empty;

    protected async override Task OnInitializedAsync()
    {
        Properties = await ObjectExtensionManager.Instance.GetPropertiesAndCheckPolicyAsync<TEntityType>(ServiceProvider);
    }
}
