using RonditASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            ID = user.ID;
            Username = user.Username;
            Email = user.Email;
            Role = user.Role;
            Password = user.Password;
        }

        public UserViewModel()
        {

        }

        public int ID { get; private set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
