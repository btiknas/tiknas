using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.Alerts;

public interface IAlertManager
{
    AlertList Alerts { get; }
}
