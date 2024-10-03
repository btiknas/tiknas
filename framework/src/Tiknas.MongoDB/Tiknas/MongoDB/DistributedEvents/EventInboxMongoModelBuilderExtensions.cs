using Tiknas.Data;

namespace Tiknas.MongoDB.DistributedEvents;

public static class EventInboxMongoModelBuilderExtensions
{
    public static void ConfigureEventInbox(this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<IncomingEventRecord>(b =>
        {
            b.CollectionName = TiknasCommonDbProperties.DbTablePrefix + "EventInbox";
        });
    }
}
