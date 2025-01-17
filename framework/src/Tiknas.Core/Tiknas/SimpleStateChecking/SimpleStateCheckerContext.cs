﻿using System;

namespace Tiknas.SimpleStateChecking;

public class SimpleStateCheckerContext<TState>
    where TState : IHasSimpleStateCheckers<TState>
{
    public IServiceProvider ServiceProvider { get; }

    public TState State { get; }

    public SimpleStateCheckerContext(IServiceProvider serviceProvider, TState state)
    {
        ServiceProvider = serviceProvider;
        State = state;
    }
}
