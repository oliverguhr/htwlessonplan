using HtwLessonPlan.Model;
using System.Collections.Generic;
using System.Text;

namespace HtwLessonPlan
{
    internal class Ical
    {
        internal static string Generate(List<CalendarEvent> events)
        {
            // Create new StreamWriter to write iCalendar file.
            StringBuilder sw = new StringBuilder();
            // Write the opening line of iCalendar.
            sw.AppendLine("BEGIN:VCALENDAR");
            // Write the version (2 required for OS X iCal).
            sw.AppendLine("VERSION:2.0");
            // Loop through rows in data source to write each event.
            foreach (var calendarEvent in events)
            {
                // Create string to format start time properly.
                string strStart = calendarEvent.Start.ToUniversalTime().ToString("yyyyMMdd'T'HHmmss");
                // Create string to format end time properly.
                string strEnd = calendarEvent.End.ToUniversalTime().ToString("yyyyMMdd'T'HHmmss");
                // Create a string for the event title.
                string strSummary = calendarEvent.Title;
                // Create a string for the event description.
                string strDesctiption = calendarEvent.Room;
                // Write the event start.
                sw.AppendLine("BEGIN:VEVENT");
                // Write the event summary.
                sw.AppendLine("SUMMARY:" + strSummary);
                // Write the event description. (URL added for display in description.)
                sw.AppendLine("DESCRIPTION:" + strDesctiption);
                // Write the start time of the event. The Z is for UTC.
                sw.AppendLine("DTSTART:" + strStart + "Z");
                // Write the end time of the event. The Z is for UTC.
                sw.AppendLine("DTEND:" + strEnd + "Z");
                // Write the event end.
                sw.AppendLine("END:VEVENT");
            }
            // Write the end line of iCalendar.
            sw.AppendLine("END:VCALENDAR");
            // Close the StreamWriter.
            return sw.ToString();
        }
    }
}