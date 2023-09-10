using BusinessDayCounter.BusinessLayer;
using BusinessDayCounter.Data;
using System.Data;

namespace BusinessDayCounter
{
   public class BusinessDayCounter
   {
      static void Main(string[] args)
      {
         //Creating instance of the service
         BusinessDayCalculator holidayService = new BusinessDayCalculator();

         //Declaring Start Date and End Date for the operation
         DateTime firstDate = new DateTime(2013, 10, 07);
         DateTime secondDate = new DateTime(2014, 01, 01);

         //Fetchin Default Public Holidays and Ruled Public Holidays
         PublicHolidayDefault publicHolidays = new PublicHolidayDefault();
         var listPublicHolidays = publicHolidays.GetPublicHolidays().Select(day => day.HolidayDate).ToList();
         var listRuledPublicHolidays = publicHolidays.GetPublicHolidayRules();

         // Calling functions to calculate days.
         int weekDays = holidayService.WeekdaysBetweenTwoDates(firstDate, secondDate);
         int businessDays = holidayService.BusinessDaysBetweenTwoDates(firstDate, secondDate, listPublicHolidays);
         int businessDaysWithRule = holidayService.BusinessDaysBetweenTwoDates(firstDate, secondDate, listRuledPublicHolidays);

         // Logging the response
         Console.WriteLine($"No of Weekdays between : {firstDate.Date.ToLongDateString()} and {secondDate.Date.ToLongDateString()} is : {weekDays} Day(s)");
         Console.WriteLine($"No of Business Days (Using Date Public Holiday) between : {firstDate.Date.ToLongDateString()} and {secondDate.Date.ToLongDateString()} is : {businessDays} Day(s)");
         Console.WriteLine($"No of Business Days (Using Ruled Public Holiday) between : {firstDate.Date.ToLongDateString()} and {secondDate.Date.ToLongDateString()} is : {businessDaysWithRule} Day(s)");
         Console.WriteLine("Press any key to exit.");
         string userInput = Console.ReadLine();
      }
   }

}
