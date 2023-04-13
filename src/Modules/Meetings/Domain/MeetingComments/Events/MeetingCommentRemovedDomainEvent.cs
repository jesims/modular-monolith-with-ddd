using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events;

public class MeetingCommentRemovedDomainEvent : DomainEventBase
{
    public MeetingCommentRemovedDomainEvent(MeetingCommentId meetingCommentId)
    {
        MeetingCommentId = meetingCommentId;
    }

    public MeetingCommentId MeetingCommentId { get; }
}
