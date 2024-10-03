using System;
using System.Data;

namespace Tiknas.Uow;

public class TiknasUnitOfWorkOptions : ITiknasUnitOfWorkOptions
{
    /// <summary>
    /// Default: false.
    /// </summary>
    public bool IsTransactional { get; set; }

    public IsolationLevel? IsolationLevel { get; set; }

    /// <summary>
    /// Milliseconds
    /// </summary>
    public int? Timeout { get; set; }

    public TiknasUnitOfWorkOptions()
    {

    }

    public TiknasUnitOfWorkOptions(bool isTransactional = false, IsolationLevel? isolationLevel = null, int? timeout = null)
    {
        IsTransactional = isTransactional;
        IsolationLevel = isolationLevel;
        Timeout = timeout;
    }

    public TiknasUnitOfWorkOptions Clone()
    {
        return new TiknasUnitOfWorkOptions
        {
            IsTransactional = IsTransactional,
            IsolationLevel = IsolationLevel,
            Timeout = Timeout
        };
    }
}
