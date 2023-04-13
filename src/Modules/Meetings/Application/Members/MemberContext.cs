using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members;

public class MemberContext : IMemberContext
{
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public MemberContext(IExecutionContextAccessor executionContextAccessor)
    {
        _executionContextAccessor = executionContextAccessor;
    }

    public MemberId MemberId => new(_executionContextAccessor.UserId);
}
