using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Users.GetUser;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.GetAuthenticatedUser
{
    internal class GetAuthenticatedUserQueryHandler : IQueryHandler<GetAuthenticatedUserQuery, UserDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IExecutionContextAccessor _executionContextAccessor;

        public GetAuthenticatedUserQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IExecutionContextAccessor executionContextAccessor)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<UserDto> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT "
                               + $"u.id AS {nameof(UserDto.Id)}, "
                               + $"u.is_active AS {nameof(UserDto.IsActive)}, "
                               + $"u.login AS {nameof(UserDto.Login)}, "
                               + $"u.email AS {nameof(UserDto.Email)}, "
                               + $"u.name AS {nameof(UserDto.Name)} "
                               + "FROM sss_users.users AS u "
                               + "WHERE u.id = @UserId";

            return await connection.QuerySingleAsync<UserDto>(sql, new
            {
                _executionContextAccessor.UserId
            });
        }
    }
}