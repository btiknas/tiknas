using System;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.Localization;

public class LocalizableStringSerializer : ILocalizableStringSerializer, ITransientDependency
{
    protected TiknasLocalizationOptions LocalizationOptions { get; }

    public LocalizableStringSerializer(IOptions<TiknasLocalizationOptions> localizationOptions)
    {
        LocalizationOptions = localizationOptions.Value;
    }

    public virtual string? Serialize(ILocalizableString? localizableString)
    {
        if (localizableString == null)
        {
            return null;
        }

        if (localizableString is LocalizableString realLocalizableString)
        {
            return $"L:{realLocalizableString.ResourceName},{realLocalizableString.Name}";
        }

        if (localizableString is FixedLocalizableString fixedLocalizableString)
        {
            return $"F:{fixedLocalizableString.Value}";
        }

        throw new TiknasException($"Unknown {nameof(ILocalizableString)} type: {localizableString.GetType().FullName}");
    }

    public virtual ILocalizableString Deserialize(string value)
    {
        if (value.IsNullOrEmpty() ||
            value.Length < 3 ||
            value[1] != ':')
        {
            return new FixedLocalizableString(value);
        }

        var type = value[0];
        switch (type)
        {
            case 'F':
                return new FixedLocalizableString(value.Substring(2));
            case 'L':
                var commaPosition = value.IndexOf(',', 2);
                if (commaPosition == -1)
                {
                    throw new TiknasException("Invalid LocalizableString value: " + value);
                }

                var resourceName = value.Substring(2, commaPosition - 2);
                var name = value.Substring(commaPosition + 1);
                if (name.IsNullOrWhiteSpace())
                {
                    throw new TiknasException("Invalid LocalizableString value: " + value);
                }

                return LocalizableString.Create(name, resourceName);
            default:
                return new FixedLocalizableString(value);
        }
    }
}
