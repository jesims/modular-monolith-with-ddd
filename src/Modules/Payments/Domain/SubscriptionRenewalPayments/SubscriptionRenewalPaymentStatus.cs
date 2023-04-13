namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments;

public class SubscriptionRenewalPaymentStatus
{
    private SubscriptionRenewalPaymentStatus(string code)
    {
        Code = code;
    }

    public static SubscriptionRenewalPaymentStatus WaitingForPayment => new(nameof(WaitingForPayment));

    public static SubscriptionRenewalPaymentStatus Paid => new(nameof(Paid));

    public static SubscriptionRenewalPaymentStatus Expired => new(nameof(Expired));

    public string Code { get; }

    public static SubscriptionRenewalPaymentStatus Of(string code)
    {
        return new SubscriptionRenewalPaymentStatus(code);
    }
}
