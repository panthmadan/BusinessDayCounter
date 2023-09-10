using BusinessDayCounter.Interface;
using BusinessDayCounter.Model;
using System.ComponentModel;

namespace BusinessDayCounter.BusinessLayer
{
   /// <summary>
   /// Implementing Intefrace to calculate different types of days between provided start date and end date.
   /// </summary>
   public class BusinessDayCalculator : IBusinessDayCalculator
   {
      private List<DayOfWeek> weekends;
      public BusinessDayCalculator()
      {
         // Declaring the days of week which should not be included in counting Weekdays and Business Days.
         weekends = new List<DayOfWeek>() { DayOfWeek.Saturday, DayOfWeek.Sunday };
      }

      /// <summary>
      /// Method to get number of weekdays between two given dates.
      /// </summary>
      /// <param name="firstDate">Start Date</param>
      /// <param name="secondDate">End Date</param>
      /// <returns>Number Of Days</returns>
      public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
      {
         int weekdays = GetDaysBetweenTwoDates(firstDate, secondDate);
         return weekdays;
      }

      /// <summary>
      /// Method to get number of business days between two given dates. 
      /// </summary>
      /// <param name="firstDate">Start Date</param>
      /// <param name="secondDate">End Date</param>
      /// <param name="publicHolidays">List of Public Holiday Dates</param>
      /// <returns>Number Of Days</returns>
      public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime>?
      publicHolidays)
      {
         int weekdays = GetDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);
         return weekdays;
      }

      /// <summary>
      /// Method to get number of business days between two given dates. 
      /// </summary>
      /// <param name="firstDate">Start Date</param>
      /// <param name="secondDate">End Date</param>
      /// <param name="publicRuleHolidays">List of Public Holidays with Rules</param>
      /// <returns>Number Of Days</returns>
      public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHolidayWithRule>?
      publicRuleHolidays)
      {
         List<DateTime> publicHolidays = GetAllPublicHolidayFromRules(firstDate, secondDate, publicRuleHolidays);
         int weekdays = GetDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);
         return weekdays;
      }



      /// <summary>
      /// Internal method to get number of business days between two given dates. 
      /// </summary>
      /// <param name="firstDate">Start Date</param>
      /// <param name="secondDate">End Date</param>
      /// <param name="publicHolidays">List of Public Holiday Dates</param>
      /// <returns>Number Of Days</returns>
      private int GetDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime>?
    publicHolidays = null)
      {
         int weekDaysCounter = 0;
         try
         {
            if (secondDate <= firstDate)
            {
               return weekDaysCounter;
            }

            DateTime startDate = firstDate.Date;
            DateTime endDate = secondDate.Date;
            DateTime iterationDate = startDate.AddDays(1);
            List<DateTime> publicHolidayWeekdays = new List<DateTime>();

            while (iterationDate != endDate)
            {
               if (!weekends.Contains(iterationDate.DayOfWeek))
               {
                  weekDaysCounter += 1;
               }
               iterationDate = iterationDate.AddDays(1);
            }

            if (publicHolidays != null && publicHolidays.Count > 0)
            {
               publicHolidays = publicHolidays.OrderBy(x => x.Date).Distinct().ToList();

               publicHolidayWeekdays = publicHolidays.Where(holiday => holiday > startDate && holiday < endDate).Distinct().ToList();
               if (publicHolidayWeekdays.Count > 0)
               {
                  publicHolidayWeekdays = publicHolidayWeekdays.Where(holiday => !weekends.Contains(holiday.DayOfWeek)).ToList();
               }
            }

            if (weekDaysCounter > 0 && publicHolidayWeekdays.Count > 0)
            {
               weekDaysCounter = weekDaysCounter - publicHolidayWeekdays.Count;
            }
         }
         catch (Exception exception)
         {
            Console.WriteLine("Invalid Operation : Error - " + exception.Message);
         }
         return weekDaysCounter;
      }

      /// <summary>
      /// Internal method to get number of business days between two given dates. 
      /// </summary>
      /// <param name="firstDate">Start Date</param>
      /// <param name="secondDate">End Date</param>
      /// <param name="publicRuleHolidays">List of Public Holidays with Rules</param>
      /// <returns>Number Of Days</returns>
      private List<DateTime> GetAllPublicHolidayFromRules(DateTime firstDate, DateTime secondDate, IList<PublicHolidayWithRule>
      publicRuleHolidays)
      {
         int startDateYear = firstDate.Year;
         int endDateYear = secondDate.Year;

         List<DateTime> listRuledPublicHolidays = new List<DateTime>();
         if (publicRuleHolidays.Count > 0)
         {
            foreach (PublicHolidayWithRule rule in publicRuleHolidays)
            {
               // Fetch the relevant holiday dates as per the configured rule of the public holiday.
               switch (rule.HolidayRuleType)
               {
                  case HolidayRuleType.FixedDay:
                     listRuledPublicHolidays.AddRange(GetHolidayDates_FixedDay(rule));
                     break;

                  case HolidayRuleType.SameDay_EveryYear:
                     listRuledPublicHolidays.AddRange(GetHolidayDates_SameDay_EveryYear(rule, startDateYear, endDateYear));
                     break;

                  case HolidayRuleType.CertainDay_EveryYear:
                     listRuledPublicHolidays.AddRange(GetHolidayDates_CertainDay_EveryYear(rule, startDateYear, endDateYear));
                     break;

                  default:
                     break;
               }
            }
         }

         return listRuledPublicHolidays;
      }

      /// <summary>
      /// Internal method to derive all holiday dates for Fixed Holiday Type
      /// </summary>
      /// <param name="rule">Public Holioday rule for the given Holiday</param>
      /// <returns>List of Derived Public Day Dates</returns>
      private List<DateTime> GetHolidayDates_FixedDay(PublicHolidayWithRule rule)
      {
         List<DateTime> holidayDates = new List<DateTime>();
         int year = Convert.ToInt32(rule.Year);
         int month = (int)rule.Month;
         int dayOfMonth = Convert.ToInt32(rule.DayOfMonth);
         DateTime holidayDate = new DateTime(year, month, dayOfMonth).Date;
         holidayDates = GetHolidayWithAdditionalDate(holidayDate, rule.ExtendHolidayIfWeekend);
         return holidayDates;
      }

      /// <summary>
      /// Internal method to derive all holiday dates for the Holiday of type occuring every year on same date.
      /// </summary>
      /// <param name="rule">Public Holioday rule for the given Holiday</param>
      /// <param name="startYear">Year of Start Date</param>
      /// <param name="endYear">Year of End Date</param>
      /// <returns>List of Derived Public Day Dates</returns>
      private List<DateTime> GetHolidayDates_SameDay_EveryYear(PublicHolidayWithRule rule, int startYear, int endYear)
      {
         List<DateTime> holidayDates = new List<DateTime>();

         int dayOfMonth = Convert.ToInt32(rule.DayOfMonth);
         int month = (int)rule.Month;
         int reference_StartYear = startYear;
         int year = reference_StartYear;

         while (reference_StartYear <= endYear)
         {
            year = reference_StartYear;
            DateTime holidayDate = new DateTime(year, month, dayOfMonth);
            holidayDates.AddRange(GetHolidayWithAdditionalDate(holidayDate, rule.ExtendHolidayIfWeekend));

            reference_StartYear += 1;
         }
         return holidayDates;
      }

      /// <summary>
      /// Internal method to derive all holiday dates for the Holiday of type occuring on a certian day  in a month every year.
      /// </summary>
      /// <param name="rule">Public Holioday rule for the given Holiday</param>
      /// <param name="startYear">Year of Start Date</param>
      /// <param name="endYear">Year of End Date</param>
      /// <returns>List of Derived Public Day Dates</returns>
      private List<DateTime> GetHolidayDates_CertainDay_EveryYear(PublicHolidayWithRule rule, int startYear, int endYear)
      {
         List<DateTime> holidayDates = new List<DateTime>();

         int month = (int)rule.Month;
         int weekOfMonth = (int)rule.WeekOfMonth;

         int reference_StartYear = startYear;
         int year = reference_StartYear;

         while (reference_StartYear <= endYear)
         {
            year = reference_StartYear;
            int dayOfMonth = GetDayOfMonth(rule.DayOfWeek, month, rule.WeekOfMonth, year);

            DateTime holidayDate = new DateTime(year, month, dayOfMonth);
            holidayDates.AddRange(GetHolidayWithAdditionalDate(holidayDate, rule.ExtendHolidayIfWeekend));

            reference_StartYear += 1;
         }
         return holidayDates;
      }

      /// <summary>
      /// Check for additional public holiday if the holiday rue is configured to extend the holiday when it falls on a Saturday or Sunday. 
      /// </summary>
      /// <param name="holidayDate">Date of Actual Holiday</param>
      /// <param name="extendHolidayIfWeekend">Property to allow additional public holiday</param>
      /// <returns>List of Derived Public Day Dates</returns>
      private List<DateTime> GetHolidayWithAdditionalDate(DateTime holidayDate, bool extendHolidayIfWeekend)
      {
         List<DateTime> holidayDates = new List<DateTime>() { holidayDate };

         if (extendHolidayIfWeekend)
         {
            if (holidayDate.DayOfWeek == DayOfWeek.Saturday)
            {
               // Saturday to Monday is 2 days
               holidayDate = holidayDate.AddDays(2).Date;
               holidayDates.Add(holidayDate);
            }

            else if (holidayDate.DayOfWeek == DayOfWeek.Sunday)
            {
               // Sunday to Monday is 1 day
               holidayDate = holidayDate.AddDays(1).Date;
               holidayDates.Add(holidayDate);
            }
         }
         return holidayDates;
      }

      /// <summary>
      /// Dervie the holiday date for the Holiday of type occuring on a certian day in a month every year.
      /// </summary>
      /// <param name="dayOfWeek">Day of week (Mon...Wed..Sun)</param>
      /// <param name="month">Month number of the year (Jan...July..Dec)</param>
      /// <param name="weekOfMonth">Week number of the month (First...Third..Last)</param>
      /// <param name="year">Year</param>
      /// <returns>List of Derived Public Day Dates</returns>
      private int GetDayOfMonth(DayOfWeek dayOfWeek, int month, WeekOfMonth weekOfMonth, int year)
      {
         DateTime firstDayOfMonth = new DateTime(year, month, 1);
         DateTime endDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
         DateTime dateForRequiredWeek = new DateTime();
         DateTime refrenceDate = firstDayOfMonth;
         int weekCount = 0;
         int daysInWeek = 7;

         // Setting the date to reach to the required day of the first week
         while (refrenceDate.DayOfWeek != dayOfWeek)
         {
            refrenceDate = refrenceDate.AddDays(1);
            dateForRequiredWeek = refrenceDate;
         }

         switch (weekOfMonth)
         {
            // Add required days to the date as per the week number
            case WeekOfMonth.Second:
               weekCount = (int)WeekOfMonth.Second - 1;
               dateForRequiredWeek = dateForRequiredWeek.AddDays(weekCount * daysInWeek);
               break;

            case WeekOfMonth.Third:
               weekCount = (int)WeekOfMonth.Third - 1;
               dateForRequiredWeek = dateForRequiredWeek.AddDays(weekCount * daysInWeek);
               break;

            case WeekOfMonth.Fourth:
               weekCount = (int)WeekOfMonth.Fourth - 1;
               dateForRequiredWeek = dateForRequiredWeek.AddDays(weekCount * daysInWeek);
               break;

            case WeekOfMonth.Last:
               weekCount = (int)WeekOfMonth.Last - 1;
               bool dateOutOfRange = dateForRequiredWeek.AddDays(weekCount * daysInWeek) > endDayOfMonth;
               if (dateOutOfRange)
               {
                  // Fetching 4th Week if date out range from the given month
                  weekCount -= 1;
               };
               dateForRequiredWeek = dateForRequiredWeek.AddDays(weekCount * daysInWeek);
               break;

            case WeekOfMonth.First:
            default:
               break;
         }

         return dateForRequiredWeek.Day;
      }

   }
}
