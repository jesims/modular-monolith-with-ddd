using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;

public class MeetingComment : Entity, IAggregateRoot
{
    private readonly MeetingId _meetingId;

    private readonly MemberId _authorId;

    private MeetingCommentId? _inReplyToCommentId;

    private string _comment;

    private DateTime _createDate;

    private DateTime? _editDate;

    private bool _isRemoved;

    private string _removedByReason;

    private MeetingComment(
        MeetingId meetingId,
        MemberId authorId,
        string comment,
        MeetingCommentId? inReplyToCommentId,
        MeetingCommentingConfiguration meetingCommentingConfiguration,
        MeetingGroup meetingGroup)
    {
        CheckRule(new CommentTextMustBeProvidedRule(comment));
        CheckRule(new CommentCanBeCreatedOnlyIfCommentingForMeetingEnabledRule(meetingCommentingConfiguration));
        CheckRule(new CommentCanBeAddedOnlyByMeetingGroupMemberRule(authorId, meetingGroup));

        Id = new MeetingCommentId(Guid.NewGuid());
        _meetingId = meetingId;
        _authorId = authorId;
        _comment = comment;

        _inReplyToCommentId = inReplyToCommentId;

        _createDate = SystemClock.Now;
        _editDate = null;

        _isRemoved = false;
        _removedByReason = null;

        if (inReplyToCommentId == null)
        {
            AddDomainEvent(new MeetingCommentAddedDomainEvent(Id, _meetingId, comment));
        }
        else
        {
            AddDomainEvent(new ReplyToMeetingCommentAddedDomainEvent(Id, inReplyToCommentId, comment));
        }
    }

    private MeetingComment()
    {
        // Only for EF.
    }

    public MeetingCommentId Id { get; }

    public void Edit(MemberId editorId, string editedComment,
        MeetingCommentingConfiguration meetingCommentingConfiguration)
    {
        CheckRule(new CommentTextMustBeProvidedRule(editedComment));
        CheckRule(new MeetingCommentCanBeEditedOnlyByAuthorRule(_authorId, editorId));
        CheckRule(new CommentCanBeEditedOnlyIfCommentingForMeetingEnabledRule(meetingCommentingConfiguration));

        _comment = editedComment;
        _editDate = SystemClock.Now;

        AddDomainEvent(new MeetingCommentEditedDomainEvent(Id, editedComment));
    }

    public void Remove(MemberId removingMemberId, MeetingGroup meetingGroup, string reason = null)
    {
        CheckRule(new MeetingCommentCanBeRemovedOnlyByAuthorOrGroupOrganizerRule(meetingGroup, _authorId,
            removingMemberId));
        CheckRule(new RemovingReasonCanBeProvidedOnlyByGroupOrganizerRule(meetingGroup, removingMemberId, reason));

        _isRemoved = true;
        _removedByReason = reason ?? string.Empty;

        AddDomainEvent(new MeetingCommentRemovedDomainEvent(Id));
    }

    public MeetingComment Reply(MemberId replierId, string reply, MeetingGroup meetingGroup,
        MeetingCommentingConfiguration meetingCommentingConfiguration)
    {
        return new MeetingComment(
            _meetingId,
            replierId,
            reply,
            Id,
            meetingCommentingConfiguration,
            meetingGroup);
    }

    public MeetingMemberCommentLike Like(
        MemberId likerId,
        MeetingGroupMemberData likerMeetingGroupMember,
        int meetingMemberCommentLikesCount)
    {
        CheckRule(new CommentCanBeLikedOnlyByMeetingGroupMemberRule(likerMeetingGroupMember));
        CheckRule(new CommentCannotBeLikedByTheSameMemberMoreThanOnceRule(meetingMemberCommentLikesCount));

        return MeetingMemberCommentLike.Create(Id, likerId);
    }

    public MeetingId GetMeetingId()
    {
        return _meetingId;
    }

    internal static MeetingComment Create(
        MeetingId meetingId,
        MemberId authorId,
        string comment,
        MeetingGroup meetingGroup,
        MeetingCommentingConfiguration meetingCommentingConfiguration)
    {
        return new MeetingComment(
            meetingId,
            authorId,
            comment,
            null,
            meetingCommentingConfiguration,
            meetingGroup);
    }
}
