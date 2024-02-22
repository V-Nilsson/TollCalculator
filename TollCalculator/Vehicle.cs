namespace TollFeeCalculator
{
    public interface Vehicle
    {
        public bool IsTollFree { get; }
        String GetVehicleType();
    }
}