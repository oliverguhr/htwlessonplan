﻿using HtwLessonPlan.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtwLessonPlan
{
    internal class Ical
    {
        internal static string Generate(List<CalendarEvent> events)
        {            
            StringBuilder sw = new StringBuilder();            
            sw.AppendLine("BEGIN:VCALENDAR");
            // Write the version (2 required for OS X iCal).
            sw.AppendLine("VERSION:2.0");
            sw.AppendLine("X-WR-CALNAME:HTW Dresden");
                        
            foreach (var calendarEvent in events)
            {
                // Create string to format start time properly.
                string strStart = calendarEvent.Start.ToUniversalTime().ToString("yyyyMMdd'T'HHmmss");
                // Create string to format end time properly.
                string strEnd = calendarEvent.End.ToUniversalTime().ToString("yyyyMMdd'T'HHmmss");                           
                
                sw.AppendLine("BEGIN:VEVENT");
                sw.AppendFormat("UID:{0}@htw-dresden.de",DateTime.Now.Ticks);
                sw.AppendLine();
                sw.AppendLine("SUMMARY:" + calendarEvent.Title);
                sw.AppendLine("DESCRIPTION: Raum" + calendarEvent.Room);
                sw.AppendLine("LOCATION:" + calendarEvent.Room);
                sw.AppendLine("DTSTAMP:"+DateTime.UtcNow.ToString("yyyyMMdd'T'HHmmss")+"Z");
                sw.AppendLine("DTSTART:" + strStart + "Z");
                sw.AppendLine("DTEND:" + strEnd + "Z");                       
                sw.AppendLine("END:VEVENT");
            }            
            sw.AppendLine("END:VCALENDAR");            
            return sw.ToString();
        }
    }
}