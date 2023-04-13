using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MemberCannotHaveSetAttendeeRoleMoreThanOnceRule : IBusinessRule
{
    private readonly MeetingAttendeeRole _meetingAttendeeRole;

    internal MemberCannotHaveSetAttendeeRoleMoreThanOnceRule(MeetingAttendeeRole meetingAttendeeRole)
    {
        _meetingAttendeeRole = meetingAttendeeRole;
    }

    public string Message => "Member cannot be attendee of meeting more than once";

    public bool IsBroken()
    {
        return _meetingAttendeeRole == MeetingAttendeeRole.Attendee;
    }
}
