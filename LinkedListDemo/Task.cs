using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace LinkedListDemo
{
    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public int DeadlineYear { get; set; }
        public int DeadlineMonth { get; set; }
        public int DeadlineDay { get; set; }

        /// <summary>
        /// A new instance of the Task class.
        /// </summary>
        public Task()
        {
            Title = "No title";
            Description = "No description";
            Subject = "No subject";
            DeadlineYear = DateTime.Now.Year;
            DeadlineMonth = DateTime.Now.Month;
            DeadlineDay = DateTime.Now.Day;
        }

        /// <summary>
        /// A new instance of the Task class.
        /// </summary>
        /// <param name="title">Task title.</param>
        /// <param name="description">Task description.</param>
        /// <param name="subject">Subject task belongs to.</param>
        /// <param name="year">Year of task deadline.</param>
        /// <param name="month">Month of task deadline.</param>
        /// <param name="day">Day of task deadline.</param>
        public Task(string title, string description, string subject, int year, int month, int day)
        {
            Title = title;
            Description = description;
            Subject = subject;
            DeadlineYear = year;
            DeadlineMonth = month;
            DeadlineDay = day;
        }

        /// <summary>
        /// Get a deadline date.
        /// </summary>
        /// <returns>Returns a deadline date in the following format: YYYY-MM-DD.</returns>
        public string GetDate()
        {
            string date = DeadlineYear.ToString() + "-";
            date += DeadlineMonth < 10 ? "0" + DeadlineMonth.ToString() + "-" : DeadlineMonth.ToString() + "-";
            date += DeadlineDay < 10 ? "0" + DeadlineDay.ToString() : DeadlineDay.ToString();
            return date;
        }
    }
}
