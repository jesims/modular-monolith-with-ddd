using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork.Rules;

public class ValueOfMoneyMustNotBeNegativeRule : IBusinessRule
{
    private readonly decimal _value;

    public ValueOfMoneyMustNotBeNegativeRule(decimal value)
    {
        _value = value;
    }

    public string Message => "Value of money must not be negative.";

    public bool IsBroken()
    {
        return _value < 0;
    }
}
