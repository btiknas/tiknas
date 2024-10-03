namespace Tiknas.EventBus.Distributed;

public static class TiknasDistributedEventBusExtensions
{
    public static ISupportsEventBoxes AsSupportsEventBoxes(this IDistributedEventBus eventBus)
    {
        var supportsEventBoxes = eventBus as ISupportsEventBoxes;
        if (supportsEventBoxes == null)
        {
            throw new TiknasException($"Given type ({eventBus.GetType().AssemblyQualifiedName}) should implement {nameof(ISupportsEventBoxes)}!");
        }

        return supportsEventBoxes;
    }
}
