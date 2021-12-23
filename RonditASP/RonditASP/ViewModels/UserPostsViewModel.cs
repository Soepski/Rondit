using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RonditASP.Models;

namespace RonditASP.ViewModels
{
    public class UserPostsViewModel
    {
        UserViewModel ConvertToUserViewModel(User user)
        {
            UserViewModel uvm = new UserViewModel(user);

            return uvm;
        }

        public UserPostsViewModel(User user, List<Post> posts)
        {
            this.user = ConvertToUserViewModel(user);
            this.posts = posts;
        }

        public UserViewModel user { get; set; }
        public List<Post> posts { get; set; }
    }
}
