using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork.Rules;

public class MoneyMustHaveTheSameCurrencyRule : IBusinessRule
{
    private readonly MoneyValue _left;

    private readonly MoneyValue _right;

    public MoneyMustHaveTheSameCurrencyRule(MoneyValue left, MoneyValue right)
    {
        _left = left;
        _right = right;
    }

    public string Message => "Currency of money must be the same.";

    public bool IsBroken()
    {
        return _left.Currency != _right.Currency;
    }
}
