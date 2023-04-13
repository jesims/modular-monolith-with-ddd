using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

public class MeetingNotAttendee : Entity
{
    private DateTime _decisionDate;

    private bool _decisionChanged;

    private DateTime? _decisionChangeDate;

    private MeetingNotAttendee()
    {
    }

    private MeetingNotAttendee(MeetingId meetingId, MemberId memberId)
    {
        MemberId = memberId;
        MeetingId = meetingId;
        _decisionDate = DateTime.UtcNow;

        AddDomainEvent(new MeetingNotAttendeeAddedDomainEvent(MeetingId, MemberId));
    }

    internal MemberId MemberId { get; }

    internal MeetingId MeetingId { get; }

    internal static MeetingNotAttendee CreateNew(MeetingId meetingId, MemberId memberId)
    {
        return new MeetingNotAttendee(meetingId, memberId);
    }

    internal bool IsActiveNotAttendee(MemberId memberId)
    {
        return !_decisionChanged && MemberId == memberId;
    }

    internal void ChangeDecision()
    {
        _decisionChanged = true;
        _decisionChangeDate = SystemClock.Now;

        AddDomainEvent(new MeetingNotAttendeeChangedDecisionDomainEvent(MemberId, MeetingId));
    }
}
