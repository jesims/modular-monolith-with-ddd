using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using Dapper;
using MediatR;
using Newtonsoft.Json;
using Polly;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand, Unit>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IInternalCommandsMapper _internalCommandsMapper;

        public ProcessInternalCommandsCommandHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IInternalCommandsMapper internalCommandsMapper)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _internalCommandsMapper = internalCommandsMapper;
        }

        public async Task<Unit> Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            string sql = "SELECT " +
                               $"command.id AS {nameof(InternalCommandDto.Id)}, " +
                               $"command.type AS {nameof(InternalCommandDto.Type)}, " +
                               $"command.data AS {nameof(InternalCommandDto.Data)} " +
                               "FROM sss_administration.internal_commands as command " +
                               "WHERE command.processed_date IS NULL " +
                               "ORDER BY command.enqueue_date";

            var commands = await connection.QueryAsync<InternalCommandDto>(sql);

            var internalCommandsList = commands.AsList();
            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                });
            foreach (var internalCommand in internalCommandsList)
            {
                var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommand(
                    internalCommand));

                if (result.Outcome == OutcomeType.Failure)
                {
                    const string updateOnErrorSql = "UPDATE sss_administration.InternalCommands " +
                                                    "SET ProcessedDate = @NowDate, " +
                                                    "Error = @Error " +
                                                    "WHERE Id = @Id";

                    await connection.ExecuteScalarAsync(
                        updateOnErrorSql,
                        new
                        {
                            NowDate = DateTime.UtcNow,
                            Error = result.FinalException.ToString(),
                            internalCommand.Id
                        });
                }
            }

            return Unit.Value;
        }

        private async Task ProcessCommand(
            InternalCommandDto internalCommand)
        {
            var type = _internalCommandsMapper.GetType(internalCommand.Type);
            dynamic commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type);

            await CommandsExecutor.Execute(commandToProcess);
        }

        private class InternalCommandDto
        {
            public Guid Id { get; set; }

            public string Type { get; set; }

            public string Data { get; set; }
        }
    }
}