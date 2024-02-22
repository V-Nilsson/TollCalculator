namespace TollCalculator;
public record TollFee(TimeOnly Start, TimeOnly End, int Fee)
{
    public static IEnumerable<TollFee> GetTollFees() => new[]
    {
        new TollFee(new TimeOnly(6,00), new TimeOnly(6,29), 8),
        new TollFee(new TimeOnly(6,30), new TimeOnly(6,59), 13),
        new TollFee(new TimeOnly(7,00), new TimeOnly(7,59), 18),
        new TollFee(new TimeOnly(8,00), new TimeOnly(8,29), 13),
        new TollFee(new TimeOnly(8,30), new TimeOnly(14,59), 8),
        new TollFee(new TimeOnly(15,00), new TimeOnly(15,29), 13),
        new TollFee(new TimeOnly(15,30), new TimeOnly(16,59), 18),
        new TollFee(new TimeOnly(17,00), new TimeOnly(17,59), 13),
        new TollFee(new TimeOnly(18,00), new TimeOnly(18,29), 8)
    };
}
