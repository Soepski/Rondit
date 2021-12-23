using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.DTOs
{
    public class PostUpvoteDTO
    {
        public PostUpvoteDTO(int userID, int postID, int vote)
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
