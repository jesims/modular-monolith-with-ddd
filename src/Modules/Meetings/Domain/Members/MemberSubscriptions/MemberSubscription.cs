using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions.Events;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions;

public class MemberSubscription : Entity, IAggregateRoot
{
    private DateTime _expirationDate;

    private MemberSubscription()
    {
        // Only for EF.
    }

    private MemberSubscription(MemberId memberId, DateTime expirationDate)
    {
        Id = new MemberSubscriptionId(memberId.Value);
        _expirationDate = expirationDate;

        AddDomainEvent(new MemberSubscriptionExpirationDateChangedDomainEvent(memberId, _expirationDate));
    }

    public MemberSubscriptionId Id { get; }

    public static MemberSubscription CreateForMember(MemberId memberId, DateTime expirationDate)
    {
        return new MemberSubscription(memberId, expirationDate);
    }

    public void ChangeExpirationDate(DateTime expirationDate)
    {
        _expirationDate = expirationDate;

        AddDomainEvent(new MemberSubscriptionExpirationDateChangedDomainEvent(
            new MemberId(Id.Value),
            _expirationDate));
    }
}
