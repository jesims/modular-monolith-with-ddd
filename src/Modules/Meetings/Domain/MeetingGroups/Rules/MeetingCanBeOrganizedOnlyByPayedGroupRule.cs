﻿using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules;

public class MeetingCanBeOrganizedOnlyByPayedGroupRule : IBusinessRule
{
    private readonly DateTime? _paymentDateTo;

    internal MeetingCanBeOrganizedOnlyByPayedGroupRule(DateTime? paymentDateTo)
    {
        _paymentDateTo = paymentDateTo;
    }

    public string Message => "Meeting can be organized only by payed group";

    public bool IsBroken()
    {
        return !_paymentDateTo.HasValue || _paymentDateTo < SystemClock.Now;
    }
}
