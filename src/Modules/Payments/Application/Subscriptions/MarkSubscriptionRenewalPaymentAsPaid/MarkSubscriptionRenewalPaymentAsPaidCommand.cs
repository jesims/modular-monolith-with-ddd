using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionRenewalPaymentAsPaid;

public class MarkSubscriptionRenewalPaymentAsPaidCommand : CommandBase
{
    public MarkSubscriptionRenewalPaymentAsPaidCommand(Guid subscriptionRenewalPaymentId)
    {
        SubscriptionRenewalPaymentId = subscriptionRenewalPaymentId;
    }

    public Guid SubscriptionRenewalPaymentId { get; }
}
