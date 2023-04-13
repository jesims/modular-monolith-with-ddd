using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

public class MeetingAttendeeRole : ValueObject
{
    private MeetingAttendeeRole(string value)
    {
        Value = value;
    }

    public static MeetingAttendeeRole Host => new("Host");

    public static MeetingAttendeeRole Attendee => new("Attendee");

    public string Value { get; }
}
