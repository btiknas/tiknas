﻿using System;
using System.Data;

namespace Tiknas.Uow;

//TODO: Implement default options!

/// <summary>
/// Global (default) unit of work options
/// </summary>
public class TiknasUnitOfWorkDefaultOptions
{
    /// <summary>
    /// Default value: <see cref="UnitOfWorkTransactionBehavior.Auto"/>.
    /// </summary>
    public UnitOfWorkTransactionBehavior TransactionBehavior { get; set; } = UnitOfWorkTransactionBehavior.Auto;

    public IsolationLevel? IsolationLevel { get; set; }

    public int? Timeout { get; set; }

    internal TiknasUnitOfWorkOptions Normalize(TiknasUnitOfWorkOptions options)
    {
        if (options.IsolationLevel == null)
        {
            options.IsolationLevel = IsolationLevel;
        }

        if (options.Timeout == null)
        {
            options.Timeout = Timeout;
        }

        return options;
    }

    public bool CalculateIsTransactional(bool autoValue)
    {
        switch (TransactionBehavior)
        {
            case UnitOfWorkTransactionBehavior.Enabled:
                return true;
            case UnitOfWorkTransactionBehavior.Disabled:
                return false;
            case UnitOfWorkTransactionBehavior.Auto:
                return autoValue;
            default:
                throw new TiknasException("Not implemented TransactionBehavior value: " + TransactionBehavior);
        }
    }
}
