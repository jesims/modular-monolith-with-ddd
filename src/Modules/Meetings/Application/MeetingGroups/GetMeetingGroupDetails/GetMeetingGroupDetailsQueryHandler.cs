using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetMeetingGroupDetails
{
    internal class GetMeetingGroupDetailsQueryHandler : IQueryHandler<GetMeetingGroupDetailsQuery, MeetingGroupDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingGroupDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MeetingGroupDetailsDto> Handle(GetMeetingGroupDetailsQuery query, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            var meetingGroup = await connection.QuerySingleAsync<MeetingGroupDetailsDto>(
                "SELECT " +
                $"meeting_group.id AS {nameof(MeetingGroupDetailsDto.Id)}, " +
                $"meeting_group.name AS {nameof(MeetingGroupDetailsDto.Name)}, " +
                $"meeting_group.description AS {nameof(MeetingGroupDetailsDto.Description)}, " +
                $"meeting_group.location_city AS {nameof(MeetingGroupDetailsDto.LocationCity)}, " +
                $"meeting_group.location_country_code AS {nameof(MeetingGroupDetailsDto.LocationCountryCode)} " +
                "FROM sss_meetings.meeting_groups AS meeting_group " +
                "WHERE meeting_group.id = @MeetingGroupId",
                new
                {
                    query.MeetingGroupId
                });

            meetingGroup.MembersCount = await GetMembersCount(query.MeetingGroupId, connection);

            return meetingGroup;
        }

        private static async Task<int> GetMembersCount(Guid meetingGroupId, IDbConnection connection)
        {
            return await connection.ExecuteScalarAsync<int>(
                "SELECT " +
                "   COUNT(*) " +
                "FROM sss_meetings.meeting_groups AS meeting_group " +
                "   INNER JOIN sss_meetings.meeting_group_members AS meeting_group_member " +
                "       ON meeting_group.id = meeting_group_member.meeting_group_id " +
                "WHERE meeting_group.id = @MeetingGroupId AND " +
                "   meeting_group_member.is_active = true",
                new
                {
                    meetingGroupId
                });
        }
    }
}