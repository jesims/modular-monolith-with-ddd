using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

public class Term : ValueObject
{
    private Term(DateTime? startDate, DateTime? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public static Term NoTerm => new(null, null);

    public DateTime? StartDate { get; }

    public DateTime? EndDate { get; }

    public static Term CreateNewBetweenDates(DateTime? startDate, DateTime? endDate)
    {
        return new Term(startDate, endDate);
    }

    internal bool IsInTerm(DateTime date)
    {
        var left = !StartDate.HasValue || StartDate.Value <= date;

        var right = !EndDate.HasValue || EndDate.Value >= date;

        return left && right;
    }
}
