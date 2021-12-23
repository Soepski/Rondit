using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.DTOs
{
    public class MessageDTO
    {
        public MessageDTO(UserDTO zender, UserDTO ontvanger, string bericht, DateTime datum)
        {
            Zender = zender;
            Ontvanger = ontvanger;
            Bericht = bericht;
            Datum = datum;
        }

        public UserDTO Zender { get; set; }
        public UserDTO Ontvanger { get; set; }
        public string Bericht { get; set; }
        public DateTime Datum { get; set; }
    }
}
