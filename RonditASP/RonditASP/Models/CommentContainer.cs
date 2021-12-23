using Data_layer.DALs;
using Data_layer.DTOs;
using Data_layer.Interfaces;
using RonditASP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.Models
{
    public class CommentContainer : ICommentContainer
    {
        UserContainer uc = new UserContainer(new UserDAL());
        PostContainer pc = new PostContainer(new PostDAL());
        IComment commentdal;

        public CommentContainer(IComment icomment)
        {
            this.commentdal = icomment;
        }

        public CommentDTO ConvertComment(Comment comment)
        {
            CommentDTO commentdto = new CommentDTO(comment.CommentID, uc.ConvertUser(uc.GetUser(comment.user.ID)), comment.PostID, comment.Inhoud, comment.Datum);

            return commentdto;
        }

        public List<Comment> ConvertCommentsDTOs(List<CommentDTO> commentDTOs)
        {
            List<Comment> comments = new List<Comment>();

            foreach (CommentDTO dto in commentDTOs)
            {
                comments.Add(new Comment(dto.CommentID, uc.GetUser(dto.user.ID), dto.PostID, dto.Inhoud, dto.Datum));
            }

            return comments;
        }

        public bool DeleteComment(CommentDTO comment)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetAllComments(Post post)
        {
            List<Comment> comments = ConvertCommentsDTOs(commentdal.GetAllComments(pc.ConvertPost(post)));

            return comments;
        }

        public bool CreateComment(User auteur, Post post, string comment)
        {
            bool succeeded = commentdal.CreateComment(uc.ConvertUser(auteur), pc.ConvertPost(post), comment);

            return succeeded;
        }
    }
}
