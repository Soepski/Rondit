using Data_layer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.Interfaces
{
    public interface IMessage
    {
        List<MessageDTO> GetAllMessage(UserDTO user);
        bool SendMessage(UserDTO zender, UserDTO ontvanger, string inhoud);
        bool DeleteMessage(MessageDTO message);
    }
}
