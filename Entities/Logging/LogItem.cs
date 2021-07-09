using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Logging
{
    public class LogItem
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public LogItem() { }
        public LogItem(DateTime date, string title, string details)
        {
            Date = date;
            Title = title;
            Details = details;
        }
    }
}
