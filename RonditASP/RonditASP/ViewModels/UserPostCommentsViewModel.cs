using RonditASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.ViewModels
{
    public class UserPostCommentsViewModel
    {
        UserViewModel ConvertToUserViewModel(User user)
        {
            UserViewModel uvm = new UserViewModel(user);

            return uvm;
        }

        public UserPostCommentsViewModel(User user, Post post, List<Comment> comments)
        {
            this.user = ConvertToUserViewModel(user);
            this.post = post;
            this.comments = comments;
        }

        public UserViewModel user { get; set; }
        public Post post { get; set; }
        public List<Comment> comments { get; set; }
    }
}
