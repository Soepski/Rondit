using RonditASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RonditASP.ViewModels
{
    public class UserProfileViewModel
    {
        UserViewModel ConvertToUserViewModel(User user)
        {
            UserViewModel uvm = new UserViewModel(user);

            return uvm;
        }

        public UserProfileViewModel(User user, bool follow, bool logged)
        {
            this.user = ConvertToUserViewModel(user);
            this.follow = follow;
            this.logged = logged;
        }

        public UserViewModel user { get; set; }
        public bool follow { get; set; }
        public bool logged { get; set; }
    }
}
