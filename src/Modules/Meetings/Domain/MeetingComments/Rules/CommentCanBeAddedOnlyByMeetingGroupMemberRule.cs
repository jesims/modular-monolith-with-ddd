using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;

public class CommentCanBeAddedOnlyByMeetingGroupMemberRule : IBusinessRule
{
    private readonly MemberId _authorId;
    private readonly MeetingGroup _meetingGroup;

    public CommentCanBeAddedOnlyByMeetingGroupMemberRule(MemberId authorId, MeetingGroup meetingGroup)
    {
        _authorId = authorId;
        _meetingGroup = meetingGroup;
    }

    public string Message => "Only meeting attendee can add comments";

    public bool IsBroken()
    {
        return !_meetingGroup.IsMemberOfGroup(_authorId);
    }
}
