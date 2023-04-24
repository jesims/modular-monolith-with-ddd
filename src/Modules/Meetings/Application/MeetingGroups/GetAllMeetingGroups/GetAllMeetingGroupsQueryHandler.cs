using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetMeetingGroupDetails;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups
{
    internal class GetAllMeetingGroupsQueryHandler : IQueryHandler<GetAllMeetingGroupsQuery, List<MeetingGroupDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetAllMeetingGroupsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingGroupDto>> Handle(GetAllMeetingGroupsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                                $"meeting_group.id AS {nameof(MeetingGroupDetailsDto.Id)}, " +
                                $"meeting_group.name AS {nameof(MeetingGroupDetailsDto.Name)}, " +
                                $"meeting_group.description AS {nameof(MeetingGroupDetailsDto.Description)}, " +
                                $"meeting_group.location_city AS {nameof(MeetingGroupDetailsDto.LocationCity)}, " +
                                $"meeting_group.location_country_code AS {nameof(MeetingGroupDetailsDto.LocationCountryCode)} " +
                                "FROM sss_meetings.meeting_groups AS meeting_group";
                
            var meetingGroups = await connection.QueryAsync<MeetingGroupDto>(sql);

            return meetingGroups.AsList();
        }
    }
}