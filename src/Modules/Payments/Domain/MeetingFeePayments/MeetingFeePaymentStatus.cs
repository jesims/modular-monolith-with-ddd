using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments;

public class MeetingFeePaymentStatus : ValueObject
{
    private MeetingFeePaymentStatus(string code)
    {
        Code = code;
    }

    public static MeetingFeePaymentStatus WaitingForPayment => new(nameof(WaitingForPayment));

    public static MeetingFeePaymentStatus Paid => new(nameof(Paid));

    public static MeetingFeePaymentStatus Expired => new(nameof(Expired));

    public string Code { get; }

    public static MeetingFeePaymentStatus Of(string code)
    {
        return new MeetingFeePaymentStatus(code);
    }
}
