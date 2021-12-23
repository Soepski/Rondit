using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using RonditASP.Models;
using Data_layer.DALs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RonditASP.ViewModels;

namespace RonditASP.Controllers
{
    public class HomeController : Controller
    {
        readonly PostContainer pc = new PostContainer(new PostDAL());
        readonly UserContainer uc = new UserContainer(new UserDAL());
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Check of gebruiker is ingelogd
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                User u = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));

                List<PostUpvote> pus = pc.GetAllUserVotes(u);

                List<Post> posts = pc.GetAll();

                //Check of er een upvote of downvote is bij een PostID. Plaats deze in de point property in de posts
                foreach (PostUpvote pu in pus)
                {
                    foreach (Post p in posts)
                    {
                        if (pu.PostID == p.PostID)
                        {
                            p.Vote = pu.Vote;
                        }
                    }   
                    
                }

                UserPostsViewModel upvm = new UserPostsViewModel(u, posts);

                return View(upvm);
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
