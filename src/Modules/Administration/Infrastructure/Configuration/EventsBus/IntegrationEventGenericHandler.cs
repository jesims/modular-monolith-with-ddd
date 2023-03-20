using System.Threading.Tasks;
using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Serialization;
using Dapper;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.EventsBus
{
    internal class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IntegrationEvent
    {
        public async Task Handle(T @event)
        {
            using var scope = AdministrationCompositionRoot.BeginLifetimeScope();
            using var connection = scope.Resolve<ISqlConnectionFactory>().GetOpenConnection();

            string type = @event.GetType().FullName;
            var data = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            var sql = "INSERT INTO sss_administration.inbox_messages (id, occurred_on, type, data) " +
                      "VALUES (@Id, @OccurredOn, @Type, @Data)";

            await connection.ExecuteScalarAsync(sql, new
            {
                @event.Id,
                @event.OccurredOn,
                type,
                data
            });
        }
    }
}