namespace TollCalculator.TollFreeDates;
public class BoxingDay : TollFreeDate
{
    public override DateOnly Date(int year) => new(year, 12, 26);
}
