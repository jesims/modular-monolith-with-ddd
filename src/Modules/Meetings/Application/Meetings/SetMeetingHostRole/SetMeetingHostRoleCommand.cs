using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingHostRole;

public class SetMeetingHostRoleCommand : CommandBase
{
    public SetMeetingHostRoleCommand(Guid memberId, Guid meetingId)
    {
        MemberId = memberId;
        MeetingId = meetingId;
    }

    public Guid MemberId { get; }

    public Guid MeetingId { get; }
}
