﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tiknas.Uow;

public interface ITransactionApi : IDisposable
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
