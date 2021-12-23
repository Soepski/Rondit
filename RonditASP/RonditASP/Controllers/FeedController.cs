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
    public class FeedController : Controller
    {
        ActivityContainer ac = new ActivityContainer(new ActivityDAL());
        public IActionResult Index()
        {
            //Check of gebruiker is ingelogd
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));

                List<Activity> activities = ac.GetAllFriendFeed(user);

                return View(activities);
            }
            
        }
    }
}