﻿using System;
using JetBrains.Annotations;

namespace Tiknas.AspNetCore.Components.ExceptionHandling;

public class UserExceptionInformerContext
{
    [NotNull]
    public Exception Exception { get; }

    public UserExceptionInformerContext(Exception exception)
    {
        Exception = exception;
    }
}
