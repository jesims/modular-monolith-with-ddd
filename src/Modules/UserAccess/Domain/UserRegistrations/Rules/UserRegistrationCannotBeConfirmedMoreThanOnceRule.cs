using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Rules;

public class UserRegistrationCannotBeConfirmedMoreThanOnceRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserRegistrationCannotBeConfirmedMoreThanOnceRule(UserRegistrationStatus actualRegistrationStatus)
    {
        _actualRegistrationStatus = actualRegistrationStatus;
    }

    public string Message => "User Registration cannot be confirmed more than once";

    public bool IsBroken()
    {
        return _actualRegistrationStatus == UserRegistrationStatus.Confirmed;
    }
}
