using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiknas.Cli.ProjectModification;

public class AddModuleInfoOutput
{
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string DocumentationLinks { get; set; }

    public string InstallationCompleteMessage { get; set; }
}
