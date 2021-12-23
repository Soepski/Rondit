using Data_layer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.Interfaces
{
    public interface IComment
    {
        List<CommentDTO> GetAllComments(PostDTO post);
        bool CreateComment(UserDTO auteur, PostDTO post, string comment);
        bool DeleteComment(CommentDTO comment);
    }
}
