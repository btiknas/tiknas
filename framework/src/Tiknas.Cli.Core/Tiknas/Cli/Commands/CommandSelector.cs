using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Tiknas.Cli.Args;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Commands;

public class CommandSelector : ICommandSelector, ITransientDependency
{
    protected TiknasCliOptions Options { get; }

    public CommandSelector(IOptions<TiknasCliOptions> options)
    {
        Options = options.Value;
    }

    public Type Select(CommandLineArgs commandLineArgs)
    {
        if (commandLineArgs.Command.IsNullOrWhiteSpace())
        {
            return typeof(HelpCommand);
        }

        return Options.Commands.GetOrDefault(commandLineArgs.Command)
               ?? typeof(HelpCommand);
    }
}
