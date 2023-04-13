using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class AttendeeCanBeAddedOnlyInRsvpTermRule : IBusinessRule
{
    private readonly Term _rsvpTerm;

    internal AttendeeCanBeAddedOnlyInRsvpTermRule(Term rsvpTerm)
    {
        _rsvpTerm = rsvpTerm;
    }

    public string Message => "Attendee can be added only in RSVP term";

    public bool IsBroken()
    {
        return !_rsvpTerm.IsInTerm(SystemClock.Now);
    }
}
