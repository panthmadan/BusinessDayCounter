namespace BusinessDayCounter.Model
{

   /// <summary>
   /// Set the Public Holiday using holiday date
   /// </summary>
   public class PublicHoliday
   {
      public DateTime HolidayDate { get; set; }
   }

   /// <summary>
   /// Set the Public Holiday using holiday rule
   /// </summary>
   public class PublicHolidayWithRule
   {
      public string HolidayName { get; set; }

      public bool ExtendHolidayIfWeekend { get; set; }

      public HolidayOccurenceType HolidayOccurenceType { get; set; }

      public HolidayRuleType HolidayRuleType { get; set; }

      public int? DayOfMonth { get; set; }

      public Month Month { get; set; }

      public int? Year { get; set; }

      public DayOfWeek DayOfWeek { get; set; }

      public WeekOfMonth WeekOfMonth { get; set; }

   }

}
