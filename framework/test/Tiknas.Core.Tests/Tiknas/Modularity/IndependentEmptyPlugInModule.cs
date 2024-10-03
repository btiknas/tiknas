namespace Tiknas.Modularity;

public class IndependentEmptyPlugInModule : TestModuleBase
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        base.PreConfigureServices(context);
        SkipAutoServiceRegistration = true;
    }
}
