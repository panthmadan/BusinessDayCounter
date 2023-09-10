using BusinessDayCounter.Model;

namespace BusinessDayCounter.Data
{
   public class PublicHolidayDefault
   {
      /// <summary>
      /// Fetch List of manually added Public Holidays which has been set with DateTime
      /// </summary>
      /// <returns>List of Public Holidays</returns>
      public List<PublicHoliday> GetPublicHolidays()
      {
         List<PublicHoliday> publicHolidays = new List<PublicHoliday>()
         {
            new PublicHoliday(){ HolidayDate = new DateTime(2013,12,25).Date},
            new PublicHoliday(){ HolidayDate = new DateTime(2013,12,26).Date},
            new PublicHoliday(){ HolidayDate = new DateTime(2014,01,01).Date},
         };

         return publicHolidays;
      }

      /// <summary>
      /// Fetch List of manually added Public Holidays Rules which has been set using rules
      /// </summary>
      /// <returns>List of Public Holidays Rules</returns>
      public List<PublicHolidayWithRule> GetPublicHolidayRules()
      {
         List<PublicHolidayWithRule> publicHolidays = new List<PublicHolidayWithRule>();
         PublicHolidayWithRule publicHoliday = new PublicHolidayWithRule();

         //Adding Anzac Day - 25th April : Every Year
         publicHoliday = new PublicHolidayWithRule()
         {
            HolidayName = "Anzac Day",
            ExtendHolidayIfWeekend = false,
            HolidayOccurenceType = HolidayOccurenceType.RepeatEveryYear,
            HolidayRuleType = HolidayRuleType.SameDay_EveryYear,
            DayOfMonth = 25,
            Month = Month.April
         };

         publicHolidays.Add(publicHoliday);

         //Adding New Years Day - 1st January : Every Year with Additional Holidays
         publicHoliday = new PublicHolidayWithRule()
         {
            HolidayName = "New Years Day",
            ExtendHolidayIfWeekend = true,
            HolidayOccurenceType = HolidayOccurenceType.RepeatEveryYear,
            HolidayRuleType = HolidayRuleType.SameDay_EveryYear,
            DayOfMonth = 1,
            Month = Month.January
         };

         publicHolidays.Add(publicHoliday);

         //Adding Queens Birthday - Second Monday of June Every Year
         publicHoliday = new PublicHolidayWithRule()
         {
            HolidayName = "Queens Birthday",
            ExtendHolidayIfWeekend = false,
            HolidayOccurenceType = HolidayOccurenceType.RepeatEveryYear,
            HolidayRuleType = HolidayRuleType.CertainDay_EveryYear,
            DayOfWeek = DayOfWeek.Tuesday,
            WeekOfMonth = WeekOfMonth.Fourth,
            Month = Month.January
         };

         publicHolidays.Add(publicHoliday);

         //Adding Easter Monday - 1st April 2024 : Once
         publicHoliday = new PublicHolidayWithRule()
         {
            HolidayName = "Easter Monday",
            ExtendHolidayIfWeekend = false,
            HolidayOccurenceType = HolidayOccurenceType.Once,
            HolidayRuleType = HolidayRuleType.FixedDay,
            DayOfMonth = 01,
            Month = Month.April,
            Year = 2024
         };

         publicHolidays.Add(publicHoliday);

         //Adding Christmas Day - 25th December : Every Year with Additional Holidays
         publicHoliday = new PublicHolidayWithRule()
         {
            HolidayName = "Christmas Day",
            ExtendHolidayIfWeekend = true,
            HolidayOccurenceType = HolidayOccurenceType.RepeatEveryYear,
            HolidayRuleType = HolidayRuleType.SameDay_EveryYear,
            DayOfMonth = 25,
            Month = Month.December
         };

         publicHolidays.Add(publicHoliday);


         return publicHolidays;
      }
   }
}
