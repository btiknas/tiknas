using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Tiknas.Data;
using Tiknas.EntityFrameworkCore.Modeling;

namespace Tiknas.EntityFrameworkCore.DistributedEvents;

public static class EventOutboxDbContextModelBuilderExtensions
{
    public static void ConfigureEventOutbox([NotNull] this ModelBuilder builder)
    {
        builder.Entity<OutgoingEventRecord>(b =>
        {
            b.ToTable(TiknasCommonDbProperties.DbTablePrefix + "EventOutbox", TiknasCommonDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.EventName).IsRequired().HasMaxLength(OutgoingEventRecord.MaxEventNameLength);
            b.Property(x => x.EventData).IsRequired();

            b.HasIndex(x => x.CreationTime);
        });
    }
}
