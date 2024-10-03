using System;

namespace Tiknas.Domain.Repositories;

public interface IEntityChangeTrackingProvider
{
    bool? Enabled { get; }

    IDisposable Change(bool? enabled);
}
