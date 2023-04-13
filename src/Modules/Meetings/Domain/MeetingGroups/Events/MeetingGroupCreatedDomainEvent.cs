using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;

public class MeetingGroupCreatedDomainEvent : DomainEventBase
{
    public MeetingGroupCreatedDomainEvent(MeetingGroupId meetingGroupId, MemberId creatorId)
    {
        MeetingGroupId = meetingGroupId;
        CreatorId = creatorId;
    }

    public MeetingGroupId MeetingGroupId { get; }

    public MemberId CreatorId { get; }
}
