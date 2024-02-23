namespace TollCalculator.TollFreeDates;
public abstract class TollFreeDate
{
    public abstract DateOnly Date(int year);

    public static IEnumerable<DateOnly> GetDates(int year) => new[]
    {
        new NewYearsDay().Date(year),
        new EpiphanyEve().Date(year),
        new GoodFriday().Date(year),
        new EasterDay().Date(year),
        new EasterMonday().Date(year),
        new AscensionDay().Date(year),
        new FirstOfMay().Date(year),
        new NationalDayOfSweden().Date(year),
        new ChristmasEve().Date(year),
        new ChristmasDay().Date(year),
        new BoxingDay().Date(year),
    };
}
