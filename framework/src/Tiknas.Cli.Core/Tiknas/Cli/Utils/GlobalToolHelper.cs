using System;
using System.IO;

namespace Tiknas.Cli.Utils;

public class GlobalToolHelper
{
    /// <summary>
    /// Checks whether the tool is installed or not.
    /// </summary>
    /// <param name="toolCommandName">Eg: For TiknasSuite tool it's "tiknas-suite", for TIKNAS CLI tool it's "tiknas"</param>
    public static bool IsGlobalToolInstalled(string toolCommandName)
    {
        string suitePath;

        if (PlatformHelper.GetPlatform() == RuntimePlatform.LinuxOrMacOs)
        {
            suitePath = Environment
                .ExpandEnvironmentVariables(
                    Path.Combine("%HOME%", ".dotnet", "tools", toolCommandName)
                );
        }
        else
        {
            suitePath = Environment
                .ExpandEnvironmentVariables(
                    Path.Combine(@"%USERPROFILE%", ".dotnet", "tools", toolCommandName + ".exe")
                );
        }

        return File.Exists(suitePath);
    }
}
