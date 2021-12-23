using Data_layer.DALs;
using Data_layer.DTOs;
using Data_layer.Interfaces;
using RonditASP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.Models
{
    public class MessageContainer : IMessageContainer
    {
        UserContainer uc = new UserContainer(new UserDAL());
        IMessage messagedal;

        public MessageContainer(IMessage imessage)
        {
            this.messagedal = imessage;
        }

        public List<Message> ConvertMessageDTO(List<MessageDTO> lMessageDTOs)
        {
            List<Message> messages = new List<Message>();

            foreach (MessageDTO m in lMessageDTOs)
            {
                User zender = new User(m.Zender.ID, m.Zender.Username, m.Zender.Email, m.Zender.Role);
                User ontvanger = new User(m.Ontvanger.ID, m.Ontvanger.Username, m.Ontvanger.Email, m.Ontvanger.Role);
                messages.Add(new Message(zender, ontvanger, m.Bericht, m.Datum));
            }

            return messages;
        }

        public List<Message> GetAll(User user)
        {
            List<Message> messages = ConvertMessageDTO(messagedal.GetAllMessage(uc.ConvertUser(user)));

            return messages;
        }

        public bool Create(User sender, User receiver, string description)
        {
            UserDTO senderdto = uc.ConvertUser(sender);
            UserDTO receiverdto = uc.ConvertUser(receiver);

            bool succeeded = messagedal.SendMessage(senderdto, receiverdto, description);

            return succeeded;
        }
    }
}
