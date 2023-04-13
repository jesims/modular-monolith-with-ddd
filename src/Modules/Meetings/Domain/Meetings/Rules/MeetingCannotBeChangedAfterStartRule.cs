using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class MeetingCannotBeChangedAfterStartRule : IBusinessRule
{
    private readonly MeetingTerm _meetingTerm;

    public MeetingCannotBeChangedAfterStartRule(MeetingTerm meetingTerm)
    {
        _meetingTerm = meetingTerm;
    }

    public string Message => "Meeting cannot be changed after start";

    public bool IsBroken()
    {
        return _meetingTerm.IsAfterStart();
    }
}
