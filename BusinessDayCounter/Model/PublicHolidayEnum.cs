namespace BusinessDayCounter.Model
{
   public class PublicHolidayEnum
   {
   }

   //
   // Summary:
   //     Specifies the rule type of the holiday
   public enum HolidayRuleType
   {
      //
      // Summary:
      //     Used for holidays having fixed day of a month of a year. (Date, Month and Year needed)
      FixedDay,
      //
      // Summary:
      //     Used for holidays for same day of a month every year. (Date and Month needed)
      SameDay_EveryYear,
      //
      // Summary:
      //     Same day of n-th Week of a month every year. (Day Of Week, Week number of Month and Month needed)
      CertainDay_EveryYear
   }

   //
   // Summary:
   //     Specifies the occurence type of the holiday
   public enum HolidayOccurenceType
   {
      //
      // Summary:
      //     Holiday which is set to occur once a year
      Once,
      //
      // Summary:
      //     Holiday which is set to occur every year
      RepeatEveryYear
   }

   //
   // Summary:
   //     Specifies the month names in a Year.
   public enum Month
   {
      //
      // Summary:
      //     January which is first month of a Year
      January = 1,
      //
      // Summary:
      //     February which is second month of a Year
      February = 2,
      //
      // Summary:
      //     March which is third month of a Year
      March = 3,
      //
      // Summary:
      //     April which is fourth month of a Year
      April = 4,
      //
      // Summary:
      //     May which is fifth month of a Year
      May = 5,
      //
      // Summary:
      //     June which is sixth month of a Year
      June = 6,
      //
      // Summary:
      //     July which is seventh month of a Year
      July = 7,
      //
      // Summary:
      //     August which is eighth month of a Year
      August = 8,
      //
      // Summary:
      //     September which is ninth month of a Year
      September = 9,
      //
      // Summary:
      //     October which is tenth month of a Year
      October = 10,
      //
      // Summary:
      //     November which is eleventh month of a Year
      November = 11,
      //
      // Summary:
      //     December which is twelveth month of a Year
      December = 12
   }

   //
   // Summary:
   //     Specifies the week number occurence of a month.
   public enum WeekOfMonth
   {
      //
      // Summary:
      //     First Week of the Month
      First = 1,
      //
      // Summary:
      //     Second Week of the Month
      Second = 2,
      //
      // Summary:
      //     Third Week of the Month
      Third = 3,
      //
      // Summary:
      //     Fourth Week of the Month
      Fourth = 4,
      //
      // Summary:
      //     Last Week of the Month
      Last = 5
   }

}
