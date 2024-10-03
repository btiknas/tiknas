using System;
using System.Reflection;
using MongoDB.Bson.Serialization;
using Tiknas.Data;

namespace Tiknas.MongoDB;

public static class TiknasBsonClassMapExtensions
{
    public static void ConfigureTiknasConventions(this BsonClassMap map)
    {
        map.AutoMap();
        map.TryConfigureExtraProperties();
    }

    public static bool TryConfigureExtraProperties<TEntity>(this BsonClassMap<TEntity> map)
        where TEntity : class, IHasExtraProperties
    {
        var property = map.ClassType.GetProperty(
            nameof(IHasExtraProperties.ExtraProperties),
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty
        );

        if (property?.DeclaringType != map.ClassType)
        {
            return false;
        }

        map.SetExtraElementsMember(new BsonMemberMap(
            map,
            typeof(TEntity).GetMember(nameof(IHasExtraProperties.ExtraProperties))[0])
        );

        return true;
    }

    public static bool TryConfigureExtraProperties(this BsonClassMap map)
    {
        if (!map.ClassType.IsAssignableTo<IHasExtraProperties>())
        {
            return false;
        }

        var property = map.ClassType.GetProperty(
            nameof(IHasExtraProperties.ExtraProperties),
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty
        );

        if (property?.DeclaringType != map.ClassType)
        {
            return false;
        }

        map.MapExtraElementsMember(property);

        return true;
    }
}
