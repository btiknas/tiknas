using System;
using System.Collections.Generic;
using Tiknas.Data;
using Tiknas.MultiTenancy;

namespace Tiknas.Emailing;

[Serializable]
public class BackgroundEmailSendingJobArgs : IMultiTenant
{
    public Guid? TenantId { get; set; }

    public string? From { get; set; }

    public string To { get; set; } = default!;

    public string? Subject { get; set; }

    public string? Body { get; set; }

    /// <summary>
    /// Default: true.
    /// </summary>
    public bool IsBodyHtml { get; set; } = true;

    public AdditionalEmailSendingArgs? AdditionalEmailSendingArgs { get; set; }
}
