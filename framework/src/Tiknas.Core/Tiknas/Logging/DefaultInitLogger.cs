﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Tiknas.Logging;

public class DefaultInitLogger<T> : IInitLogger<T>
{
    public List<TiknasInitLogEntry> Entries { get; }

    public DefaultInitLogger()
    {
        Entries = new List<TiknasInitLogEntry>();
    }

    public virtual void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        Entries.Add(new TiknasInitLogEntry
        {
            LogLevel = logLevel,
            EventId = eventId,
            State = state!,
            Exception = exception,
            Formatter = (s, e) => formatter((TState)s, e),
        });
    }

    public virtual bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public virtual IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return NullDisposable.Instance;
    }
}
