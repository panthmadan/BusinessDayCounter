using BusinessDayCounter.Model;

namespace BusinessDayCounter.Interface
{
   public interface IBusinessDayCalculator
   {
      /// <summary>
      /// Interface to calculate number of weekdays between given two dates excluding Saturday and Sunday.
      /// </summary>
      /// <param name="firstDate">Start Date</param>
      /// <param name="secondDate">End Date</param>
      /// <returns>Number of Weekdays</returns>
      int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate);

      /// <summary>
      /// Interface to calculate number of weekdays between given two dates excluding Saturday and Sunday and Public Holidays.
      /// </summary>
      /// <param name="firstDate">Start Date</param>
      /// <param name="secondDate">End Date</param>
      /// <param name="publicHolidays">Public Holiday Dates</param>
      /// <returns></returns>
      int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime>? publicHolidays = null);

      /// <summary>
      /// Interface to calculate number of weekdays between given two dates excluding Saturday and Sunday and Public Holidays.
      /// </summary>
      /// <param name="firstDate">Start Date</param>
      /// <param name="secondDate">End Date</param>
      /// <param name="publicRuleHolidays"> Public Holiday Rules</param>
      /// <returns>Number of Business days</returns>
      int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHolidayWithRule>?
      publicRuleHolidays = null);

   }
}
