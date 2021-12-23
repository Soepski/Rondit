using System;

namespace RonditASP.Models
{
    public class Comment
    {
        public Comment(int commentID, User user, int postID, string inhoud, DateTime datum)
        {
            CommentID = commentID;
            this.user = user;
            PostID = postID;
            Inhoud = inhoud;
            Datum = datum;
        }

        public int CommentID { get; set; }
        public User user { get; set; }
        public int PostID { get; set; }
        public string Inhoud { get; set; }
        public DateTime Datum { get; set; }
    }
}