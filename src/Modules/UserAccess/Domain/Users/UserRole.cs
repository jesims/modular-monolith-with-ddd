using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.Users;

public class UserRole : ValueObject
{
    private UserRole(string value)
    {
        Value = value;
    }

    public static UserRole Member => new(nameof(Member));

    public static UserRole Administrator => new(nameof(Administrator));

    public string Value { get; }
}
