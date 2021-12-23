using Data_layer.DTOs;
using RonditASP.Models;
using System.Collections.Generic;

namespace RonditASP.Interfaces
{
    public interface ICommentContainer
    {
        CommentDTO ConvertComment(Comment comment);
        List<Comment> ConvertCommentsDTOs(List<CommentDTO> commentDTOs);
        bool CreateComment(User auteur, Post post, string comment);
        bool DeleteComment(CommentDTO comment);
        List<Comment> GetAllComments(Post post);
    }
}