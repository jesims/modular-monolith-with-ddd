using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events;

public class MeetingCommentingEnabledDomainEvent : DomainEventBase
{
    public MeetingCommentingEnabledDomainEvent(MeetingId meetingId)
    {
        MeetingId = meetingId;
    }

    public MeetingId MeetingId { get; }
}
