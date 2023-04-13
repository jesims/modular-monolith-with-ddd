using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentLike;

public class AddMeetingCommentLikeCommand : CommandBase
{
    public AddMeetingCommentLikeCommand(Guid meetingCommentId)
    {
        MeetingCommentId = meetingCommentId;
    }

    public Guid MeetingCommentId { get; }
}
