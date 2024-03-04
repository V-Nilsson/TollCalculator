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
        public int GetDailyTollFee(Vehicle vehicle, DateTime[] passages)
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
                // Plocka ut passager som skett inom en timme, för att sedan lägga till den med högst värde till totalen.
                var passagesWithinHour = orderedPassages.Skip(i).Where(t => t - orderedPassages[i] < TimeSpan.FromHours(1)).ToArray();
                totalFee += passagesWithinHour.Select(GetTollFee).Max();

                // Öka räknaren med antalet passager som skedde inom en timme så att dessa inte tas betalt för flera gånger.
                counter += passagesWithinHour.Length;
            }
            if (totalFee > MaxFee) totalFee = MaxFee;
            return totalFee;
        }

        // Tid och avgift har brutits ut till eget Record, så att vi kan jämföra en passagetid med en lista av TollFees.
        // Vid förändringar av avgifter eller tider behöver vi enbart ändra i det nya recordet
        // Kontrollen av tollfreevehicle har flyttats så att GetTollFee enbart gör en sak. 
        private int GetTollFee(DateTime date)
        {
            return TollFee.GetTollFees().SingleOrDefault(t => TimeOnly.FromDateTime(date).IsBetween(t.Start, t.End))?.Fee ?? 0;
        }

        // Gratis dagar utbrutna till egna klasser för att göra koden mer läsbar samt att förenkla vid förändringar, som tex tillägg av ny helgdag.
        // Tidigare var de dessutom beroende av ett specifikt år och datum, vilket nu är borta.
        private bool IsTollFreeDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            return TollFreeDates.TollFreeDate.GetDates(DateTime.Now.Year).Contains(DateOnly.FromDateTime(date)) ||
                   date.Month == MonthOfJuly;
        }
    }
}