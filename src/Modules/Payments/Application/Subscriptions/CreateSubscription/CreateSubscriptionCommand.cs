using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.CreateSubscription;

public class CreateSubscriptionCommand : InternalCommandBase<Guid>
{
    [JsonConstructor]
    public CreateSubscriptionCommand(
        Guid id,
        Guid subscriptionPaymentId)
        : base(id)
    {
        SubscriptionPaymentId = subscriptionPaymentId;
    }

    public Guid SubscriptionPaymentId { get; }
}
