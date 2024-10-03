using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tiknas.Timing;

namespace Tiknas.EntityFrameworkCore.ValueConverters;

public class TiknasDateTimeValueConverter : ValueConverter<DateTime, DateTime>
{
    public static IClock? Clock { get; set; }

    public TiknasDateTimeValueConverter(ConverterMappingHints? mappingHints = null)
        : base(
            x => Clock!.Normalize(x),
            x => Clock!.Normalize(x),
            mappingHints)
    {
    }
}

public class TiknasNullableDateTimeValueConverter : ValueConverter<DateTime?, DateTime?>
{
    public static IClock? Clock { get; set; }

    public TiknasNullableDateTimeValueConverter(ConverterMappingHints? mappingHints = null)
        : base(
            x => x.HasValue ? Clock!.Normalize(x.Value) : x,
            x => x.HasValue ? Clock!.Normalize(x.Value) : x,
            mappingHints)
    {
    }
}
