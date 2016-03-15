﻿using HtwLessonPlan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HtwLessonPlan
{
    internal class HtwWebservice
    {
        public Uri ApiAdress { get; set; }

        public HtwWebservice(Uri apiAdress)
        {
            ApiAdress = apiAdress;
        }

        internal async Task<List<CalendarEvent>> LoadCalendar(string studentNumber)
        {
            var link = await GetCsvLink(studentNumber);

            var csv = await GetCsvData(link);

            return CsvVCardConverter.ParseCsv(csv);
        }   

        private async Task<Uri> GetCsvLink(string studentNumber)
        {
            KeyValuePair<string, string> matrikelnummer = new KeyValuePair<string, string>("matr", studentNumber);
            FormUrlEncodedContent postdata = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>() { matrikelnummer });

            HttpClient request = new HttpClient();
            var response = await request.PostAsync(ApiAdress, postdata);

            string responseText = await response.Content.ReadAsStringAsync();

            var start = responseText.IndexOf("action=") + 7;
            var end = responseText.IndexOf(">", start);

            var link = responseText.Substring(start, end - start);

            return new Uri(ApiAdress, link);
        }

        private async Task<List<string>> GetCsvData(Uri csvLink)
        {
            HttpClient request = new HttpClient();
            var csv = Encoding.GetEncoding(1252).GetString(await request.GetByteArrayAsync(csvLink));
            var lines = csv.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            return lines.Skip(1).ToList();
        }
    }
}