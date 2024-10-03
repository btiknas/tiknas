using System;
using System.Linq;
using Tiknas.Cli.ProjectBuilding.Files;
using Tiknas.Cli.ProjectBuilding.Templates.App;

namespace Tiknas.Cli.ProjectBuilding.Building.Steps;

public class AppModuleDatabaseManagementSystemChangeStep : ProjectBuildPipelineStep
{
    public override void Execute(ProjectBuildContext context)
    {
        switch (context.BuildArgs.DatabaseManagementSystem)
        {
            case DatabaseManagementSystem.MySQL:
                ChangeEntityFrameworkCoreDependency(context, "Tiknas.EntityFrameworkCore.MySQL",
                    "Tiknas.EntityFrameworkCore.MySQL",
                    "TiknasEntityFrameworkCoreMySQLModule");
                AddMySqlServerVersion(context);
                ChangeUseSqlServer(context, "UseMySQL", "UseMySql");
                break;

            case DatabaseManagementSystem.PostgreSQL:
                ChangeEntityFrameworkCoreDependency(context, "Tiknas.EntityFrameworkCore.PostgreSql",
                    "Tiknas.EntityFrameworkCore.PostgreSql",
                    "TiknasEntityFrameworkCorePostgreSqlModule");
                ChangeUseSqlServer(context, "UseNpgsql");
                break;

            case DatabaseManagementSystem.Oracle:
                ChangeEntityFrameworkCoreDependency(context, "Tiknas.EntityFrameworkCore.Oracle",
                    "Tiknas.EntityFrameworkCore.Oracle",
                    "TiknasEntityFrameworkCoreOracleModule");
                AdjustOracleDbContextOptionsBuilder(context);
                ChangeUseSqlServer(context, "UseOracle");
                break;

            case DatabaseManagementSystem.OracleDevart:
                ChangeEntityFrameworkCoreDependency(context, "Tiknas.EntityFrameworkCore.Oracle.Devart",
                    "Tiknas.EntityFrameworkCore.Oracle.Devart",
                    "TiknasEntityFrameworkCoreOracleDevartModule");
                AdjustOracleDbContextOptionsBuilder(context);
                ChangeUseSqlServer(context, "UseOracle");
                break;

            case DatabaseManagementSystem.SQLite:
                ChangeEntityFrameworkCoreDependency(context, "Tiknas.EntityFrameworkCore.Sqlite",
                    "Tiknas.EntityFrameworkCore.Sqlite",
                    "TiknasEntityFrameworkCoreSqliteModule");
                ChangeUseSqlServer(context, "UseSqlite");
                break;

            default:
                return;
        }
    }

    private void AdjustOracleDbContextOptionsBuilder(ProjectBuildContext context)
    {
        var dbContextFactoryFiles = context.Files.Where(f => f.Name.EndsWith("DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));
        foreach (var dbContextFactoryFile in dbContextFactoryFiles)
        {
            dbContextFactoryFile?.ReplaceText("new DbContextOptionsBuilder",
                $"(DbContextOptionsBuilder<{context.BuildArgs.SolutionName.ProjectName}{(false ? "Migrations" : string.Empty)}DbContext>) new DbContextOptionsBuilder");
        }
    }

    private void AddMySqlServerVersion(ProjectBuildContext context)
    {
        var dbContextFactoryFiles = context.Files.Where(f => f.Name.EndsWith("DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));
        foreach (var dbContextFactoryFile in dbContextFactoryFiles)
        {
            dbContextFactoryFile?.ReplaceText("configuration.GetConnectionString(\"Default\")", "configuration.GetConnectionString(\"Default\"), MySqlServerVersion.LatestSupportedServerVersion");
        }
    }

    private void ChangeEntityFrameworkCoreDependency(ProjectBuildContext context, string newPackageName, string newModuleNamespace, string newModuleClass)
    {
        var efCoreProjectFiles = context.Files.Where(f => f.Name.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase));
        foreach (var efCoreProjectFile in efCoreProjectFiles)
        {
            efCoreProjectFile?.ReplaceText("Tiknas.EntityFrameworkCore.SqlServer", newPackageName);
        }

        var efCoreModuleClasses = context.Files.Where(f => f.Name.EndsWith("Module.cs", StringComparison.OrdinalIgnoreCase));
        foreach (var efCoreModuleClass in efCoreModuleClasses)
        {
            efCoreModuleClass?.ReplaceText("Tiknas.EntityFrameworkCore.SqlServer", newModuleNamespace);
            efCoreModuleClass?.ReplaceText("TiknasEntityFrameworkCoreSqlServerModule", newModuleClass);
        }
    }

    private void ChangeUseSqlServer(ProjectBuildContext context, string newUseMethodForEfModule, string newUseMethodForDbContext = null)
    {
        if (newUseMethodForDbContext == null)
        {
            newUseMethodForDbContext = newUseMethodForEfModule;
        }

        const string oldUseMethod = "UseSqlServer";

        var efCoreModuleClasses = context.Files.Where(f => f.Name.EndsWith("Module.cs", StringComparison.OrdinalIgnoreCase));
        foreach (var efCoreModuleClass in efCoreModuleClasses)
        {
            efCoreModuleClass.ReplaceText(oldUseMethod, newUseMethodForEfModule);
        }

        var dbContextFactoryFiles = context.Files.Where(f => f.Name.EndsWith("DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));
        foreach (var dbContextFactoryFile in dbContextFactoryFiles)
        {
            dbContextFactoryFile?.ReplaceText(oldUseMethod, newUseMethodForDbContext);
        }
    }
}
