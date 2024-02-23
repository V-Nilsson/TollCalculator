namespace TollCalculator.TollFreeDates;
public class FirstOfMay : TollFreeDate
{
    public override DateOnly Date(int year) => new(year, 5, 1);
}
