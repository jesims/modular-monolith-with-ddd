using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Rules;

public class UserRegistrationCannotBeExpiredMoreThanOnceRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserRegistrationCannotBeExpiredMoreThanOnceRule(UserRegistrationStatus actualRegistrationStatus)
    {
        _actualRegistrationStatus = actualRegistrationStatus;
    }

    public string Message => "User Registration cannot be expired more than once";

    public bool IsBroken()
    {
        return _actualRegistrationStatus == UserRegistrationStatus.Expired;
    }
}
