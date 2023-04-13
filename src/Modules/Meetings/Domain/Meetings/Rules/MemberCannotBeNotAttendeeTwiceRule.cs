using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MemberCannotBeNotAttendeeTwiceRule : IBusinessRule
{
    private readonly List<MeetingNotAttendee> _notAttendees;

    private readonly MemberId _memberId;

    public MemberCannotBeNotAttendeeTwiceRule(List<MeetingNotAttendee> notAttendees, MemberId memberId)
    {
        _notAttendees = notAttendees;
        _memberId = memberId;
    }

    public string Message => "Member cannot be active not attendee twice";

    public bool IsBroken()
    {
        return _notAttendees.SingleOrDefault(x => x.IsActiveNotAttendee(_memberId)) != null;
    }
}
