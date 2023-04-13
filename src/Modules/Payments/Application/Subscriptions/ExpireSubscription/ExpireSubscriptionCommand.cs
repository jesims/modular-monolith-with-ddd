using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscription;

public class ExpireSubscriptionCommand : InternalCommandBase
{
    [JsonConstructor]
    public ExpireSubscriptionCommand(
        Guid id,
        Guid subscriptionId)
        : base(id)
    {
        SubscriptionId = subscriptionId;
    }

    public Guid SubscriptionId { get; }
}
