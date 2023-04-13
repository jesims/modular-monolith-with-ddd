public partial class Build
{
    [Parameter("Modular Monolith database connection string")] readonly string DatabaseConnectionString;

    Target CompileDbUpMigrator => _ => _
        .Executes(() =>
        {
            var dbUpMigratorProject = Solution.GetProject("DatabaseMigrator");
            DotNetBuild(s => s
                .SetProjectFile(dbUpMigratorProject)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(OutputDbUbMigratorBuildDirectory)
            );
        });

    Target MigrateDatabase => _ => _
        .Requires(() => DatabaseConnectionString != null)
        .DependsOn(CompileDbUpMigrator)
        .Executes(() =>
        {
            var migrationsPath = DatabaseDirectory / "Migrations";

            DotNet($"{DbUpMigratorPath} {DatabaseConnectionString} {migrationsPath}");
        });
}
