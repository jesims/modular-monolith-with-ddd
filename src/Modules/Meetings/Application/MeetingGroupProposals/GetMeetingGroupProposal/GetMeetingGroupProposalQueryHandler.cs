using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal
{
    internal class GetMeetingGroupProposalQueryHandler : IQueryHandler<GetMeetingGroupProposalQuery, MeetingGroupProposalDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingGroupProposalQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MeetingGroupProposalDto> Handle(GetMeetingGroupProposalQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            string sql = "SELECT " +
                         $"proposal.id AS {nameof(MeetingGroupProposalDto.Id)}, " +
                         $"proposal.name AS {nameof(MeetingGroupProposalDto.Name)}, " +
                         $"proposal.proposal_user_id AS {nameof(MeetingGroupProposalDto.ProposalUserId)}, " +
                         $"proposal.location_city AS {nameof(MeetingGroupProposalDto.LocationCity)}, " +
                         $"proposal.location_country_code AS {nameof(MeetingGroupProposalDto.LocationCountryCode)}, " +
                         $"proposal.description AS {nameof(MeetingGroupProposalDto.Description)}, " +
                         $"proposal.proposal_date AS {nameof(MeetingGroupProposalDto.ProposalDate)}, " +
                         $"proposal.status_code AS {nameof(MeetingGroupProposalDto.StatusCode)} " +
                         "FROM sss_meetings.meeting_group_proposals AS proposal " +
                         "WHERE proposal.id = @MeetingGroupProposalId";

            return await connection.QuerySingleAsync<MeetingGroupProposalDto>(sql, new { query.MeetingGroupProposalId });
        }
    }
}