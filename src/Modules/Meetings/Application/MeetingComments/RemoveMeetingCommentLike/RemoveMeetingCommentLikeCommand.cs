using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingCommentLike;

public class RemoveMeetingCommentLikeCommand : CommandBase
{
    public RemoveMeetingCommentLikeCommand(Guid meetingCommentId)
    {
        MeetingCommentId = meetingCommentId;
    }

    public Guid MeetingCommentId { get; }
}
