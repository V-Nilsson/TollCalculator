namespace TollCalculator.TollFreeDates;
public class EasterMonday : TollFreeDate
{
    public override DateOnly Date(int year) => new EasterDay().Date(year).AddDays(1);
}
