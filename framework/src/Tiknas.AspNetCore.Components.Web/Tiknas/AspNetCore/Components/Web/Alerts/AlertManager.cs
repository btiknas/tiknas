using Tiknas.AspNetCore.Components.Alerts;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.Web.Alerts;

public class AlertManager : IAlertManager, IScopedDependency
{
    public AlertList Alerts { get; }

    public AlertManager()
    {
        Alerts = new AlertList();
    }
}
