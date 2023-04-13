using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

public class MeetingWaitlistMember : Entity
{
    private bool _isSignedOff;

    private DateTime? _signOffDate;

    private bool _isMovedToAttendees;

    private DateTime? _movedToAttendeesDate;

    private MeetingWaitlistMember()
    {
    }

    private MeetingWaitlistMember(MeetingId meetingId, MemberId memberId)
    {
        MemberId = memberId;
        MeetingId = meetingId;
        SignUpDate = SystemClock.Now;
        _isMovedToAttendees = false;

        AddDomainEvent(new MeetingWaitlistMemberAddedDomainEvent(MeetingId, MemberId));
    }

    internal MemberId MemberId { get; }

    internal MeetingId MeetingId { get; }

    internal DateTime SignUpDate { get; private set; }

    internal static MeetingWaitlistMember CreateNew(MeetingId meetingId, MemberId memberId)
    {
        return new MeetingWaitlistMember(meetingId, memberId);
    }

    internal void MarkIsMovedToAttendees()
    {
        _isMovedToAttendees = true;
        _movedToAttendeesDate = SystemClock.Now;
    }

    internal bool IsActiveOnWaitList(MemberId memberId)
    {
        return MemberId == memberId && IsActive();
    }

    internal bool IsActive()
    {
        return !_isSignedOff && !_isMovedToAttendees;
    }

    internal void SignOff()
    {
        _isSignedOff = true;
        _signOffDate = SystemClock.Now;

        AddDomainEvent(new MemberSignedOffFromMeetingWaitlistDomainEvent(MeetingId, MemberId));
    }
}
