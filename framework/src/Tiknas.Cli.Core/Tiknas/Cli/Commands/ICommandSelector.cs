using System;
using Tiknas.Cli.Args;

namespace Tiknas.Cli.Commands;

public interface ICommandSelector
{
    Type Select(CommandLineArgs commandLineArgs);
}
