using Data_layer.DTOs;
using RonditASP.Models;
using System.Collections.Generic;

namespace RonditASP.Interfaces
{
    public interface IMessageContainer
    {
        List<Message> ConvertMessageDTO(List<MessageDTO> lMessageDTOs);
        bool Create(User sender, User receiver, string description);
        List<Message> GetAll(User user);
    }
}