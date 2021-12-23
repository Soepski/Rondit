using Data_layer.DTOs;
using RonditASP.Models;
using System.Collections.Generic;

namespace RonditASP.Interfaces
{
    public interface IPostContainer
    {
        PostDTO ConvertPost(Post post);
        Post ConvertPostDTO(PostDTO p);
        List<Post> ConvertPostDTOs(List<PostDTO> lPostDTOs);
        List<PostUpvote> ConvertPostUpvoteDTOs(List<PostUpvoteDTO> pudl);
        bool Create(Post post);
        bool DownvotePost(User user, Post post);
        List<Post> GetAll();
        List<PostUpvote> GetAllUserVotes(User user);
        Post GetPostByID(int id);
        bool RemovePost(Post post);
        bool UpvotePost(User user, Post post);
    }
}