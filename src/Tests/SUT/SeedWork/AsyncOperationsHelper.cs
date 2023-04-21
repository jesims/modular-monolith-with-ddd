using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace CompanyName.MyMeetings.SUT.SeedWork
{
    internal static class AsyncOperationsHelper
    {
        public static async Task WaitForProcessing(string connectionString, int timeoutInSeconds = 20)
        {
            await using var sqlConnection = new NpgsqlConnection(connectionString);

            var start = Stopwatch.StartNew();
            while (start.Elapsed.Seconds < timeoutInSeconds)
            {
                var internalCommandsCountUsers = await sqlConnection.ExecuteScalarAsync<int>(
                    "SELECT " +
                    "COUNT(*) " +
                    "FROM sss_users.internal_commands AS command " +
                    "WHERE command.processed_date IS NULL");

                var inboxCountUsers = await sqlConnection.ExecuteScalarAsync<int>(
                    "SELECT " +
                    "COUNT(*) " +
                    "FROM sss_users.inbox_messages AS inbox " +
                    "WHERE inbox.processed_date IS NULL");

                var outboxCountUsers = await sqlConnection.ExecuteScalarAsync<int>(
                    "SELECT " +
                    "COUNT(*) " +
                    "FROM sss_users.outbox_messages AS outbox " +
                    "WHERE outbox.processed_date IS NULL");
                
                var internalCommandsCountMeetings = await sqlConnection.ExecuteScalarAsync<int>(
                    "SELECT " +
                    "COUNT(*) " +
                    "FROM sss_meetings.internal_commands AS command " +
                    "WHERE command.processed_date IS NULL");

                var inboxCountMeetings = await sqlConnection.ExecuteScalarAsync<int>(
                    "SELECT " +
                    "COUNT(*) " +
                    "FROM sss_meetings.inbox_messages AS inbox " +
                    "WHERE inbox.processed_date IS NULL");

                var outboxCountMeetings = await sqlConnection.ExecuteScalarAsync<int>(
                    "SELECT " +
                    "COUNT(*) " +
                    "FROM sss_meetings.outbox_messages AS outbox " +
                    "WHERE outbox.processed_date IS NULL");

                var internalCommandsCountAdministration = await sqlConnection.ExecuteScalarAsync<int>(
                    "SELECT " +
                    "COUNT(*) " +
                    "FROM sss_administration.internal_commands AS command " +
                    "WHERE command.processed_date IS NULL");

                var inboxCountMeetingsAdministration = await sqlConnection.ExecuteScalarAsync<int>(
                    "SELECT " +
                    "COUNT(*) " +
                    "FROM sss_administration.inbox_messages AS inbox " +
                    "WHERE inbox.processed_date IS NULL");

                var outboxCountMeetingsAdministration = await sqlConnection.ExecuteScalarAsync<int>(
                    "SELECT " +
                    "COUNT(*) " +
                    "FROM sss_administration.outbox_messages AS outbox " +
                    "WHERE outbox.processed_date IS NULL");

                if (internalCommandsCountUsers == 0 && 
                    inboxCountUsers == 0 && 
                    outboxCountUsers == 0 &&
                    internalCommandsCountMeetings == 0 && 
                    inboxCountMeetings == 0 && 
                    outboxCountMeetings == 0 && 
                    internalCommandsCountAdministration == 0 && 
                    inboxCountMeetingsAdministration == 0 && 
                    outboxCountMeetingsAdministration == 0
                   )
                {
                    return;
                }

                Thread.Sleep(100);
            }

            throw new Exception("Timeout for processing elapsed.");
        }
    }
}