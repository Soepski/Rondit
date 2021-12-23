using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_layer.DALs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RonditASP.Models;
using Microsoft.AspNetCore.Http;
using RonditASP.ViewModels;

namespace RonditASP.Controllers
{
    public class PostController : Controller
    {
        PostContainer pc = new PostContainer(new PostDAL());
        CommentContainer cc = new CommentContainer(new CommentDAL());
        public IActionResult Index()
        {
            //Check of gebruiker is ingelogd
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }

        public IActionResult ShowPost(int id)
        {
            User u = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            Post p = pc.GetPostByID(id);
            List<Comment> comments = cc.GetAllComments(p);

            UserPostCommentsViewModel upcvm = new UserPostCommentsViewModel(u, p, comments);

            return View(upcvm);
        }

        public IActionResult DeletePost(int id)
        {
            User u = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            Post post = pc.GetPostByID(id);

            if (u.Role == "admin")
            {
                if (pc.RemovePost(post))
                {
                    return RedirectToAction("ShowError", "Error", new { error = "Post is deleted" });
                }
                else
                {
                    return RedirectToAction("ShowError", "Error", new { error = "Something went wrong" });
                }
            }
            else
            {
                return RedirectToAction("ShowError", "Error", new { error = "You're not an admin" });
            }

        }

        public IActionResult UpvotePost(int postid)
        {
            User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            Post post = pc.GetPostByID(postid);

            pc.UpvotePost(user, post);

            return RedirectToAction("Index", "Home");
            //return RedirectToAction("ShowError", "Error", new { error = "Post is geupvote" });
        }
        public IActionResult DownvotePost(int postid)
        {
            User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            Post post = pc.GetPostByID(postid);

            pc.DownvotePost(user, post);

            return RedirectToAction("Index", "Home");
            //return RedirectToAction("ShowError", "Error", new { error = "Post is gedownvote" });
        }

        public IActionResult Create(PostCreateViewModel pcvm)
        {
            User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));

            if (user != null)
            {
                Post post = new Post(0, user, pcvm.PostTitle, pcvm.PostDescription, DateTime.Now, 0);

                PostViewModel pvm = new PostViewModel(post);

                try
                {
                    pc.Create(post);
                }
                catch (ArgumentException ex)
                {
                    return RedirectToAction("ShowError", "Error", new { error = ex.Message });
                }

                return View(pvm);
            }
            else
            {
                return RedirectToAction("ShowError", "Error", new { error = "Je bent niet ingelogd" });
            }

        }
    }
}
