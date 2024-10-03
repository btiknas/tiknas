using System;

namespace Tiknas.Tracing;

public interface ICorrelationIdProvider
{
    string? Get();

    IDisposable Change(string? correlationId);
}
