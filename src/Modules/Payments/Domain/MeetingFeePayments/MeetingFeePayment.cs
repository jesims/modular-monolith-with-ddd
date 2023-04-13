using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments;

public class MeetingFeePayment : AggregateRoot
{
    private MeetingFeeId _meetingFeeId;

    private MeetingFeePaymentStatus _status;

    public static MeetingFeePayment Create(
        MeetingFeeId meetingFeeId)
    {
        var meetingFeePayment = new MeetingFeePayment();

        var meetingFeePaymentCreated = new MeetingFeePaymentCreatedDomainEvent(
            Guid.NewGuid(),
            meetingFeeId.Value,
            MeetingFeePaymentStatus.WaitingForPayment.Code);

        meetingFeePayment.Apply(meetingFeePaymentCreated);
        meetingFeePayment.AddDomainEvent(meetingFeePaymentCreated);

        return meetingFeePayment;
    }

    public void Expire()
    {
        var @event =
            new MeetingFeePaymentPaidDomainEvent(
                Id,
                MeetingFeePaymentStatus.Expired.Code);

        Apply(@event);
        AddDomainEvent(@event);
    }

    public void MarkAsPaid()
    {
        var @event =
            new MeetingFeePaymentPaidDomainEvent(
                Id,
                MeetingFeePaymentStatus.Paid.Code);

        Apply(@event);
        AddDomainEvent(@event);
    }

    public MeetingFeePaymentSnapshot GetSnapshot()
    {
        return new MeetingFeePaymentSnapshot(Id, _meetingFeeId.Value);
    }

    protected override void Apply(IDomainEvent @event)
    {
        this.When((dynamic)@event);
    }

    private void When(MeetingFeePaymentCreatedDomainEvent @event)
    {
        Id = @event.MeetingFeePaymentId;
        _meetingFeeId = new MeetingFeeId(@event.MeetingFeeId);
        _status = MeetingFeePaymentStatus.Of(@event.Status);
    }

    private void When(MeetingFeePaymentExpiredDomainEvent @event)
    {
        _status = MeetingFeePaymentStatus.Of(@event.Status);
    }

    private void When(MeetingFeePaymentPaidDomainEvent @event)
    {
        _status = MeetingFeePaymentStatus.Of(@event.Status);
    }
}
