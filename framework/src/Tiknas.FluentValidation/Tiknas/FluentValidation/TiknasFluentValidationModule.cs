using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.Validation;

namespace Tiknas.FluentValidation;

[DependsOn(
    typeof(TiknasValidationModule)
    )]
public class TiknasFluentValidationModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new TiknasFluentValidationConventionalRegistrar());
    }
}
