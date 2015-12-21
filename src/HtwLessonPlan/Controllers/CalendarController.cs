using HtwLessonPlan.Model;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace HtwLessonPlan.Controllers
{
    [Route("student/")]
    public class CalendarController : Controller
    {
        private ILogger log;

        public CalendarController(ILoggerFactory loggerFactory)
        {
            log = loggerFactory.CreateLogger("CalendarController");            
        }

        // GET student/12345/lessons.ical
        [Produces("text/calendar")]
        [HttpGet("{studentNumber}/lessons.ical")]
        public async Task<IActionResult> Get(string studentNumber)
        {
            log.LogInformation("Request for {0}", studentNumber);

            HtwWebservice htw = new HtwWebservice(new Uri("http://www2.htw-dresden.de/~rawa/cgi-bin/auf/raiplan_kal.php"));

            List<CalendarEvent> calendar = new List<CalendarEvent>();

            try
            {
                calendar = await htw.LoadCalendar(studentNumber);
            }
            catch (Exception ex)
            {
                log.LogError("Could not load htw calendar",ex);
                return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
            var data = System.Text.Encoding.UTF8.GetBytes(Ical.Generate(calendar));

            log.LogVerbose("Sending ICal with {0} events", calendar.Count);
            return new FileContentResult(data, "text/calendar");
        }
    }
}