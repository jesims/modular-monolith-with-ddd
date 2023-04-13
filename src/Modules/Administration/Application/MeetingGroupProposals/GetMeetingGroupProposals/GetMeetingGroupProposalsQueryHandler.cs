using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposals;

internal class
    GetMeetingGroupProposalsQueryHandler : IQueryHandler<GetMeetingGroupProposalsQuery, List<MeetingGroupProposalDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetMeetingGroupProposalsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<List<MeetingGroupProposalDto>> Handle(GetMeetingGroupProposalsQuery query,
        CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sql = "SELECT " +
                  $"meeting_group_proposal.id AS {nameof(MeetingGroupProposalDto.Id)}, " +
                  $"meeting_group_proposal.name AS {nameof(MeetingGroupProposalDto.Name)}, " +
                  $"meeting_group_proposal.proposal_user_id AS {nameof(MeetingGroupProposalDto.ProposalUserId)}, " +
                  $"meeting_group_proposal.location_city AS {nameof(MeetingGroupProposalDto.LocationCity)}, " +
                  $"meeting_group_proposal.location_country_code AS {nameof(MeetingGroupProposalDto.LocationCountryCode)}, " +
                  $"meeting_group_proposal.description AS {nameof(MeetingGroupProposalDto.Description)}, " +
                  $"meeting_group_proposal.proposal_date AS {nameof(MeetingGroupProposalDto.ProposalDate)}, " +
                  $"meeting_group_proposal.status_code AS {nameof(MeetingGroupProposalDto.StatusCode)}, " +
                  $"meeting_group_proposal.decision_date AS {nameof(MeetingGroupProposalDto.DecisionDate)}, " +
                  $"meeting_group_proposal.decision_user_id AS {nameof(MeetingGroupProposalDto.DecisionUserId)}, " +
                  $"meeting_group_proposal.decision_code AS {nameof(MeetingGroupProposalDto.DecisionCode)}, " +
                  $"meeting_group_proposal.decision_reject_reason AS {nameof(MeetingGroupProposalDto.DecisionRejectReason)} " +
                  "FROM sss_administration.meeting_group_proposals AS meeting_group_proposal ";

        return (await connection.QueryAsync<MeetingGroupProposalDto>(sql)).AsList();
    }
}
