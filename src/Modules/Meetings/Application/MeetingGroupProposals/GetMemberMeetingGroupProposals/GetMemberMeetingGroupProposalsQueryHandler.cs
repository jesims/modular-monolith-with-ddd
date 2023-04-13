using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMemberMeetingGroupProposals;

internal class
    GetMemberMeetingGroupProposalsQueryHandler : IQueryHandler<GetMemberMeetingGroupProposalsQuery,
        List<MeetingGroupProposalDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    private readonly IMemberContext _memberContext;

    public GetMemberMeetingGroupProposalsQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory,
        IMemberContext memberContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _memberContext = memberContext;
    }

    public async Task<List<MeetingGroupProposalDto>> Handle(GetMemberMeetingGroupProposalsQuery query,
        CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sql = "SELECT "
                  + $"meeting_group_proposal.id AS {nameof(MeetingGroupProposalDto.Id)}, "
                  + $"meeting_group_proposal.name AS {nameof(MeetingGroupProposalDto.Name)}, "
                  + $"meeting_group_proposal.proposal_user_id AS {nameof(MeetingGroupProposalDto.ProposalUserId)}, "
                  + $"meeting_group_proposal.location_city AS {nameof(MeetingGroupProposalDto.LocationCity)}, "
                  + $"meeting_group_proposal.location_country_code AS {nameof(MeetingGroupProposalDto.LocationCountryCode)}, "
                  + $"meeting_group_proposal.description AS {nameof(MeetingGroupProposalDto.Description)}, "
                  + $"meeting_group_proposal.proposal_date AS {nameof(MeetingGroupProposalDto.ProposalDate)}, "
                  + $"meeting_group_proposal.status_code AS {nameof(MeetingGroupProposalDto.StatusCode)} "
                  + "FROM sss_meetings.meeting_group_proposals AS meeting_group_proposal "
                  + "WHERE meeting_group_proposal.proposal_user_id = @MemberId "
                  + "ORDER BY meeting_group_proposal.name";

        var meetingGroupProposals = await connection.QueryAsync<MeetingGroupProposalDto>(
            sql,
            new
            {
                MemberId = _memberContext.MemberId.Value
            });

        return meetingGroupProposals.AsList();
    }
}
