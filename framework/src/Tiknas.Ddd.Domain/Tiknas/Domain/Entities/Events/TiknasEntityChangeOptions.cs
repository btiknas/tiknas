namespace Tiknas.Domain.Entities.Events;

public class TiknasEntityChangeOptions
{
    /// <summary>
    /// Default: true.
    /// Publish the EntityUpdatedEvent when any navigation property changes.
    /// </summary>
    public bool PublishEntityUpdatedEventWhenNavigationChanges { get; set; } = true;
}
