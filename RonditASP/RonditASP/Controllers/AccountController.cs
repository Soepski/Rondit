using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Data_layer.DALs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RonditASP.Models;
using RonditASP.ViewModels;

namespace RonditASP.Controllers
{
    public class AccountController : Controller
    {
        UserContainer uc = new UserContainer(new UserDAL());

        public IActionResult Index(string username)
        {
            if (username == "Default")
            {
                //Check of gebruiker is ingelogd
                if (HttpContext.Session.GetString("User") == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
                    bool follow = false;
                    bool logged = true;

                    UserProfileViewModel upvm = new UserProfileViewModel(user, follow, logged);

                    return View(upvm);
                }
            }
            else
            {
                User follower = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
                User followed = uc.GetUser(username);

                if (uc.CheckFollow(follower, followed))
                {
                    UserProfileViewModel upvm = new UserProfileViewModel(followed, true, false);
                    return View(upvm);
                }
                else
                {
                    UserProfileViewModel upvm = new UserProfileViewModel(followed, false, false);
                    return View(upvm);
                }
               
            }
            
        }
        public IActionResult Follow(int id)
        {
            //Check of gebruiker is ingelogd
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));

                if (user.ID == id)
                {
                    return RedirectToAction("ShowError", "Error", new { error = "Ge probeert uw eigen te volgen maatje, proef ik hier narcisme?" }); 
                }
                else
                {
                    if (uc.FollowUser(uc.GetUser(user.ID), uc.GetUser(id)))
                    {
                        return RedirectToAction("Index", "Account", new { username = uc.GetUser(id).Username });
                        //return RedirectToAction("ShowError", "Error", new { error = $"Je volgt nu {uc.GetUser(id).Username}" });
                    }
                    else
                    {
                        return RedirectToAction("ShowError", "Error", new { error = $"Je volgt deze persoon al" });
                    }
                    
                }
            }
        }
        public IActionResult Unfollow(int id)
        {
            //Check of gebruiker is ingelogd
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));

                if (user.ID == id)
                {
                    return RedirectToAction("ShowError", "Error", new { error = "Ge probeert uw eigen te ontvolgen maatje, weet nie hoe ge het voor elkaar krijgt maar goed" });
                }
                else
                {
                    if (uc.UnfollowUser(uc.GetUser(user.ID), uc.GetUser(id)))
                    {
                        return RedirectToAction("Index", "Account", new { username = uc.GetUser(id).Username });
                        //return RedirectToAction("ShowError", "Error", new { error = $"Je hebt {uc.GetUser(id).Username} ontvolgt" });
                    }
                    else
                    {
                        return RedirectToAction("ShowError", "Error", new { error = $"Je volgt deze persoon niet" });
                    }

                }
            }
        }
    }
}