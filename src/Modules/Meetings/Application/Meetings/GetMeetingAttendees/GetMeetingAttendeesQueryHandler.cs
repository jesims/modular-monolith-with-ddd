using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingAttendees
{
    internal class GetMeetingAttendeesQueryHandler : IQueryHandler<GetMeetingAttendeesQuery, List<MeetingAttendeeDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingAttendeesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingAttendeeDto>> Handle(GetMeetingAttendeesQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            return (await connection.QueryAsync<MeetingAttendeeDto>(
                "SELECT " +
                $"member.first_name AS {nameof(MeetingAttendeeDto.FirstName)}, " +
                $"member.last_name AS {nameof(MeetingAttendeeDto.LastName)}, " +
                $"meeting_attendee.role_code AS {nameof(MeetingAttendeeDto.RoleCode)}, " +
                $"meeting_attendee.decision_date AS {nameof(MeetingAttendeeDto.DecisionDate)}, " +
                $"meeting_attendee.guests_number AS {nameof(MeetingAttendeeDto.GuestsNumber)}, " +
                $"meeting_attendee.attendee_id AS {nameof(MeetingAttendeeDto.AttendeeId)} " +
                "FROM sss_meetings.meeting_attendees AS meeting_attendee " +
                "   INNER JOIN sss_meetings.members AS member " +
                "       ON member.id = meeting_attendee.attendee_id " +
                "WHERE meeting_attendee.meeting_id = @MeetingId",
                new
                {
                    query.MeetingId
                })).AsList();
        }
    }
}