using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SignUpMemberToWaitlist;

public class SignUpMemberToWaitlistCommand : CommandBase
{
    public SignUpMemberToWaitlistCommand(Guid meetingId)
    {
        MeetingId = meetingId;
    }

    public Guid MeetingId { get; }
}
