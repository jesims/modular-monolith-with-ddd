using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfiguration.GetMeetingCommentingConfiguration
{
    internal class GetMeetingCommentingConfigurationQueryHandler : IQueryHandler<GetMeetingCommentingConfigurationQuery, MeetingCommentingConfigurationDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingCommentingConfigurationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MeetingCommentingConfigurationDto> Handle(GetMeetingCommentingConfigurationQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            string sql = "SELECT " +
                         $"meeting_commenting_configuration.meeting_id AS {nameof(MeetingCommentingConfigurationDto.MeetingId)}, " +
                         $"meeting_commenting_configuration.is_commenting_enabled AS {nameof(MeetingCommentingConfigurationDto.IsCommentingEnabled)} " +
                         "FROM sss_meetings.meeting_commenting_configurations AS meeting_commenting_configuration " +
                         "WHERE meeting_commenting_configuration.meeting_id = @MeetingId";

            return await connection.QuerySingleOrDefaultAsync<MeetingCommentingConfigurationDto>(sql, new { query.MeetingId });
        }
    }
}