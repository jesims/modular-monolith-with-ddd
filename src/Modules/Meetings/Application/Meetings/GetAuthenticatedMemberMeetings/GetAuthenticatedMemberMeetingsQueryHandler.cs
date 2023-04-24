using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetAuthenticatedMemberMeetings
{
    internal class GetAuthenticatedMemberMeetingsQueryHandler : IQueryHandler<GetAuthenticatedMemberMeetingsQuery, List<MemberMeetingDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IExecutionContextAccessor _executionContextAccessor;

        public GetAuthenticatedMemberMeetingsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IExecutionContextAccessor executionContextAccessor)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<List<MemberMeetingDto>> Handle(GetAuthenticatedMemberMeetingsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            return (await connection.QueryAsync<MemberMeetingDto>(
                "SELECT " +
                $"meeting.id AS {nameof(MemberMeetingDto.MeetingId)}, " +
                $"meeting.location_city AS {nameof(MemberMeetingDto.LocationCity)}, " +
                $"meeting.location_address AS {nameof(MemberMeetingDto.LocationAddress)}, " +
                $"meeting.location_postal_code AS {nameof(MemberMeetingDto.LocationPostalCode)}, " +
                $"meeting.term_start_date AS {nameof(MemberMeetingDto.TermStartDate)}, " +
                $"meeting.term_end_date AS {nameof(MemberMeetingDto.TermEndDate)}, " +
                $"meeting.title AS {nameof(MemberMeetingDto.Title)}, " +
                $"meeting_attendee.role_code AS {nameof(MemberMeetingDto.RoleCode)} " +
                "FROM sss_meetings.meetings AS meeting " +
                "   INNER JOIN sss_meetings.meeting_attendees AS meeting_attendee " +
                "       ON meeting.id = meeting_attendee.meeting_id " +
                "WHERE meeting_attendee.attendee_id = @AttendeeId AND meeting_attendee.is_removed = false",
                new
                {
                    AttendeeId = _executionContextAccessor.UserId
                })).AsList();
        }
    }
}