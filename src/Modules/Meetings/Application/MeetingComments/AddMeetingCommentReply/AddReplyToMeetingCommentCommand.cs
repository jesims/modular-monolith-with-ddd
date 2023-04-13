using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddCommentReply;

public class AddReplyToMeetingCommentCommand : CommandBase<Guid>
{
    public AddReplyToMeetingCommentCommand(Guid inReplyToCommentId, string reply)
    {
        InReplyToCommentId = inReplyToCommentId;
        Reply = reply;
    }

    public Guid InReplyToCommentId { get; }

    public string Reply { get; }
}
