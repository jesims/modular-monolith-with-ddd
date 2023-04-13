using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

public class MeetingTerm : ValueObject
{
    private MeetingTerm(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public DateTime StartDate { get; }

    public DateTime EndDate { get; }

    public static MeetingTerm CreateNewBetweenDates(DateTime startDate, DateTime endDate)
    {
        return new MeetingTerm(startDate, endDate);
    }

    internal bool IsAfterStart()
    {
        return SystemClock.Now > StartDate;
    }
}
