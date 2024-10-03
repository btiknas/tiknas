using System;
using System.Threading.Tasks;
using Tiknas.DynamicProxy;

namespace Tiknas.Autofac.Interception;

public class Autofac_Interception_Test : TiknasInterceptionTestBase<AutofacTestModule>
{
    protected override Task<Action<TiknasApplicationCreationOptions>> SetTiknasApplicationCreationOptionsAsync()
    {
        return Task.FromResult<Action<TiknasApplicationCreationOptions>>(options => options.UseAutofac());
    }
}
