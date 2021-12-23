using RonditASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.ViewModels
{
    public class PostViewModel
    {
        UserViewModel ConvertToUserViewModel(User user)
        {
            UserViewModel uvm = new UserViewModel(user);

            return uvm;
        }

        public PostViewModel(Post post)
        {
            PostID = post.PostID;
            User = ConvertToUserViewModel(post.User);
            Title = post.Title;
            Description = post.Description;
            Date = post.Date;
            Points = post.Points;
            Vote = post.Vote;
        }


        public int PostID { get; private set; }
        public UserViewModel User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Points { get; private set; }
        public int Vote { get; set; }
    }
}
