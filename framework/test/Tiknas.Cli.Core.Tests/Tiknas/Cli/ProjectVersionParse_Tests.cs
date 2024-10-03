using Shouldly;
using Tiknas.Cli.ProjectModification;
using Xunit;

namespace Tiknas.Cli;

public class ProjectVersionParse_Tests
{
    [Fact]
    public void Find_Tiknas_Version()
    {
        const string csprojContent = "<Project Sdk=\"Microsoft.NET.Sdk\">" +
                                     "<Import Project=\"..\\..\\common.props\" />" +
                                     "<PropertyGroup>" +
                                     "<TargetFramework>net5.0</TargetFramework>" +
                                     "<RootNamespace>Blazoor.EfCore07062034</RootNamespace>" +
                                     "</PropertyGroup>" +
                                     "<ItemGroup>" +
                                     "<ProjectReference Include=\"..\\Blazoor.EfCore07062034.Domain.Shared\\Blazoor.EfCore07062034.Domain.Shared.csproj\" />" +
                                     "</ItemGroup>" +
                                     "<ItemGroup>" +
                                     "<PackageReference    Include=\"Tiknas.Emailing\"   Version=\"4.4.0-rc.1\"  />" +
                                     "<PackageReference  Include=\"Tiknas.PermissionManagement.Domain.Identity\"   Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"Tiknas.IdentityServer.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"Tiknas.PermissionManagement.Domain.IdentityServer\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"Tiknas.BackgroundJobs.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"Tiknas.AuditLogging.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"Tiknas.FeatureManagement.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"Tiknas.SettingManagement.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"Tiknas.BlobStoring.Database.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"Tiknas.Identity.Pro.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"Tiknas.LanguageManagement.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"Tiknas.LeptonTheme.Management.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"Tiknas.Saas.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "<PackageReference Include=\"Tiknas.TextTemplateManagement.Domain\" Version=\"4.4.0-rc.1\" />" +
                                     "</ItemGroup>" +
                                     "</Project>";

        var success = SolutionPackageVersionFinder.TryParseVersionFromCsprojFile(csprojContent, out var version);
        success.ShouldBe(true);
        version.ShouldBe("4.4.0-rc.1");
    }

    [Fact]
    public void Find_Tiknas_Semantic_Version()
    {
        const string csprojContent = "<Project Sdk=\"Microsoft.NET.Sdk\">" +
                                     "<Import Project=\"..\\..\\common.props\" />" +
                                     "<PropertyGroup>" +
                                     "<TargetFramework>net5.0</TargetFramework>" +
                                     "<RootNamespace>Blazoor.EfCore07062034</RootNamespace>" +
                                     "</PropertyGroup>" +
                                     "<ItemGroup>" +
                                     "<ProjectReference Include=\"..\\Blazoor.EfCore07062034.Domain.Shared\\Blazoor.EfCore07062034.Domain.Shared.csproj\" />" +
                                     "</ItemGroup>" +
                                     "<ItemGroup>" +
                                     "<PackageReference Include=\"Tiknas.Emailing\" Version=\"12.8.3-beta.1\"  />" +
                                     "</ItemGroup>" +
                                     "</Project>";

        var success = SolutionPackageVersionFinder.TryParseSemanticVersionFromCsprojFile(csprojContent, out var version);
        success.ShouldBe(true);
        version.Major.ShouldBe(12);
        version.Minor.ShouldBe(8);
        version.Patch.ShouldBe(3);
        version.Release.ShouldBe("beta.1");
    }

}
