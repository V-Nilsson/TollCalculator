namespace TollCalculator.TollFreeDates;
public class EpiphanyEve : TollFreeDate
{
    public override DateOnly Date(int year) => new(year, 1, 6);
}
