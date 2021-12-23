using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.DTOs
{
    public class PostDTO
    {
        public PostDTO(int postID, int userID, string title, string description, DateTime date, int points, int vote)
        {
            PostID = postID;
            UserID = userID;
            Title = title;
            Description = description;
            Date = date;
            Points = points;
            Vote = vote;
        }

        public int PostID { get; private set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Points { get; private set; }
        public int Vote { get; set; }
    }
}
