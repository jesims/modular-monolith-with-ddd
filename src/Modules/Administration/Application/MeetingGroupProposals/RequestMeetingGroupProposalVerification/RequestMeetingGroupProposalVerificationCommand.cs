using System;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.
    RequestMeetingGroupProposalVerification;

public class RequestMeetingGroupProposalVerificationCommand : InternalCommandBase<Guid>
{
    [JsonConstructor]
    public RequestMeetingGroupProposalVerificationCommand(
        Guid id,
        Guid meetingGroupProposalId,
        string name,
        string description,
        string locationCity,
        string locationCountryCode,
        Guid proposalUserId,
        DateTime proposalDate)
        : base(id)
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
