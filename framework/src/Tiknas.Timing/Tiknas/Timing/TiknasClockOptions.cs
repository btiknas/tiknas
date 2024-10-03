using System;

namespace Tiknas.Timing;

public class TiknasClockOptions
{
    /// <summary>
    /// Default: <see cref="DateTimeKind.Unspecified"/>
    /// </summary>
    public DateTimeKind Kind { get; set; }

    public TiknasClockOptions()
    {
        Kind = DateTimeKind.Unspecified;
    }
}
