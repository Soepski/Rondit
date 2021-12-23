using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.DTOs
{
    public class CommentDTO
    {
        public CommentDTO(int commentID, UserDTO user, int postID, string inhoud, DateTime datum)
        {
            CommentID = commentID;
            this.user = user;
            PostID = postID;
            Inhoud = inhoud;
            Datum = datum;
        }

        public int CommentID { get; set; }
        public UserDTO user { get; set; }
        public int PostID { get; set; }
        public string Inhoud { get; set; }
        public DateTime Datum { get; set; }
    }
}
