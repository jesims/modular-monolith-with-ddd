using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;

public class MeetingGroupProposalStatus : ValueObject
{
    private MeetingGroupProposalStatus(string value)
    {
        Value = value;
    }

    public string Value { get; }

    internal static MeetingGroupProposalStatus InVerification => new("InVerification");

    internal static MeetingGroupProposalStatus Accepted => new("Accepted");

    internal bool IsAccepted => Value == "Accepted";
}
