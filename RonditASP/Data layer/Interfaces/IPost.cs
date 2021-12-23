using Data_layer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.Interfaces
{
    public interface IPost
    {
        List<PostDTO> GetAllPosts();
        PostDTO GetPostByID(int id);
        bool CreatePost(PostDTO post);
        bool RemovePost(PostDTO post);
        bool UpdatePost(UserDTO user, PostDTO post, int vote);
        List<PostUpvoteDTO> GetAllUserVotes(UserDTO user);
    }
}
