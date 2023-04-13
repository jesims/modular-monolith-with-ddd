using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

public class SubscriptionPeriod : ValueObject
{
    private SubscriptionPeriod(string code)
    {
        Code = code;
    }

    public string Code { get; }

    public static SubscriptionPeriod Month => new(nameof(Month));

    public static SubscriptionPeriod HalfYear => new(nameof(HalfYear));

    public static SubscriptionPeriod Of(string code)
    {
        return new SubscriptionPeriod(code);
    }

    public static string GetName(string code)
    {
        return code == Month.Code
            ? "Month"
            : "6 months";
    }

    public int GetMonthsNumber()
    {
        return this == Month ? 1 : 6;
    }
}
