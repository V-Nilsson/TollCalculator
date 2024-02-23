namespace TollCalculator.TollFreeDates;
public class ChristmasEve : TollFreeDate
{
    public override DateOnly Date(int year) => new(year, 12, 24);
}
