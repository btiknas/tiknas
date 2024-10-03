using Microsoft.Extensions.DependencyInjection;

namespace Tiknas.Autofac;

public class Autofac_DependencyInjection_Standard_Tests : DependencyInjection_Standard_Tests
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
