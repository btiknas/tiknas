using System.Threading.Tasks;
using JetBrains.Annotations;
using Tiknas;

namespace Microsoft.Extensions.Localization;

public static class TiknasStringLocalizerFactoryExtensions
{
    public static IStringLocalizer? CreateDefaultOrNull(this IStringLocalizerFactory localizerFactory)
    {
        return (localizerFactory as ITiknasStringLocalizerFactory)
            ?.CreateDefaultOrNull();
    }

    public static IStringLocalizer? CreateByResourceNameOrNull(
        this IStringLocalizerFactory localizerFactory,
        string resourceName)
    {
        return (localizerFactory as ITiknasStringLocalizerFactory)
            ?.CreateByResourceNameOrNull(resourceName);
    }
    
    [NotNull]
    public static IStringLocalizer CreateByResourceName(
        this IStringLocalizerFactory localizerFactory,
        string resourceName)
    {
        var localizer = localizerFactory.CreateByResourceNameOrNull(resourceName);
        if (localizer == null)
        {
            throw new TiknasException("Couldn't find a localizer with given resource name: " + resourceName);
        }
        
        return localizer;
    }
    
    public static async Task<IStringLocalizer?> CreateByResourceNameOrNullAsync(
        this IStringLocalizerFactory localizerFactory,
        string resourceName)
    {
        var tiknasLocalizerFactory = localizerFactory as ITiknasStringLocalizerFactory;
        if (tiknasLocalizerFactory == null)
        {
            return null;
        } 
        
        return await tiknasLocalizerFactory.CreateByResourceNameOrNullAsync(resourceName);
    }
    
    [NotNull]
    public async static Task<IStringLocalizer> CreateByResourceNameAsync(
        this IStringLocalizerFactory localizerFactory,
        string resourceName)
    {
        var localizer = await localizerFactory.CreateByResourceNameOrNullAsync(resourceName);
        if (localizer == null)
        {
            throw new TiknasException("Couldn't find a localizer with given resource name: " + resourceName);
        }
        
        return localizer;
    }
}
