using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Application.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetAllMeetingGroupProposals
{
    internal class GetAllMeetingGroupProposalsQueryHandler : IQueryHandler<GetAllMeetingGroupProposalsQuery, List<MeetingGroupProposalDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllMeetingGroupProposalsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingGroupProposalDto>> Handle(GetAllMeetingGroupProposalsQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var parameters = new DynamicParameters();
            var pageData = PagedQueryHelper.GetPageData(query);
            parameters.Add(nameof(PagedQueryHelper.Offset), pageData.Offset);
            parameters.Add(nameof(PagedQueryHelper.Next), pageData.Next);

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
                         "ORDER BY proposal.name";

            sql = PagedQueryHelper.AppendPageStatement(sql);

            var meetingGroupProposals = await connection.QueryAsync<MeetingGroupProposalDto>(sql, parameters);

            return meetingGroupProposals.AsList();
        }
    }
}