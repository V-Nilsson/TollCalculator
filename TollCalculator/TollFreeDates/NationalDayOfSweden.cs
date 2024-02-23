namespace TollCalculator.TollFreeDates;
public class NationalDayOfSweden : TollFreeDate
{
    public override DateOnly Date(int year) => new(year, 6, 6);
}
