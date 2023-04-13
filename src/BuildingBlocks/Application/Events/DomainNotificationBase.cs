using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.BuildingBlocks.Application.Events;

public class DomainNotificationBase<T> : IDomainEventNotification<T>
    where T : IDomainEvent
{
    public DomainNotificationBase(T domainEvent, Guid id)
    {
        Id = id;
        DomainEvent = domainEvent;
    }

    public T DomainEvent { get; }

    public Guid Id { get; }
}
