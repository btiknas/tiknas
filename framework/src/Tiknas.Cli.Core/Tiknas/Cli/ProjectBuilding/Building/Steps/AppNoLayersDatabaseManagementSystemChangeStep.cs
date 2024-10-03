using System;
using System.Linq;
using Tiknas.Cli.ProjectBuilding.Files;

namespace Tiknas.Cli.ProjectBuilding.Building.Steps;

public class AppNoLayersDatabaseManagementSystemChangeStep : ProjectBuildPipelineStep
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
                ChangeUseSqlServer(context, "UseOracle");
                break;

            case DatabaseManagementSystem.OracleDevart:
                ChangeEntityFrameworkCoreDependency(context, "Tiknas.EntityFrameworkCore.Oracle.Devart",
                    "Tiknas.EntityFrameworkCore.Oracle.Devart",
                    "TiknasEntityFrameworkCoreOracleDevartModule");
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

    private void AddMySqlServerVersion(ProjectBuildContext context)
    {
        var dbContextFactoryFile = context.Files.FirstOrDefault(f => f.Name.EndsWith("DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));

        dbContextFactoryFile?.ReplaceText("configuration.GetConnectionString(\"Default\")",
            "configuration.GetConnectionString(\"Default\"), MySqlServerVersion.LatestSupportedServerVersion");
    }

    private void ChangeEntityFrameworkCoreDependency(ProjectBuildContext context, string newPackageName, string newModuleNamespace, string newModuleClass)
    {
        var projectFile = context.Files.FirstOrDefault(f => f.Name.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase));
        projectFile?.ReplaceText("Tiknas.EntityFrameworkCore.SqlServer", newPackageName);

        var moduleClass = context.Files.FirstOrDefault(f => f.Name.EndsWith("Module.cs", StringComparison.OrdinalIgnoreCase));
        moduleClass?.ReplaceText("Tiknas.EntityFrameworkCore.SqlServer", newModuleNamespace);
        moduleClass?.ReplaceText("TiknasEntityFrameworkCoreSqlServerModule", newModuleClass);
    }

    private void ChangeUseSqlServer(ProjectBuildContext context, string newUseMethodForEfModule, string newUseMethodForDbContext = null)
    {
        if (newUseMethodForDbContext == null)
        {
            newUseMethodForDbContext = newUseMethodForEfModule;
        }

        var oldUseMethod = "UseSqlServer";

        var moduleClass = context.Files.FirstOrDefault(f => f.Name.EndsWith("Module.cs", StringComparison.OrdinalIgnoreCase));
        moduleClass?.ReplaceText(oldUseMethod, newUseMethodForEfModule);

        var dbContextFactoryFile = context.Files.FirstOrDefault(f => f.Name.EndsWith("DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));
        dbContextFactoryFile?.ReplaceText(oldUseMethod, newUseMethodForDbContext);
    }
}
