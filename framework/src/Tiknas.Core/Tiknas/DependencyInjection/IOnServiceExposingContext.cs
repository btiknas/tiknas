using System;
using System.Collections.Generic;

namespace Tiknas.DependencyInjection;

public interface IOnServiceExposingContext
{
    Type ImplementationType { get; }

    List<ServiceIdentifier> ExposedTypes { get; }
}
