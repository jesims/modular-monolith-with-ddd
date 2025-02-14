﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Outbox;
using Dapper;
using MediatR;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Administration.IntegrationTests.SeedWork
{
    public class OutboxMessagesHelper
    {
        public static async Task<List<OutboxMessageDto>> GetOutboxMessages(IDbConnection connection)
        {
            const string sql = "SELECT " +
                               $"outbox_message.id AS {nameof(OutboxMessageDto.Id)}, " +
                               $"outbox_message.type AS {nameof(OutboxMessageDto.Type)}, " +
                               $"outbox_message.data AS {nameof(OutboxMessageDto.Data)} " +
                               "FROM sss_administration.outbox_messages AS outbox_message " +
                               "ORDER BY outbox_message.occurred_on";

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
            return messages.AsList();
        }

        public static T Deserialize<T>(OutboxMessageDto message)
            where T : class, INotification
        {
            Type type = Assembly.GetAssembly(typeof(MeetingGroupProposalAcceptedNotification)).GetType(typeof(T).FullName);
            return JsonConvert.DeserializeObject(message.Data, type) as T;
        }
    }
}