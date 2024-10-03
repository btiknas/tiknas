using System.Collections.Generic;

namespace Tiknas.TestBase.Logging;

public interface ICanLogOnObject
{
    List<string> Logs { get; }
}
