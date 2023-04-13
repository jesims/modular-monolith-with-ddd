using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

public class MeetingGroupMember : Entity
{
    private readonly MeetingGroupMemberRole _role;

    private bool _isActive;

    private DateTime? _leaveDate;

    private MeetingGroupMember()
    {
        // Only for EF.
    }

    private MeetingGroupMember(
        MeetingGroupId meetingGroupId,
        MemberId memberId,
        MeetingGroupMemberRole role)
    {
        MeetingGroupId = meetingGroupId;
        MemberId = memberId;
        _role = role;
        JoinedDate = SystemClock.Now;
        _isActive = true;

        AddDomainEvent(new NewMeetingGroupMemberJoinedDomainEvent(MeetingGroupId, MemberId, _role));
    }

    internal MeetingGroupId MeetingGroupId { get; }

    internal MemberId MemberId { get; }

    internal DateTime JoinedDate { get; private set; }

    internal static MeetingGroupMember CreateNew(
        MeetingGroupId meetingGroupId,
        MemberId memberId,
        MeetingGroupMemberRole role)
    {
        return new MeetingGroupMember(meetingGroupId, memberId, role);
    }

    internal void Leave()
    {
        _isActive = false;
        _leaveDate = SystemClock.Now;

        AddDomainEvent(new MeetingGroupMemberLeftGroupDomainEvent(MeetingGroupId, MemberId));
    }

    internal bool IsMember(MemberId memberId)
    {
        return _isActive && MemberId == memberId;
    }

    internal bool IsOrganizer(MemberId memberId)
    {
        return IsMember(memberId) && _role == MeetingGroupMemberRole.Organizer;
    }
}
