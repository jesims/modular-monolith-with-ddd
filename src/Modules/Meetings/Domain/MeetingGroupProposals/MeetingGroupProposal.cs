using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;

public class MeetingGroupProposal : Entity, IAggregateRoot
{
    private readonly string _name;

    private readonly string _description;

    private readonly MeetingGroupLocation _location;

    private readonly DateTime _proposalDate;

    private readonly MemberId _proposalUserId;

    private MeetingGroupProposalStatus _status;

    private MeetingGroupProposal()
    {
        // Only for EF.
    }

    private MeetingGroupProposal(
        string name,
        string description,
        MeetingGroupLocation location,
        MemberId proposalUserId)
    {
        Id = new MeetingGroupProposalId(Guid.NewGuid());
        _name = name;
        _description = description;
        _location = location;
        _proposalUserId = proposalUserId;
        _proposalDate = SystemClock.Now;
        _status = MeetingGroupProposalStatus.InVerification;

        AddDomainEvent(new MeetingGroupProposedDomainEvent(Id, _name, _description, proposalUserId, _proposalDate,
            _location.City, _location.CountryCode));
    }

    public MeetingGroupProposalId Id { get; }

    public MeetingGroup CreateMeetingGroup()
    {
        return MeetingGroup.CreateBasedOnProposal(Id, _name, _description, _location, _proposalUserId);
    }

    public static MeetingGroupProposal ProposeNew(
        string name,
        string description,
        MeetingGroupLocation location,
        MemberId proposalMemberId)
    {
        return new MeetingGroupProposal(name, description, location, proposalMemberId);
    }

    public void Accept()
    {
        CheckRule(new MeetingGroupProposalCannotBeAcceptedMoreThanOnceRule(_status));

        _status = MeetingGroupProposalStatus.Accepted;

        AddDomainEvent(new MeetingGroupProposalAcceptedDomainEvent(Id));
    }
}
