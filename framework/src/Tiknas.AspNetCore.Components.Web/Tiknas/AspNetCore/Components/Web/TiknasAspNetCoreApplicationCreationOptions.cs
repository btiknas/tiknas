namespace Tiknas.AspNetCore.Components.Web;

public class TiknasAspNetCoreApplicationCreationOptions
{
    public TiknasApplicationCreationOptions ApplicationCreationOptions { get; }

    public TiknasAspNetCoreApplicationCreationOptions(
        TiknasApplicationCreationOptions applicationCreationOptions)
    {
        ApplicationCreationOptions = applicationCreationOptions;
    }
}
