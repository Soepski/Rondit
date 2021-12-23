using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.DTOs
{
    public class ActivityDTO
    {
        public ActivityDTO(int activityid, UserDTO user, string message, DateTime date)
        {
            this.activityid = activityid;
            this.user = user;
            this.message = message;
            this.date = date;
        }

        public int activityid { get; set; }
        public UserDTO user { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }

    }
}
