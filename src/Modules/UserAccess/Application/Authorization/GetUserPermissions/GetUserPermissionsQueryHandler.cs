using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Authorization.GetUserPermissions;

internal class GetUserPermissionsQueryHandler : IQueryHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUserPermissionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT "
                           + $"user_permission.permission_code AS {nameof(UserPermissionDto.Code)} "
                           + "FROM sss_users.user_roles AS user_roles"
                           + "    INNER JOIN sss_users.roles_to_permissions AS user_permission "
                           + "        ON user_roles.role_code = user_permission.role_code "
                           + "WHERE user_roles.user_id = @UserId";
        var permissions = await connection.QueryAsync<UserPermissionDto>(sql, new { request.UserId });

        return permissions.AsList();
    }
}
