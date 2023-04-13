using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

public class MoneyValue : ValueObject
{
    private MoneyValue(decimal? value, string currency)
    {
        Value = value;
        Currency = currency;
    }

    public static MoneyValue Undefined => new(null, null);

    public decimal? Value { get; }

    public string Currency { get; }

    public static MoneyValue Of(decimal value, string currency)
    {
        return new MoneyValue(value, currency);
    }

    public static MoneyValue operator *(int left, MoneyValue right)
    {
        return new MoneyValue(right.Value * left, right.Currency);
    }
}
