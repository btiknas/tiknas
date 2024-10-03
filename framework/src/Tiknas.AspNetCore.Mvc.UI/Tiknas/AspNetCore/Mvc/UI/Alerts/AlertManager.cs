using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.UI.Alerts;

public class AlertManager : IAlertManager, IScopedDependency
{
    public AlertList Alerts { get; }

    public AlertManager()
    {
        Alerts = new AlertList();
    }
}
