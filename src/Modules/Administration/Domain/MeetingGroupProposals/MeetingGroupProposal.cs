using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Rules;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;

public class MeetingGroupProposal : Entity, IAggregateRoot
{
    private string _name;

    private string _description;

    private MeetingGroupLocation _location;

    private DateTime _proposalDate;

    private UserId _proposalUserId;

    private MeetingGroupProposalStatus _status;

    private MeetingGroupProposalDecision _decision;

    private MeetingGroupProposal(
        MeetingGroupProposalId id,
        string name,
        string description,
        MeetingGroupLocation location,
        UserId proposalUserId,
        DateTime proposalDate)
    {
        Id = id;
        _name = name;
        _description = description;
        _location = location;
        _proposalUserId = proposalUserId;
        _proposalDate = proposalDate;

        _status = MeetingGroupProposalStatus.ToVerify;
        _decision = MeetingGroupProposalDecision.NoDecision;

        AddDomainEvent(new MeetingGroupProposalVerificationRequestedDomainEvent(Id));
    }

    private MeetingGroupProposal()
    {
        _decision = MeetingGroupProposalDecision.NoDecision;
    }

    public MeetingGroupProposalId Id { get; }

    public void Accept(UserId userId)
    {
        CheckRule(new MeetingGroupProposalCanBeVerifiedOnceRule(_decision));

        _decision = MeetingGroupProposalDecision.AcceptDecision(DateTime.UtcNow, userId);

        _status = _decision.GetStatusForDecision();

        AddDomainEvent(new MeetingGroupProposalAcceptedDomainEvent(Id));
    }

    public void Reject(UserId userId, string rejectReason)
    {
        CheckRule(new MeetingGroupProposalCanBeVerifiedOnceRule(_decision));
        CheckRule(new MeetingGroupProposalRejectionMustHaveAReasonRule(rejectReason));

        _decision = MeetingGroupProposalDecision.RejectDecision(DateTime.UtcNow, userId, rejectReason);

        _status = _decision.GetStatusForDecision();

        AddDomainEvent(new MeetingGroupProposalRejectedDomainEvent(Id));
    }

    public static MeetingGroupProposal CreateToVerify(
        Guid meetingGroupProposalId,
        string name,
        string description,
        MeetingGroupLocation location,
        UserId proposalUserId,
        DateTime proposalDate)
    {
        var meetingGroupProposal = new MeetingGroupProposal(
            new MeetingGroupProposalId(meetingGroupProposalId),
            name,
            description,
            location,
            proposalUserId,
            proposalDate);

        return meetingGroupProposal;
    }
}
