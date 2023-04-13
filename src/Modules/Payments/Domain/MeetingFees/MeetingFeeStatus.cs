using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees;

public class MeetingFeeStatus : ValueObject
{
    private MeetingFeeStatus(string code)
    {
        Code = code;
    }

    public static MeetingFeeStatus WaitingForPayment => new(nameof(WaitingForPayment));

    public static MeetingFeeStatus Paid => new(nameof(Paid));

    public static MeetingFeeStatus Expired => new(nameof(Expired));

    public static MeetingFeeStatus Canceled => new(nameof(Canceled));

    public string Code { get; }

    public static MeetingFeeStatus Of(string code)
    {
        return new MeetingFeeStatus(code);
    }
}
