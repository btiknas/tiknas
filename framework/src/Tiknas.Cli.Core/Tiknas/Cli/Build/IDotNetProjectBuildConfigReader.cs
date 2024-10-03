namespace Tiknas.Cli.Build;

public interface IDotNetProjectBuildConfigReader
{
    DotNetProjectBuildConfig Read(string directoryPath);
}
