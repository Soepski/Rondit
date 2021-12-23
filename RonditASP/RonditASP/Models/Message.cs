using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.Models
{
    public class Message
    {
        public Message(User zender, User ontvanger, string bericht, DateTime datum)
        {
            Zender = zender;
            Ontvanger = ontvanger;
            Bericht = bericht;
            Datum = datum;
        }

        public User Zender { get; set; }
        public User Ontvanger { get; set; }
        public string Bericht { get; set; }
        public DateTime Datum { get; set; }
    }
}
