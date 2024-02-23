namespace TollCalculator.TollFreeDates;
public class GoodFriday : TollFreeDate
{
    public override DateOnly Date(int year) => new EasterDay().Date(year).AddDays(-2);
}
