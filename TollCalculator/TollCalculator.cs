using TollFeeCalculator;

namespace TollCalculator
{
    public partial class TollCalculator
    {

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */

        const int MaxFee = 60;
        const int MonthOfJuly = 7;
        public int GetTollFee(Vehicle vehicle, DateTime[] passages)
        {
            if (!passages.Any()) return 0;

            if (passages.Any(p => p.Date != passages.First().Date))
            {
                throw new ArgumentException("The passages must be during the same date");
            }

            if (vehicle.IsTollFree) return 0;

            if (IsTollFreeDate(passages.First().Date)) return 0;

            return CalculateTotalFee(passages);
        }

        private int CalculateTotalFee(DateTime[] passages)
        {
            var totalFee = 0;
            var orderedPassages = passages.OrderBy(p => p).ToList();
            var counter = 0;
            for (var i = 0; i < orderedPassages.Count; i = counter)
            {
                var passagesWithinHour = orderedPassages.Skip(i).Where(t => t - orderedPassages[i] < TimeSpan.FromHours(1)).ToArray();
                totalFee += passagesWithinHour.Select(GetTollFee).Max();
                counter += passagesWithinHour.Length;
            }
            if (totalFee > MaxFee) totalFee = MaxFee;
            return totalFee;
        }

        public int GetTollFee(DateTime date)
        {
            return TollFee.GetTollFees().SingleOrDefault(t => TimeOnly.FromDateTime(date).IsBetween(t.Start, t.End))?.Fee ?? 0;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            return TollFreeDates.TollFreeDate.GetDates(DateTime.Now.Year).Contains(DateOnly.FromDateTime(date)) ||
                   date.Month == MonthOfJuly;
        }
    }
}