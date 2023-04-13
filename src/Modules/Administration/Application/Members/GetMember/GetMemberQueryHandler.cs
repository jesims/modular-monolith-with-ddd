using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Members.GetMember;

internal class GetMemberQueryHandler : IQueryHandler<GetMemberQuery, MemberDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetMemberQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<MemberDto> Handle(GetMemberQuery query, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var sql = "SELECT " +
                  $"member.id AS {nameof(MemberDto.Id)}, " +
                  $"member.login AS {nameof(MemberDto.Login)}, " +
                  $"member.email AS {nameof(MemberDto.Email)}, " +
                  $"member.first_name AS {nameof(MemberDto.FirstName)}, " +
                  $"member.last_name AS {nameof(MemberDto.LastName)}, " +
                  $"member.name AS {nameof(MemberDto.Name)} " +
                  "FROM sss_administration.members AS member " +
                  "WHERE member.id = @MemberId";

        return await connection.QuerySingleAsync<MemberDto>(sql, new { query.MemberId });
    }
}
