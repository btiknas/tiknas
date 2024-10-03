using System;
using System.Collections.Generic;
using Tiknas.Aspects;
using Tiknas.DependencyInjection;

namespace Tiknas.Http.Modeling;

public class TiknasApiDescriptionModelOptions
{
    public HashSet<Type> IgnoredInterfaces { get; }

    public TiknasApiDescriptionModelOptions()
    {
        IgnoredInterfaces = new HashSet<Type>
            {
                typeof(ITransientDependency),
                typeof(ISingletonDependency),
                typeof(IDisposable),
                typeof(IAvoidDuplicateCrossCuttingConcerns)
            };
    }
}
