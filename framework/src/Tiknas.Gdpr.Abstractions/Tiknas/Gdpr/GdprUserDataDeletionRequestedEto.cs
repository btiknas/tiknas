using System;

namespace Tiknas.Gdpr;

[Serializable]
public class GdprUserDataDeletionRequestedEto
{
    public Guid UserId { get; set; }
}