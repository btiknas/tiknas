using JetBrains.Annotations;
using Tiknas.Data;

namespace Tiknas.MongoDB.DistributedEvents;

public static class EventOutboxMongoModelBuilderExtensions
{
    public static void ConfigureEventOutbox([NotNull] this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<OutgoingEventRecord>(b =>
        {
            b.CollectionName = TiknasCommonDbProperties.DbTablePrefix + "EventOutbox";
        });
    }
}
