using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tiknas.DependencyInjection;
using Tiknas.MultiTenancy;

namespace Tiknas.MongoDB;

public class TiknasMongoDbContextOptions
{
    internal Dictionary<MultiTenantDbContextType, Type> DbContextReplacements { get; }

    public Action<MongoClientSettings>? MongoClientSettingsConfigurer { get; set; }

    public TiknasMongoDbContextOptions()
    {
        DbContextReplacements = new Dictionary<MultiTenantDbContextType, Type>();
    }

    internal Type GetReplacedTypeOrSelf(Type dbContextType, MultiTenancySides multiTenancySides = MultiTenancySides.Both)
    {
        var replacementType = dbContextType;
        while (true)
        {
            var foundType = DbContextReplacements.LastOrDefault(x => x.Key.Type == replacementType && x.Key.MultiTenancySide.HasFlag(multiTenancySides));
            if (!foundType.Equals(default(KeyValuePair<MultiTenantDbContextType, Type>)))
            {
                if (foundType.Value == dbContextType)
                {
                    throw new TiknasException(
                        "Circular DbContext replacement found for " +
                        dbContextType.AssemblyQualifiedName
                    );
                }
                replacementType = foundType.Value;
            }
            else
            {
                return replacementType;
            }
        }
    }
}
