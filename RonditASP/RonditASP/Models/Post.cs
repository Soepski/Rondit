using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.Models
{
    public class Post
    {
        public Post(int postID, User user, string title, string description, DateTime date, int points)
        {
            PostID = postID;
            User = user;
            Title = title;
            Description = description;
            Date = date;
            Points = points;
        }

        public int PostID { get; private set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Points { get; private set; }
        public int Vote { get; set; }
    }
}
