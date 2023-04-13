using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

public class MeetingGroup : Entity, IAggregateRoot
{
    private readonly MemberId _creatorId;

    private readonly List<MeetingGroupMember> _members;
    private string _name;

    private string _description;

    private MeetingGroupLocation _location;

    private DateTime _createDate;

    private DateTime? _paymentDateTo;

    private MeetingGroup()
    {
        // Only for EF.
    }

    private MeetingGroup(MeetingGroupProposalId meetingGroupProposalId, string name, string description,
        MeetingGroupLocation location, MemberId creatorId)
    {
        Id = new MeetingGroupId(meetingGroupProposalId.Value);
        _name = name;
        _description = description;
        _creatorId = creatorId;
        _location = location;
        _createDate = SystemClock.Now;

        AddDomainEvent(new MeetingGroupCreatedDomainEvent(Id, creatorId));

        _members = new List<MeetingGroupMember>();
        _members.Add(MeetingGroupMember.CreateNew(Id, _creatorId, MeetingGroupMemberRole.Organizer));
    }

    public MeetingGroupId Id { get; }

    internal static MeetingGroup CreateBasedOnProposal(
        MeetingGroupProposalId meetingGroupProposalId,
        string name,
        string description,
        MeetingGroupLocation location,
        MemberId creatorId)
    {
        return new MeetingGroup(meetingGroupProposalId, name, description, location, creatorId);
    }

    public void EditGeneralAttributes(string name, string description, MeetingGroupLocation location)
    {
        _name = name;
        _description = description;
        _location = location;

        AddDomainEvent(new MeetingGroupGeneralAttributesEditedDomainEvent(_name, _description, _location));
    }

    public void JoinToGroupMember(MemberId memberId)
    {
        CheckRule(new MeetingGroupMemberCannotBeAddedTwiceRule(_members, memberId));

        _members.Add(MeetingGroupMember.CreateNew(Id, memberId, MeetingGroupMemberRole.Member));
    }

    public void LeaveGroup(MemberId memberId)
    {
        CheckRule(new NotActualGroupMemberCannotLeaveGroupRule(_members, memberId));

        var member = _members.Single(x => x.IsMember(memberId));

        member.Leave();
    }

    public void SetExpirationDate(DateTime dateTo)
    {
        _paymentDateTo = dateTo;

        AddDomainEvent(new MeetingGroupPaymentInfoUpdatedDomainEvent(Id, _paymentDateTo.Value));
    }

    public Meeting CreateMeeting(
        string title,
        MeetingTerm term,
        string description,
        MeetingLocation location,
        int? attendeesLimit,
        int guestsLimit,
        Term rsvpTerm,
        MoneyValue eventFee,
        List<MemberId> hostsMembersIds,
        MemberId creatorId)
    {
        CheckRule(new MeetingCanBeOrganizedOnlyByPayedGroupRule(_paymentDateTo));

        CheckRule(new MeetingHostMustBeAMeetingGroupMemberRule(creatorId, hostsMembersIds, _members));

        return Meeting.CreateNew(
            Id,
            title,
            term,
            description,
            location,
            MeetingLimits.Create(attendeesLimit, guestsLimit),
            rsvpTerm,
            eventFee,
            hostsMembersIds,
            creatorId);
    }

    internal bool IsMemberOfGroup(MemberId attendeeId)
    {
        return _members.Any(x => x.IsMember(attendeeId));
    }

    internal bool IsOrganizer(MemberId memberId)
    {
        return _members.Any(x => x.IsOrganizer(memberId));
    }
}
