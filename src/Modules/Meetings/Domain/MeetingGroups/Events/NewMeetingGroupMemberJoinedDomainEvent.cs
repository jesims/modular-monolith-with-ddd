using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;

public class NewMeetingGroupMemberJoinedDomainEvent : DomainEventBase
{
    public NewMeetingGroupMemberJoinedDomainEvent(MeetingGroupId meetingGroupId, MemberId memberId,
        MeetingGroupMemberRole role)
    {
        MeetingGroupId = meetingGroupId;
        MemberId = memberId;
        Role = role;
    }

    public MeetingGroupId MeetingGroupId { get; }

    public MemberId MemberId { get; }

    public MeetingGroupMemberRole Role { get; }
}
