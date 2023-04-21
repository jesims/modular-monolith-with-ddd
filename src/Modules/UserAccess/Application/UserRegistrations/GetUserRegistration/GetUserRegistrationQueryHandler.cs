using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.GetUserRegistration
{
    internal class GetUserRegistrationQueryHandler : IQueryHandler<GetUserRegistrationQuery, UserRegistrationDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserRegistrationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserRegistrationDto> Handle(GetUserRegistrationQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT "
                                + $"user_registration.id AS {nameof(UserRegistrationDto.Id)}, "
                                + $"user_registration.login AS {nameof(UserRegistrationDto.Login)}, "
                                + $"user_registration.email AS {nameof(UserRegistrationDto.Email)}, "
                                + $"user_registration.first_name AS {nameof(UserRegistrationDto.FirstName)}, "
                                + $"user_registration.last_name AS {nameof(UserRegistrationDto.LastName)}, "
                                + $"user_registration.name AS {nameof(UserRegistrationDto.Name)}, "
                                + $"user_registration.status_code AS {nameof(UserRegistrationDto.StatusCode)} "
                                + "FROM sss_users.user_registrations AS user_registration "
                                + "WHERE user_registration.id = @UserRegistrationId";

            return await connection.QuerySingleAsync<UserRegistrationDto>(
                sql,
                new
                {
                    query.UserRegistrationId
                });
        }
    }
}