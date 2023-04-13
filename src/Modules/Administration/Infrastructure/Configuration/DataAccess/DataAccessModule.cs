using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.DataAccess;

internal class DataAccessModule : Module
{
    private readonly string _databaseConnectionString;
    private readonly ILoggerFactory _loggerFactory;

    internal DataAccessModule(string databaseConnectionString, ILoggerFactory loggerFactory)
    {
        _databaseConnectionString = databaseConnectionString;
        _loggerFactory = loggerFactory;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PgSqlConnectionFactory>()
            .As<ISqlConnectionFactory>()
            .WithParameter("connectionString", _databaseConnectionString)
            .InstancePerLifetimeScope();

        builder
            .Register(c =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<AdministrationContext>();
                dbContextOptionsBuilder.UseNpgsql(_databaseConnectionString);

                dbContextOptionsBuilder
                    .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                return new AdministrationContext(dbContextOptionsBuilder.Options, _loggerFactory);
            })
            .AsSelf()
            .As<DbContext>()
            .InstancePerLifetimeScope();

        var infrastructureAssembly = typeof(AdministrationContext).Assembly;

        builder.RegisterAssemblyTypes(infrastructureAssembly)
            .Where(type => type.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .FindConstructorsWith(new AllConstructorFinder());
    }
}
