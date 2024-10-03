using System;

namespace Tiknas.Gdpr;

[Serializable]
public class GdprUserDataRequestedEto
{
    public Guid UserId { get; set; }
    
    public Guid RequestId { get; set; }
}