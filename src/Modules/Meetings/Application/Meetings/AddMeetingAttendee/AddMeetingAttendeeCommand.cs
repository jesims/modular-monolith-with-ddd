using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.AddMeetingAttendee;

public class AddMeetingAttendeeCommand : CommandBase
{
    public AddMeetingAttendeeCommand(Guid meetingId, int guestsNumber)
    {
        MeetingId = meetingId;
        GuestsNumber = guestsNumber;
    }

    public Guid MeetingId { get; }

    public int GuestsNumber { get; }
}
