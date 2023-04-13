using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;

public class MeetingMemberCommentLike : Entity, IAggregateRoot
{
    private readonly MeetingCommentId _meetingCommentId;

    private readonly MemberId _memberId;

    private MeetingMemberCommentLike()
    {
        // Only for EF.
    }

    private MeetingMemberCommentLike(MeetingCommentId meetingCommentId, MemberId memberId)
    {
        Id = new MeetingMemberCommentLikeId(Guid.NewGuid());
        _meetingCommentId = meetingCommentId;
        _memberId = memberId;

        AddDomainEvent(new MeetingCommentLikedDomainEvent(meetingCommentId, memberId));
    }

    public MeetingMemberCommentLikeId Id { get; }

    public void Remove()
    {
        AddDomainEvent(new MeetingCommentUnlikedDomainEvent(_meetingCommentId, _memberId));
    }

    public static MeetingMemberCommentLike Create(MeetingCommentId meetingCommentId, MemberId memberId)
    {
        return new MeetingMemberCommentLike(meetingCommentId, memberId);
    }
}
