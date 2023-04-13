using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingGuestsLimitCannotBeNegativeRule : IBusinessRule
{
    private readonly int _guestsLimit;

    public MeetingGuestsLimitCannotBeNegativeRule(int guestsLimit)
    {
        _guestsLimit = guestsLimit;
    }

    public string Message => "Guests limit cannot be negative";

    public bool IsBroken()
    {
        return _guestsLimit < 0;
    }
}
