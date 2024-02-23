namespace TollCalculator.TollFreeDates;
public class ChristmasDay : TollFreeDate
{
    public override DateOnly Date(int year) => new(year, 12, 25);
}
