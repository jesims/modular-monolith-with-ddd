﻿using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Serialization;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using Dapper;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing.InternalCommands;

public class CommandsScheduler : ICommandsScheduler
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public CommandsScheduler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task EnqueueAsync(ICommand command)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sqlInsert = "INSERT INTO sss_meetings.internal_commands (id, enqueue_date , type, data) VALUES " +
                                 "(@Id, @EnqueueDate, @Type, @Data)";

        await connection.ExecuteAsync(sqlInsert, new
        {
            command.Id,
            EnqueueDate = DateTime.UtcNow,
            Type = command.GetType().FullName,
            Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
            {
                ContractResolver = new AllPropertiesContractResolver()
            })
        });
    }
}
