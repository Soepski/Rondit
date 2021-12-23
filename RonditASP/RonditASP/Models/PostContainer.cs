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
    public class PostContainer : IPostContainer
    {
        IPost postdal;
        UserContainer uc = new UserContainer(new UserDAL());

        public PostContainer(IPost ipost)
        {
            this.postdal = ipost;
        }
        public bool ValidatePostTitle(Post post)
        {
            if (post.Title.Length > 600)
            {
                return false;
            }

            return true;
        }
        public bool ValidatePostDescription(Post post)
        {
            if (post.Description.Length > 600)
            {
                return false;
            }

            return true;
        }

        public List<Post> ConvertPostDTOs(List<PostDTO> lPostDTOs)
        {
            List<Post> posts = new List<Post>();

            foreach (PostDTO p in lPostDTOs)
            {
                posts.Add(new Post(p.PostID, uc.GetUser(p.UserID), p.Title, p.Description, p.Date, p.Points));
            }

            return posts;
        }

        public Post ConvertPostDTO(PostDTO p)
        {
            Post post = new Post(p.PostID, uc.GetUser(p.UserID), p.Title, p.Description, p.Date, p.Points);

            return post;
        }

        public PostDTO ConvertPost(Post post)
        {
            PostDTO postdto = new PostDTO(post.PostID, post.User.ID, post.Title, post.Description, post.Date, post.Points, post.Vote);

            return postdto;
        }

        public List<PostUpvote> ConvertPostUpvoteDTOs(List<PostUpvoteDTO> pudl)
        {
            List<PostUpvote> postUpvotes = new List<PostUpvote>();

            foreach (PostUpvoteDTO pud in pudl)
            {
                postUpvotes.Add(new PostUpvote(pud.UserID, pud.PostID, pud.Vote));
            }

            return postUpvotes;
        }

        public List<Post> GetAll()
        {
            List<Post> posts = ConvertPostDTOs(postdal.GetAllPosts());

            return posts;
        }

        public List<PostUpvote> GetAllUserVotes(User user)
        {
            List<PostUpvote> postupvotes = ConvertPostUpvoteDTOs(postdal.GetAllUserVotes(uc.ConvertUser(user)));

            return postupvotes;
        }

        public Post GetPostByID(int id)
        {
            Post post = ConvertPostDTO(postdal.GetPostByID(id));

            return post;
        }

        public bool Create(Post post)
        {
            if (!ValidatePostTitle(post))
            {
                throw new ArgumentException("Post title exceeds the maximum length of 600 characters.");
            }
            else if (!ValidatePostDescription(post))
            {
                throw new ArgumentException("Post description exceeds the maximum length of 600 characters.");
            }
            else if (!ValidatePostTitle(post) && !ValidatePostDescription(post))
            {
                throw new ArgumentException("Post title and description exceeds the maximum length of 600 characters.");
            }

            bool succeeded = postdal.CreatePost(ConvertPost(post));
            return succeeded;
        }

        public bool RemovePost(Post post)
        {
            bool succeeded = postdal.RemovePost(ConvertPost(post));

            return succeeded;
        }

        public bool UpvotePost(User user, Post post)
        {
            return postdal.UpdatePost(uc.ConvertUser(user), ConvertPost(post), +1);
        }

        public bool DownvotePost(User user, Post post)
        {

            return postdal.UpdatePost(uc.ConvertUser(user), ConvertPost(post), -1);
        }
    }
}
