using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Tiknas.Logging;

public interface IInitLogger<out T> : ILogger<T>
{
    public List<TiknasInitLogEntry> Entries { get; }
}
