using RonditASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.ViewModels
{
    public class UserMessagesViewModel
    {
        UserViewModel ConvertToUserViewModel(User user)
        {
            UserViewModel uvm = new UserViewModel(user);

            return uvm;
        }
        public UserMessagesViewModel(User user, List<Message> messages)
        {
            this.user = ConvertToUserViewModel(user);
            this.messages = messages;
        }

        public UserViewModel user { get; set; }
        public List<Message> messages { get; set; }
    }
}
