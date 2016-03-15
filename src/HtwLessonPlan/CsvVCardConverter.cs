using HtwLessonPlan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HtwLessonPlan
{
    public class CsvVCardConverter
    {
        public static List<CalendarEvent> ParseCsv(List<string> times)
        {
            List<CalendarEvent> events = new List<CalendarEvent>();

            foreach (var entry in times)
            {
                //remove empty fields, the hacky way. replace this with a csv parser lib 
                var sanitizedEntry = entry.Replace(",,,,,,", ",").Replace(",,", ",");
                string pattern = @"""\s*,\s*""";
                var row = System.Text.RegularExpressions.Regex.Split(sanitizedEntry.Substring(1, sanitizedEntry.Length - 2), pattern);

                CalendarEvent calendarEvent = new CalendarEvent();
                calendarEvent.Title = row.First();
                calendarEvent.Start = DateTime.Parse(row[1] + " " + row[2]);
                calendarEvent.End = DateTime.Parse(row[3] + " " + row[4]);
                calendarEvent.Room = row[10];

                events.Add(calendarEvent);
            }
            return events;
        }

        [Fact]
        private void ParseValidCsvInput()
        {
            var data = new List<string>() {            
                "\"NM V2,3/IA-IK Z 354\",\"15.03.2016\",\"13:20:00\",\"15.03.2016\",\"14:50:00\",\"Aus\",\"Aus\",\"15.03.2016\",\"07:10:00\",,,,,,\"\",,\"Z 354\",\"Normal\",\"Aus\",,\"Normal\",\"2\""
            };

            var vcard = CsvVCardConverter.ParseCsv(data).First();

            Assert.Equal("NM V2,3/IA-IK Z 354", vcard.Title);
            Assert.Equal(new DateTime(2016,03,15,13,20,00), vcard.Start);
            Assert.Equal( new DateTime(2016, 03, 15, 14, 50, 00), vcard.End);
            Assert.Equal("Z 354", vcard.Room);

        }


    }
}
