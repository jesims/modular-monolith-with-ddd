using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;

public class MeetingGroupProposedDomainEvent : DomainEventBase
{
    public MeetingGroupProposedDomainEvent(
        MeetingGroupProposalId meetingGroupProposalId,
        string name,
        string description,
        MemberId proposalUserId,
        DateTime proposalDate,
        string locationCity,
        string locationCountryCode)
    {
        MeetingGroupProposalId = meetingGroupProposalId;
        Name = name;
        Description = description;
        LocationCity = locationCity;
        LocationCountryCode = locationCountryCode;
        ProposalDate = proposalDate;
        ProposalUserId = proposalUserId;
    }

    public MeetingGroupProposalId MeetingGroupProposalId { get; }

    public string Name { get; }

    public string Description { get; }

    public string LocationCity { get; }

    public string LocationCountryCode { get; }

    public MemberId ProposalUserId { get; }

    public DateTime ProposalDate { get; }
}
