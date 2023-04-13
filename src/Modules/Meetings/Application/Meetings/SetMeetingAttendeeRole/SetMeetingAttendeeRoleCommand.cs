using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingAttendeeRole;

public class SetMeetingAttendeeRoleCommand : CommandBase
{
    public SetMeetingAttendeeRoleCommand(Guid memberId, Guid meetingId)
    {
        MemberId = memberId;
        MeetingId = meetingId;
    }

    public Guid MemberId { get; }

    public Guid MeetingId { get; }
}
