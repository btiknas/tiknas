using JetBrains.Annotations;

namespace Tiknas.MongoDB;

public class TiknasMongoModelBuilderConfigurationOptions
{
    [NotNull]
    public string CollectionPrefix {
        get => _collectionPrefix;
        set {
            Check.NotNull(value, nameof(value), $"{nameof(CollectionPrefix)} can not be null! Set to empty string if you don't want a collection prefix.");
            _collectionPrefix = value;
        }
    }

    private string _collectionPrefix = default!;

    public TiknasMongoModelBuilderConfigurationOptions([NotNull] string collectionPrefix = "")
    {
        Check.NotNull(collectionPrefix, nameof(collectionPrefix));

        CollectionPrefix = collectionPrefix;
    }
}
