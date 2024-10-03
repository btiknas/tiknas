using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.UI.Widgets;

public interface IWidgetManager : ITransientDependency
{
    Task<bool> IsGrantedAsync(Type widgetComponentType);

    Task<bool> IsGrantedAsync(string name);
}
