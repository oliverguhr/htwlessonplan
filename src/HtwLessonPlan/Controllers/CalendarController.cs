using HtwLessonPlan.Model;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HtwLessonPlan.Controllers
{
    [Route("student/")]
    public class CalendarController : Controller
    {
        // GET student/12345/lessons.ical
        [Produces("text/calendar")]
        [HttpGet("{studentNumber}/lessons.ics")]
        public async Task<IActionResult> Get(string studentNumber)
        {
            Trace.TraceInformation("Request for {0}", studentNumber);

            HtwWebservice htw = new HtwWebservice(new Uri("http://www2.htw-dresden.de/~rawa/cgi-bin/auf/raiplan_kal.php"));

            List<CalendarEvent> calendar = new List<CalendarEvent>();

            try
            {
                calendar = await htw.LoadCalendar(studentNumber);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
            var data = System.Text.Encoding.UTF8.GetBytes(Ical.Generate(calendar));

            Trace.TraceInformation("Sending ICal with {0} events", calendar.Count);
            return new FileContentResult(data, "text/calendar");
        }
    }
}