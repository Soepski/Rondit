using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.Models
{
    public class Activity
    {
        public Activity(int activityid, User user, string message, DateTime date)
        {
            this.activityid = activityid;
            this.user = user;
            this.message = message;
            this.date = date;
        }

        public int activityid { get; set; }
        public User user { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
    }
}
