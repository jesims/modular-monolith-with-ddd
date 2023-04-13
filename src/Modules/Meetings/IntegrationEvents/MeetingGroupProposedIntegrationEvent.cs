using System;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents;

public class MeetingGroupProposedIntegrationEvent : IntegrationEvent
{
    public MeetingGroupProposedIntegrationEvent(
        Guid id,
        DateTime occurredOn,
        Guid meetingGroupProposalId,
        string name,
        string description,
        string locationCity,
        string locationCountryCode,
        Guid proposalUserId,
        DateTime proposalDate)
        : base(id, occurredOn)
    {
        MeetingGroupProposalId = meetingGroupProposalId;
        Name = name;
        Description = description;
        LocationCity = locationCity;
        LocationCountryCode = locationCountryCode;
        ProposalUserId = proposalUserId;
        ProposalDate = proposalDate;
    }

    public Guid MeetingGroupProposalId { get; }

    public string Name { get; }

    public string Description { get; }

    public string LocationCity { get; }

    public string LocationCountryCode { get; }

    public Guid ProposalUserId { get; }

    public DateTime ProposalDate { get; }
}
