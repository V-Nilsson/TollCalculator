namespace TollCalculator.TollFreeDates;
public class NewYearsDay : TollFreeDate
{
    public override DateOnly Date(int year) => new(year, 1, 1);
}
