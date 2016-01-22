using HtwLessonPlan.Model;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using HtwLessonPlan;

namespace HtwLessonPlan.Controllers
{
    [Route("student/")]
    public class CalendarController : Controller
    {
        private ILogger log;
        private IHtwWebService service;

        public CalendarController(ILoggerFactory loggerFactory, IHtwWebService htwWebService)
        {
            log = loggerFactory.CreateLogger("CalendarController");            
            service = htwWebService;
        }

        // GET student/12345/lessons.ical
        [Produces("text/calendar")]
        [HttpGet("{studentNumber}/lessons.ical")]
        public async Task<IActionResult> Get(string studentNumber)
        {
            log.LogInformation("Request for {0}", studentNumber);

            List<CalendarEvent> calendar = new List<CalendarEvent>();

            try
            {
                calendar = await service.LoadCalendar(studentNumber);
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