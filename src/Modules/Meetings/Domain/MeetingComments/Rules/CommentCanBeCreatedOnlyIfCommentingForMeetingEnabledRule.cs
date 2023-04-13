using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;

public class CommentCanBeCreatedOnlyIfCommentingForMeetingEnabledRule : IBusinessRule
{
    private readonly MeetingCommentingConfiguration _meetingCommentingConfiguration;

    public CommentCanBeCreatedOnlyIfCommentingForMeetingEnabledRule(
        MeetingCommentingConfiguration meetingCommentingConfiguration)
    {
        _meetingCommentingConfiguration = meetingCommentingConfiguration;
    }

    public string Message => "Commenting for meeting is disabled.";

    public bool IsBroken()
    {
        return !_meetingCommentingConfiguration.GetIsCommentingEnabled();
    }
}
