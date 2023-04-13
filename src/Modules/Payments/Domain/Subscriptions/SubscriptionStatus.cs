using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

public class SubscriptionStatus : ValueObject
{
    private SubscriptionStatus(string code)
    {
        Code = code;
    }

    public static SubscriptionStatus Active => new(nameof(Active));

    public static SubscriptionStatus Expired => new(nameof(Expired));

    public string Code { get; }

    public static SubscriptionStatus Of(string code)
    {
        return new SubscriptionStatus(code);
    }
}
