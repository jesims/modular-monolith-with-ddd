using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Dapper;
using MediatR;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.Inbox;

internal class ProcessInboxCommandHandler : ICommandHandler<ProcessInboxCommand>
{
    private readonly IMediator _mediator;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public ProcessInboxCommandHandler(IMediator mediator, ISqlConnectionFactory sqlConnectionFactory)
    {
        _mediator = mediator;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Unit> Handle(ProcessInboxCommand command, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();
        var sql = "SELECT "
                  + $"inbox_message.id AS {nameof(InboxMessageDto.Id)}, "
                  + $"inbox_message.type AS {nameof(InboxMessageDto.Type)}, "
                  + $"inbox_message.data AS {nameof(InboxMessageDto.Data)} "
                  + "FROM sss_users.inbox_messages AS inbox_message "
                  + "WHERE inbox_message.processed_date IS NULL "
                  + "ORDER BY inbox_message.occurred_on";

        var messages = await connection.QueryAsync<InboxMessageDto>(sql);

        const string sqlUpdateProcessedDate = "UPDATE sss_users.inbox_messages " +
                                              "SET processed_date = @Date " +
                                              "WHERE id = @Id";

        foreach (var message in messages)
        {
            var messageAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(assembly => message.Type.Contains(assembly.GetName().Name));

            var type = messageAssembly.GetType(message.Type);
            var request = JsonConvert.DeserializeObject(message.Data, type);

            try
            {
                await _mediator.Publish((INotification)request, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await connection.ExecuteAsync(sqlUpdateProcessedDate, new
            {
                Date = DateTime.UtcNow,
                message.Id
            });
        }

        return Unit.Value;
    }
}
