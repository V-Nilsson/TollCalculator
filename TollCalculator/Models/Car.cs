using TollFeeCalculator;

namespace TollCalculator.Models
{
    public class Car : Vehicle
    {
        public TollFreeVehicles? TollFreeVehicle { get; set; }

        public bool IsTollFree => TollFreeVehicle != null;
    }
}