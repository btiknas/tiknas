using System;
using JetBrains.Annotations;

namespace Tiknas.MultiTenancy;

public interface ICurrentTenant
{
    bool IsAvailable { get; }

    Guid? Id { get; }

    string? Name { get; }

    IDisposable Change(Guid? id, string? name = null);
}
