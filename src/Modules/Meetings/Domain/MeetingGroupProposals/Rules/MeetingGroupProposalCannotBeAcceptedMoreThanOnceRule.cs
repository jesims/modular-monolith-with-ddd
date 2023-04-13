using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Rules;

public class MeetingGroupProposalCannotBeAcceptedMoreThanOnceRule : IBusinessRule
{
    private readonly MeetingGroupProposalStatus _actualStatus;

    internal MeetingGroupProposalCannotBeAcceptedMoreThanOnceRule(MeetingGroupProposalStatus actualStatus)
    {
        _actualStatus = actualStatus;
    }

    public string Message => "Meeting group proposal cannot be accepted more than once rule";

    public bool IsBroken()
    {
        return _actualStatus.IsAccepted;
    }
}
