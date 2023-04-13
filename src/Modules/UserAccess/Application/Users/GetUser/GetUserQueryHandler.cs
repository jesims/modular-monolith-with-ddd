using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.GetUser;

internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT "
                           + $"u.id AS {nameof(UserDto.Id)}, "
                           + $"u.login AS {nameof(UserDto.Login)}, "
                           + $"u.name AS {nameof(UserDto.Name)}, "
                           + $"u.email AS {nameof(UserDto.Email)}, "
                           + $"u.is_active AS {nameof(UserDto.IsActive)} "
                           + "FROM sss_users.users AS u "
                           + "WHERE u.id = @UserId";

        return await connection.QuerySingleAsync<UserDto>(sql, new
        {
            request.UserId
        });
    }
}
