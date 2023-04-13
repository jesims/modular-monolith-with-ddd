using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingAttendeesNumberIsAboveLimitRule : IBusinessRule
{
    private readonly int? _attendeesLimit;

    private readonly int _allActiveAttendeesWithGuestsNumber;

    private readonly int _guestsNumber;

    internal MeetingAttendeesNumberIsAboveLimitRule(
        int? attendeesLimit,
        int allActiveAttendeesWithGuestsNumber,
        int guestsNumber)
    {
        _attendeesLimit = attendeesLimit;
        _allActiveAttendeesWithGuestsNumber = allActiveAttendeesWithGuestsNumber;
        _guestsNumber = guestsNumber;
    }

    public string Message => "Meeting attendees number is above limit";

    public bool IsBroken()
    {
        return _attendeesLimit.HasValue &&
               _attendeesLimit.Value < _allActiveAttendeesWithGuestsNumber + 1 + _guestsNumber;
    }
}
