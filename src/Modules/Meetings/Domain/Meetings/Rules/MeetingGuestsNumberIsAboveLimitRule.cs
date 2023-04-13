using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingGuestsNumberIsAboveLimitRule : IBusinessRule
{
    private readonly int _guestsNumber;

    private readonly int _guestsLimit;

    public MeetingGuestsNumberIsAboveLimitRule(int guestsLimit, int guestsNumber)
    {
        _guestsNumber = guestsNumber;
        _guestsLimit = guestsLimit;
    }

    public string Message => "Meeting guests number is above limit";

    public bool IsBroken()
    {
        return _guestsLimit > 0 && _guestsLimit < _guestsNumber;
    }
}
