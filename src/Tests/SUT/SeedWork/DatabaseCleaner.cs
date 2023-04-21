using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace CompanyName.MyMeetings.SUT.SeedWork
{
    internal static class DatabaseCleaner
    {
        internal static async Task ClearAllData(IDbConnection connection)
        {
            await ClearAdministration(connection);

            await ClearApp(connection);

            await ClearMeetings(connection);

            //await ClearPayments(connection);

            await ClearUsers(connection);
        }

        private static async Task ClearUsers(IDbConnection connection)
        {
            var clearUsersSql = 
                "DELETE FROM sss_users.inbox_messages; " +
                "DELETE FROM sss_users.internal_commands; " +
                "DELETE FROM sss_users.outbox_messages; " +
                "DELETE FROM sss_users.user_registrations; " +
                "DELETE FROM sss_users.users; " +
                "DELETE FROM sss_users.roles_to_permissions; " +
                "DELETE FROM sss_users.user_roles; " +
                "DELETE FROM sss_users.permissions; ";

            await connection.ExecuteScalarAsync(clearUsersSql);
        }

        private static async Task ClearPayments(IDbConnection connection)
        {
            var clearPaymentsSql =
                "DELETE FROM [payments].[InboxMessages] " +
                "DELETE FROM [payments].[InternalCommands] " +
                "DELETE FROM [payments].[MeetingFees] " +
                "DELETE FROM [payments].[Messages] " +
                "DELETE FROM [payments].[OutboxMessages] " +
                "DELETE FROM [payments].[Payers] " +
                "DELETE FROM [payments].[PriceListItems] " +
                "DELETE FROM [payments].[Streams] " +
                "DELETE FROM [payments].[SubscriptionCheckpoints] " +
                "DELETE FROM [payments].[SubscriptionDetails] " +
                "DELETE FROM [payments].[SubscriptionPayments] ";

            await connection.ExecuteScalarAsync(clearPaymentsSql);
        }

        private static async Task ClearMeetings(IDbConnection connection)
        {
            var clearMeetingsSql =
                "DELETE FROM sss_meetings.inbox_messages; " +
                "DELETE FROM sss_meetings.internal_commands; " +
                "DELETE FROM sss_meetings.outbox_messages; " +
                "DELETE FROM sss_meetings.meeting_attendees; " +
                "DELETE FROM sss_meetings.meeting_group_members; " +
                "DELETE FROM sss_meetings.meeting_group_proposals; " +
                "DELETE FROM sss_meetings.meeting_groups; " +
                "DELETE FROM sss_meetings.meeting_not_attendees; " +
                "DELETE FROM sss_meetings.meeting_commenting_configurations; " +
                "DELETE FROM sss_meetings.meetings; " +
                "DELETE FROM sss_meetings.meeting_waitlist_members; " +
                "DELETE FROM sss_meetings.meeting_member_comment_likes; " +
                "DELETE FROM sss_meetings.meeting_comments; " +
                "DELETE FROM sss_meetings.countries; " +
                "DELETE FROM sss_meetings.members; ";

            await connection.ExecuteScalarAsync(clearMeetingsSql);
        }

        private static async Task ClearApp(IDbConnection connection)
        {
            var clearAppSql =
                "DELETE FROM [app].[Emails] ";
            await connection.ExecuteScalarAsync(clearAppSql);
        }

        private static async Task ClearAdministration(IDbConnection connection)
        {
            var clearAdministrationSql =
                "DELETE FROM sss_administration.inbox_messages; " +
                "DELETE FROM sss_administration.internal_commands; " +
                "DELETE FROM sss_administration.outbox_messages; " +
                "DELETE FROM sss_administration.meeting_group_proposals; " +
                "DELETE FROM sss_administration.members; ";

            await connection.ExecuteScalarAsync(clearAdministrationSql);
        }
    }
}