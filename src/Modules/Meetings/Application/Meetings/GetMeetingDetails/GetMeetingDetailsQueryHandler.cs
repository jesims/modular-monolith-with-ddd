using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingDetails
{
    internal class GetMeetingDetailsQueryHandler : IQueryHandler<GetMeetingDetailsQuery, MeetingDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MeetingDetailsDto> Handle(GetMeetingDetailsQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            return await connection.QuerySingleAsync<MeetingDetailsDto>(
                "SELECT " +
                $"meeting.id AS {nameof(MeetingDetailsDto.Id)}, " +
                $"meeting.meeting_group_id AS {nameof(MeetingDetailsDto.MeetingGroupId)}, " +
                $"meeting.title AS {nameof(MeetingDetailsDto.Title)}, " +
                $"meeting.term_start_date AS {nameof(MeetingDetailsDto.TermStartDate)}, " +
                $"meeting.term_end_date AS {nameof(MeetingDetailsDto.TermEndDate)}, " +
                $"meeting.description AS {nameof(MeetingDetailsDto.Description)}, " +
                $"meeting.location_name AS {nameof(MeetingDetailsDto.LocationName)}, " +
                $"meeting.location_address AS {nameof(MeetingDetailsDto.LocationAddress)}, " +
                $"meeting.location_postal_code AS {nameof(MeetingDetailsDto.LocationPostalCode)}, " +
                $"meeting.location_city AS {nameof(MeetingDetailsDto.LocationCity)}, " +
                $"meeting.attendees_limit AS {nameof(MeetingDetailsDto.AttendeesLimit)}, " +
                $"meeting.guests_limit AS {nameof(MeetingDetailsDto.GuestsLimit)}, " +
                $"meeting.rsvpterm_start_date AS {nameof(MeetingDetailsDto.RSVPTermStartDate)}, " +
                $"meeting.rsvpterm_end_date AS {nameof(MeetingDetailsDto.RSVPTermEndDate)}, " +
                $"meeting.event_fee_value AS {nameof(MeetingDetailsDto.EventFeeValue)}, " +
                $"meeting.event_fee_currency AS {nameof(MeetingDetailsDto.EventFeeCurrency)} " +
                "FROM sss_meetings.meetings AS meeting " +
                "WHERE meeting.id = @MeetingId",
                new
                {
                    query.MeetingId
                });
        }
    }
}