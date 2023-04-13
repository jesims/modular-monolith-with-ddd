using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork.Rules;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

public class MoneyValue : ValueObject
{
    private MoneyValue(decimal value, string currency)
    {
        Value = value;
        Currency = currency;
    }

    public decimal Value { get; }

    public string Currency { get; }

    public static MoneyValue Of(decimal value, string currency)
    {
        CheckRule(new ValueOfMoneyMustNotBeNegativeRule(value));

        return new MoneyValue(value, currency);
    }

    public static bool operator >(decimal left, MoneyValue right)
    {
        return left > right.Value;
    }

    public static bool operator <(decimal left, MoneyValue right)
    {
        return left < right.Value;
    }

    public static bool operator >=(decimal left, MoneyValue right)
    {
        return left >= right.Value;
    }

    public static bool operator <=(decimal left, MoneyValue right)
    {
        return left <= right.Value;
    }

    public static bool operator >(MoneyValue left, decimal right)
    {
        return left.Value > right;
    }

    public static bool operator <(MoneyValue left, decimal right)
    {
        return left.Value < right;
    }

    public static bool operator >=(MoneyValue left, decimal right)
    {
        return left.Value >= right;
    }

    public static bool operator <=(MoneyValue left, decimal right)
    {
        return left.Value <= right;
    }

    public static MoneyValue operator -(MoneyValue left, MoneyValue right)
    {
        CheckRule(new MoneyMustHaveTheSameCurrencyRule(left, right));

        return Of(left.Value - right.Value, left.Currency);
    }
}
