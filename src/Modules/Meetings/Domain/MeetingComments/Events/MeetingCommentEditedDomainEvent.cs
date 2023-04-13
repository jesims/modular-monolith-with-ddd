using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events;

public class MeetingCommentEditedDomainEvent : DomainEventBase
{
    public MeetingCommentEditedDomainEvent(MeetingCommentId meetingCommentId, string editedComment)
    {
        MeetingCommentId = meetingCommentId;
        EditedComment = editedComment;
    }

    public MeetingCommentId MeetingCommentId { get; }

    public string EditedComment { get; }
}
