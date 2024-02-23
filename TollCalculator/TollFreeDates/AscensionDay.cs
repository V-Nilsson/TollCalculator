namespace TollCalculator.TollFreeDates;
public class AscensionDay : TollFreeDate
{
    public override DateOnly Date(int year) => new EasterDay().Date(year).AddDays(39);
}
