using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Tiknas.Data;
using Tiknas.EntityFrameworkCore.Modeling;

namespace Tiknas.EntityFrameworkCore.DistributedEvents;

public static class EventInboxDbContextModelBuilderExtensions
{
    public static void ConfigureEventInbox([NotNull] this ModelBuilder builder)
    {
        builder.Entity<IncomingEventRecord>(b =>
        {
            b.ToTable(TiknasCommonDbProperties.DbTablePrefix + "EventInbox", TiknasCommonDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.EventName).IsRequired().HasMaxLength(IncomingEventRecord.MaxEventNameLength);
            b.Property(x => x.EventData).IsRequired();

            b.HasIndex(x => new { x.Processed, x.CreationTime });
            b.HasIndex(x => x.MessageId);
        });
    }
}
