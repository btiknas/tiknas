namespace Tiknas.Domain.Entities.Events.Distributed;

public class TiknasDistributedEntityEventOptions
{
    public IAutoEntityDistributedEventSelectorList AutoEventSelectors { get; }

    public EtoMappingDictionary EtoMappings { get; set; }

    public TiknasDistributedEntityEventOptions()
    {
        AutoEventSelectors = new AutoEntityDistributedEventSelectorList();
        EtoMappings = new EtoMappingDictionary();
    }
}
