using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Authorization.GetUserPermissions;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Authorization.GetAuthenticatedUserPermissions
{
    internal class GetAuthenticatedUserPermissionsQueryHandler : IQueryHandler<GetAuthenticatedUserPermissionsQuery, List<UserPermissionDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IExecutionContextAccessor _executionContextAccessor;

        public GetAuthenticatedUserPermissionsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IExecutionContextAccessor executionContextAccessor)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<List<UserPermissionDto>> Handle(GetAuthenticatedUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            if (!_executionContextAccessor.IsAvailable)
            {
                return new List<UserPermissionDto>();
            }

            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT DISTINCT "
                               + "permissions.permission_code AS Code "
                               + "FROM sss_users.user_roles AS roles "
                               + "     INNER JOIN sss_users.roles_to_permissions as permissions "
                               + "         ON roles.role_code = permissions.role_code "
                               + "WHERE user_permission.user_id = @UserId;";
            var permissions = await connection.QueryAsync<UserPermissionDto>(
                sql,
                new { _executionContextAccessor.UserId });

            return permissions.AsList();
        }
    }
}