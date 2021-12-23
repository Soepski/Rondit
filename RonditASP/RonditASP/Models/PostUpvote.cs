using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.Models
{
    public class PostUpvote
    {
        public PostUpvote(int userID, int postID, int vote)
        {
            UserID = userID;
            PostID = postID;
            Vote = vote;
        }

        public int UserID { get; set; }
        public int PostID { get; set; }
        public int Vote { get; set; }
    }
}
