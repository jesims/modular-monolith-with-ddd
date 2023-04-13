using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;

public class SubscriptionPaymentStatus : ValueObject
{
    private SubscriptionPaymentStatus(string code)
    {
        Code = code;
    }

    public static SubscriptionPaymentStatus WaitingForPayment => new(nameof(WaitingForPayment));

    public static SubscriptionPaymentStatus Paid => new(nameof(Paid));

    public static SubscriptionPaymentStatus Expired => new(nameof(Expired));

    public string Code { get; }

    public static SubscriptionPaymentStatus Of(string code)
    {
        return new SubscriptionPaymentStatus(code);
    }
}
