using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

public class MeetingAttendee : Entity
{
    private DateTime _decisionDate;

    private MeetingAttendeeRole _role;

    private readonly int _guestsNumber;

    private bool _decisionChanged;

    private DateTime? _decisionChangeDate;

    private DateTime? _removedDate;

    private MemberId _removingMemberId;

    private string _removingReason;

    private bool _isRemoved;

    private readonly MoneyValue _fee;

    private bool _isFeePaid;

    private MeetingAttendee()
    {
    }

    private MeetingAttendee(
        MeetingId meetingId,
        MemberId attendeeId,
        DateTime decisionDate,
        MeetingAttendeeRole role,
        int guestsNumber,
        MoneyValue eventFee)
    {
        AttendeeId = attendeeId;
        MeetingId = meetingId;
        _decisionDate = decisionDate;
        _role = role;
        _guestsNumber = guestsNumber;
        _decisionChanged = false;
        _isFeePaid = false;

        if (eventFee != MoneyValue.Undefined)
        {
            _fee = (1 + guestsNumber) * eventFee;
        }
        else
        {
            _fee = MoneyValue.Undefined;
        }

        AddDomainEvent(new MeetingAttendeeAddedDomainEvent(
            MeetingId,
            AttendeeId,
            decisionDate,
            role.Value,
            guestsNumber,
            _fee.Value,
            _fee.Currency));
    }

    internal MemberId AttendeeId { get; }

    internal MeetingId MeetingId { get; }

    internal static MeetingAttendee CreateNew(
        MeetingId meetingId,
        MemberId attendeeId,
        DateTime decisionDate,
        MeetingAttendeeRole role,
        int guestsNumber,
        MoneyValue eventFee)
    {
        return new MeetingAttendee(meetingId, attendeeId, decisionDate, role, guestsNumber, eventFee);
    }

    internal void ChangeDecision()
    {
        _decisionChanged = true;
        _decisionChangeDate = SystemClock.Now;

        AddDomainEvent(new MeetingAttendeeChangedDecisionDomainEvent(AttendeeId, MeetingId));
    }

    internal bool IsActiveAttendee(MemberId attendeeId)
    {
        return AttendeeId == attendeeId && !_decisionChanged;
    }

    internal bool IsActive()
    {
        return !_decisionChangeDate.HasValue && !_isRemoved;
    }

    internal bool IsActiveHost()
    {
        return IsActive() && _role == MeetingAttendeeRole.Host;
    }

    internal int GetAttendeeWithGuestsNumber()
    {
        return 1 + _guestsNumber;
    }

    internal void SetAsHost()
    {
        _role = MeetingAttendeeRole.Host;

        AddDomainEvent(new NewMeetingHostSetDomainEvent(MeetingId, AttendeeId));
    }

    internal void SetAsAttendee()
    {
        CheckRule(new MemberCannotHaveSetAttendeeRoleMoreThanOnceRule(_role));
        _role = MeetingAttendeeRole.Attendee;

        AddDomainEvent(new MemberSetAsAttendeeDomainEvent(MeetingId, AttendeeId));
    }

    internal void Remove(MemberId removingMemberId, string reason)
    {
        CheckRule(new ReasonOfRemovingAttendeeFromMeetingMustBeProvidedRule(reason));

        _isRemoved = true;
        _removedDate = SystemClock.Now;
        _removingReason = reason;
        _removingMemberId = removingMemberId;

        AddDomainEvent(new MeetingAttendeeRemovedDomainEvent(AttendeeId, MeetingId, reason));
    }

    internal void MarkFeeAsPayed()
    {
        _isFeePaid = true;

        AddDomainEvent(new MeetingAttendeeFeePaidDomainEvent(MeetingId, AttendeeId));
    }
}
