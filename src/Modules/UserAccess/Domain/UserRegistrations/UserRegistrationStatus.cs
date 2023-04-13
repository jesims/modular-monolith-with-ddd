using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;

public class UserRegistrationStatus : ValueObject
{
    private UserRegistrationStatus(string value)
    {
        Value = value;
    }

    public static UserRegistrationStatus WaitingForConfirmation => new(nameof(WaitingForConfirmation));

    public static UserRegistrationStatus Confirmed => new(nameof(Confirmed));

    public static UserRegistrationStatus Expired => new(nameof(Expired));

    public string Value { get; }
}
