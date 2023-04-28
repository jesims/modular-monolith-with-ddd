using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAuthenticationMemberMeetingGroups
{
    internal class GetAuthenticationMemberMeetingGroupsQueryHandler :
        IQueryHandler<GetAuthenticationMemberMeetingGroupsQuery, List<MemberMeetingGroupDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IExecutionContextAccessor _executionContextAccessor;

        public GetAuthenticationMemberMeetingGroupsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IExecutionContextAccessor executionContextAccessor)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<List<MemberMeetingGroupDto>> Handle(
            GetAuthenticationMemberMeetingGroupsQuery query,
            CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var sql =
                    "SELECT " +
                        $"meeting_group.id AS {nameof(MemberMeetingGroupDto.Id)}, " +
                        $"meeting_group.name AS {nameof(MemberMeetingGroupDto.Name)}, " +
                        $"meeting_group.description AS {nameof(MemberMeetingGroupDto.Description)}, " +
                        $"meeting_group.location_country_code AS {nameof(MemberMeetingGroupDto.LocationCountryCode)}, " +
                        $"meeting_group.location_city AS {nameof(MemberMeetingGroupDto.LocationCity)}, " +
                        $"meeting_group_member.member_id AS {nameof(MemberMeetingGroupDto.MemberId)}, " +
                        $"meeting_group_member.role_code AS {nameof(MemberMeetingGroupDto.RoleCode)} " +
                    "FROM sss_meetings.meeting_groups AS meeting_group " +
                      " INNER JOIN sss_meetings.meeting_group_members AS meeting_group_member " +
                      "     ON meeting_group.id = meeting_group_member.meeting_group_id " +
                      "WHERE meeting_group_member.member_id = @MemberId AND meeting_group_member.is_active = true";

            var meetingGroups = await connection.QueryAsync<MemberMeetingGroupDto>(
                sql,
                new
                {
                    MemberId = _executionContextAccessor.UserId
                });

            return meetingGroups.AsList();
        }
    }
}