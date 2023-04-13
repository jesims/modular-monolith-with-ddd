using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Users;

internal class UserContext : IUserContext
{
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public UserContext(IExecutionContextAccessor executionContextAccessor)
    {
        _executionContextAccessor = executionContextAccessor;
    }

    public UserId UserId => new(_executionContextAccessor.UserId);
}
