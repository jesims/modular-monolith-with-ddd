using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules;

public class NotActualGroupMemberCannotLeaveGroupRule : IBusinessRule
{
    private readonly List<MeetingGroupMember> _members;

    private readonly MemberId memberId;

    public NotActualGroupMemberCannotLeaveGroupRule(List<MeetingGroupMember> members, MemberId memberId)
    {
        _members = members;
        this.memberId = memberId;
    }

    public string Message => "Member is not member of this group so he cannot leave it";

    public bool IsBroken()
    {
        return _members.SingleOrDefault(x => x.IsMember(memberId)) == null;
    }
}
