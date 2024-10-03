using System.IO;
using System.Threading.Tasks;
using Shouldly;
using Tiknas.Cli.Args;
using Tiknas.Cli.Commands;
using Tiknas.Cli.ProjectModification;
using Xunit;

namespace Tiknas.Cli;

public class ProjectNameValidation_Tests : TiknasCliTestBase
{
    private readonly NewCommand _newCommand;

    public ProjectNameValidation_Tests()
    {
        _newCommand = GetRequiredService<NewCommand>();
    }

    [Fact]
    public async Task Illegal_ProjectName_Test()
    {
        var illegalProjectNames = new[]
        {
                "MyCompanyName.MyProjectName",
                "MyProjectName",
                "CON", //Windows doesn't accept these names as file name
                "AUX",
                "PRN",
                "COM1",
                "LPT2"
            };

        foreach (var illegalProjectName in illegalProjectNames)
        {
            var args = new CommandLineArgs("new", illegalProjectName);
            await _newCommand.ExecuteAsync(args).ShouldThrowAsync<CliUsageException>();
        }
    }

    [Fact]
    public async Task Contains_Illegal_Keyword_Test()
    {
        var illegalKeywords = new[]
        {
                "Blazor"
            };

        foreach (var illegalKeyword in illegalKeywords)
        {
            var args = new CommandLineArgs("new", illegalKeyword);
            await _newCommand.ExecuteAsync(args).ShouldThrowAsync<CliUsageException>();

            args = new CommandLineArgs("new", "Acme." + illegalKeyword);
            await _newCommand.ExecuteAsync(args).ShouldThrowAsync<CliUsageException>();
        }
    }

}
