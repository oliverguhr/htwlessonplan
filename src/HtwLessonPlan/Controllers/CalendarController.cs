using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using HtwLessonPlan.Model;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace HtwLessonPlan.Controllers
{
    [Route("api/[controller]")]
    public class CalendarController : Controller
    {
        // GET api/calendar/12345  
        [Produces("text/calendar")]
        [HttpGet("{studentNumber}")]
        public async Task<IActionResult> Get(string studentNumber)
        {
            HtwWebservice htw = new HtwWebservice(new Uri("http://www2.htw-dresden.de/~rawa/cgi-bin/auf/raiplan_kal.php"));

            List<CalendarEvent> calendar = new List<CalendarEvent>();

            try
            {
                calendar = await htw.LoadCalendar(studentNumber);
            }
            catch (Exception)
            {
                
                return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
            var data = System.Text.Encoding.UTF8.GetBytes(Ical.Generate(calendar));
            return new FileContentResult(data, "text/calendar");
        }
    }
}
