using Tiknas.Timing;

namespace Tiknas.MongoDB;

public class TiknasMongoDbOptions
{
    /// <summary>
    /// Serializer the datetime based on <see cref="TiknasClockOptions.Kind"/> in MongoDb.
    /// Default: true.
    /// </summary>
    public bool UseTiknasClockHandleDateTime { get; set; }

    public TiknasMongoDbOptions()
    {
        UseTiknasClockHandleDateTime = true;
    }
}
