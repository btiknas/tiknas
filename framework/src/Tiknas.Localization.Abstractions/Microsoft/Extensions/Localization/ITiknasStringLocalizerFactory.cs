using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Microsoft.Extensions.Localization;

public interface ITiknasStringLocalizerFactory
{
    IStringLocalizer? CreateDefaultOrNull();

    IStringLocalizer? CreateByResourceNameOrNull([NotNull] string resourceName);
    
    Task<IStringLocalizer?> CreateByResourceNameOrNullAsync([NotNull] string resourceName);
}
