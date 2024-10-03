using System;

namespace Tiknas.Gdpr;

[Serializable]
public class GdprUserDataPreparedEto
{
    public Guid RequestId { get; set; }

    public string Provider { get; set; } = default!;
    
    public GdprDataInfo Data { get; set; } = default!;
}