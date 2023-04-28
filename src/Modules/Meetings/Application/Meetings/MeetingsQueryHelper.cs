using System.Data;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings
{
    public class MeetingsQueryHelper
    {
        public static async Task<MeetingDto> GetMeeting(MeetingId meetingId, IDbConnection connection)
        {
            return await connection.QuerySingleAsync<MeetingDto>(
                "SELECT "
                + $"meeting.id AS {nameof(MeetingDto.Id)}, "
                + $"meeting.title AS {nameof(MeetingDto.Title)}, "
                + $"meeting.description AS {nameof(MeetingDto.Description)}, "
                + $"meeting.location_address AS {nameof(MeetingDto.LocationAddress)}, "
                + $"meeting.location_city AS {nameof(MeetingDto.LocationCity)}, "
                + $"meeting.location_postal_code AS {nameof(MeetingDto.LocationPostalCode)}, "
                + $"meeting.term_start_date AS {nameof(MeetingDto.TermStartDate)}, "
                + $"meeting.term_end_date AS {nameof(MeetingDto.TermEndDate)} "
                + "FROM sss_meetings.meetings AS meeting "
                + "WHERE meeting.id = @Id", new
                {
                    Id = meetingId.Value
                });
        }
    }
}