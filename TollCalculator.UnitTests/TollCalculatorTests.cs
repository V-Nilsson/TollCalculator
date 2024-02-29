using TollCalculator.Models;

namespace TollCalculator.UnitTests;
public class TollCalculatorTests
{
    private readonly TollCalculator subject;

    public TollCalculatorTests()
    {
        subject = new TollCalculator();
    }

    [Fact]
    public void GetDailyTollFee_NoPassages_ReturnsZero()
    {
        var car = new Car();
        var passages = Array.Empty<DateTime>();
        var result = subject.GetDailyTollFee(car, passages);
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetDailyTollFee_TollFreeVehicle_ReturnsZero()
    {
        var motorbike = new Motorbike();
        var passages = new[]
        {
            DateTime.Parse("2023-11-02 15:20:05")
        };
        var result = subject.GetDailyTollFee(motorbike, passages);
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetDailyTollFee_OnTollFreeDay_ReturnsZero()
    {
        var car = new Car();
        var passages = new[]
        {
            DateTime.Parse("2023-12-24 15:20:05")
        };
        var result = subject.GetDailyTollFee(car, passages);
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetDailyTollFee_DifferentDates_ThrowsArgumentException()
    {
        var car = new Car();
        var passages = new[]
        {
            DateTime.Parse("2023-11-24 15:20:05"),
            DateTime.Parse("2023-11-25 18:10:05"),
            DateTime.Parse("2023-11-25 14:10:05")
        };

        Assert.Throws<ArgumentException>(() => subject.GetDailyTollFee(car, passages));
    }

    [Fact]
    public void GetDailyTollFee_WithCar_ReturnsCorrectFee()
    {
        var car = new Car();
        var passages = new[]
        {
            DateTime.Parse("2023-10-12 15:20:05"),
            DateTime.Parse("2023-10-12 15:25:05"),
            DateTime.Parse("2023-10-12 15:30:05"),
            DateTime.Parse("2023-10-12 17:20:45"),
            DateTime.Parse("2023-10-12 17:22:10"),
            DateTime.Parse("2023-10-12 18:21:05"),
            DateTime.Parse("2023-10-12 18:40:05"),
        };
        var result = subject.GetDailyTollFee(car, passages);
        Assert.Equal(39, result);
    }

    [Fact]
    public void GetDailyTollFee_TollFreeType_ReturnsZero()
    {
        var car = new Car() { TollFreeVehicle = TollFreeVehicles.Diplomat};
        var passages = new[]
        {
            DateTime.Parse("2023-10-12 15:20:05"),
        };
        var result = subject.GetDailyTollFee(car, passages);
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetDailyTollFee_TotalAmountExceedsMax_ReturnsMax()
    {
        var car = new Car();
        var passages = new[]
        {
            DateTime.Parse("2023-10-12 06:40:05"),
            DateTime.Parse("2023-10-12 08:25:05"),
            DateTime.Parse("2023-10-12 10:30:05"),
            DateTime.Parse("2023-10-12 15:30:05"),
            DateTime.Parse("2023-10-12 17:22:10"),
            DateTime.Parse("2023-10-12 18:21:05"),
            DateTime.Parse("2023-10-12 18:40:05"),
        };
        var result = subject.GetDailyTollFee(car, passages);
        Assert.Equal(60, result);
    }
}
