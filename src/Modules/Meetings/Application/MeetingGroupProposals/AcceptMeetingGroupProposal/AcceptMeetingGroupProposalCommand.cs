using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;

public class AcceptMeetingGroupProposalCommand : InternalCommandBase
{
    [JsonConstructor]
    public AcceptMeetingGroupProposalCommand(Guid id, Guid meetingGroupProposalId)
        : base(id)
    {
        MeetingGroupProposalId = meetingGroupProposalId;
    }

    public Guid MeetingGroupProposalId { get; }
}
