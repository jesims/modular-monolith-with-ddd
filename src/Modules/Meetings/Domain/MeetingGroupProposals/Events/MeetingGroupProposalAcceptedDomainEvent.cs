using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;

public class MeetingGroupProposalAcceptedDomainEvent : DomainEventBase
{
    public MeetingGroupProposalAcceptedDomainEvent(MeetingGroupProposalId meetingGroupProposalId)
    {
        MeetingGroupProposalId = meetingGroupProposalId;
    }

    public MeetingGroupProposalId MeetingGroupProposalId { get; }
}
