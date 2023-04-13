using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingComment;

public class RemoveMeetingCommentCommand : CommandBase
{
    public RemoveMeetingCommentCommand(Guid meetingCommentId, string reason)
    {
        MeetingCommentId = meetingCommentId;
        Reason = reason;
    }

    public Guid MeetingCommentId { get; }

    public string Reason { get; }
}
