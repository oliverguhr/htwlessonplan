using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace HtwLessonPlan.Controllers
{
    [Route("api/[controller]")]
    public class CalendarController : Controller
    {
        // GET api/calendar/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
