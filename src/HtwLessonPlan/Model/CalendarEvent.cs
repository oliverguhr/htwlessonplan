﻿using System;

namespace HtwLessonPlan.Model
{
    internal struct CalendarEvent
    {
        public string Title { get; set; }
        public string Room { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}