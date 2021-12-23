using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_layer.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RonditASP.Models;

namespace RonditASP.Controllers
{
    public class CreateController : Controller
    {
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

        [HttpPost]
        public IActionResult Create(string posttitle, string postdescription)
        {
            User u = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));

            if (u != null)
            {
                Post post = new Post(0, u, posttitle, postdescription, DateTime.Now, 0);

                pc.Create(u, posttitle, postdescription);

                return View(post);
            }
            else
            {
                return RedirectToAction("ShowError", "Error");
            }
            
        }
    }
}