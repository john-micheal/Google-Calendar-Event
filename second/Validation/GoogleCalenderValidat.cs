using Microsoft.AspNetCore.Mvc.Formatters;
using Google_Calendar_events_app.DTO;

namespace Google_Calendar_events_app.Validation
{
    public static class GoogleCalenderValidat
    {
        public static string GoogleCalenderValidate(this CreateGoogleEventDto googleCalender)
        {
            if (googleCalender.Start >= googleCalender.End)
            {
                return "Error:  End Date more than Start Date. ";
            }
            if (googleCalender.Start < DateTime.Now || googleCalender.End < DateTime.Now)
            {
                return "Error: Event Date in the past. ";
            }
            if(googleCalender.Start.DayOfWeek == DayOfWeek.Friday || googleCalender.Start.DayOfWeek == DayOfWeek.Saturday)
            {
                return "It's a holiday, please enter Event not being in Saturday or Friday. ";
            }
            return string.Empty; 
        }
    }
}