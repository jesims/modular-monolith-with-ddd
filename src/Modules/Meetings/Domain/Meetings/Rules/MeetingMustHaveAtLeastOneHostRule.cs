using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingMustHaveAtLeastOneHostRule : IBusinessRule
{
    private readonly int _meetingHostNumber;

    public MeetingMustHaveAtLeastOneHostRule(int meetingHostNumber)
    {
        _meetingHostNumber = meetingHostNumber;
    }

    public string Message => "Meeting must have at least one host";

    public bool IsBroken()
    {
        return _meetingHostNumber == 0;
    }
}
