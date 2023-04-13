using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

internal class AttendeesLimitCannotBeChangedToSmallerThanActiveAttendeesRule : IBusinessRule
{
    private readonly int? _attendeesLimit;

    private readonly int _allActiveAttendeesWithGuestsNumber;

    internal AttendeesLimitCannotBeChangedToSmallerThanActiveAttendeesRule(
        MeetingLimits meetingLimits,
        int allActiveAttendeesWithGuestsNumber)
    {
        _attendeesLimit = meetingLimits.AttendeesLimit;
        _allActiveAttendeesWithGuestsNumber = allActiveAttendeesWithGuestsNumber;
    }

    public string Message => "Attendees limit cannot be change to smaller than active attendees number";

    public bool IsBroken()
    {
        return _attendeesLimit.HasValue && _attendeesLimit.Value < _allActiveAttendeesWithGuestsNumber;
    }
}
