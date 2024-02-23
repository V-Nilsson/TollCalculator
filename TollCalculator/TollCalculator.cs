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

        const int MonthOfJuly = 7;
        public int GetTollFee(Vehicle vehicle, DateTime[] passages)
        {
            if (vehicle.IsTollFree) return 0;

            if (IsTollFreeDate(passages.First().Date)) return 0;

            DateTime intervalStart = passages[0];
            int totalFee = 0;

            foreach (DateTime date in passages)
            {
                int nextFee = GetTollFee(date);
                int tempFee = GetTollFee(intervalStart);

                long diffInMillies = date.Millisecond - intervalStart.Millisecond;
                long minutes = diffInMillies / 1000 / 60;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }
            if (totalFee > 60) totalFee = 60;
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