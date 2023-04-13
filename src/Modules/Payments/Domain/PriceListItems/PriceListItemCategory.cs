using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;

public class PriceListItemCategory : ValueObject
{
    private PriceListItemCategory(string code)
    {
        Code = code;
    }

    public static PriceListItemCategory New => new(nameof(New));

    public static PriceListItemCategory Renewal => new(nameof(Renewal));

    public string Code { get; }

    public static PriceListItemCategory Of(string code)
    {
        return new PriceListItemCategory(code);
    }
}
