using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events;

public class MeetingCommentUnlikedDomainEvent : DomainEventBase
{
    public MeetingCommentUnlikedDomainEvent(MeetingCommentId meetingCommentId, MemberId likerId)
    {
        MeetingCommentId = meetingCommentId;
        LikerId = likerId;
    }

    public MeetingCommentId MeetingCommentId { get; }

    public MemberId LikerId { get; }
}
