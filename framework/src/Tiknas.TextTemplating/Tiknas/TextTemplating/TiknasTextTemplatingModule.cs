using System;
using Tiknas.Modularity;
using Tiknas.TextTemplating.Scriban;

namespace Tiknas.TextTemplating;

[Obsolete("This module will be removed in the future. Please use TiknasTextTemplatingScribanModule or TiknasTextTemplatingRazorModule.")]
[DependsOn(typeof(TiknasTextTemplatingScribanModule))]
public class TiknasTextTemplatingModule : TiknasModule
{

}
