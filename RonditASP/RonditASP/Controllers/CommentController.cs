using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_layer.DALs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RonditASP.Models;

namespace RonditASP.Controllers
{
    public class CommentController : Controller
    {
        CommentContainer cc = new CommentContainer(new CommentDAL());
        PostContainer pc = new PostContainer(new PostDAL());
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

        //Met HttpPost gedaan anders was de class te complex
        [HttpPost]
        public IActionResult Create(string comment, string postid)
        {
            //Check of gebruiker is ingelogd
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
                Post post = pc.GetPostByID(int.Parse(postid));

                bool succeeded = cc.CreateComment(user, post, comment);

                return RedirectToAction("ShowError", "Error", new { error = "Je comment is geplaatst" });
            }

            
        }
    }
}