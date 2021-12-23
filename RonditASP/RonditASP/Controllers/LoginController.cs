using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_layer.DALs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RonditASP.Models;
using RonditASP.ViewModels;

namespace RonditASP.Controllers
{
    public class LoginController : Controller
    {
        UserContainer uc = new UserContainer(new UserDAL());

        public IActionResult Index()
        {
            //Check of gebruiker is ingelogd
            if (HttpContext.Session.GetString("User") == null)
            {
                return View(null);
            }
            else
            {
                return View(null);
            }
        }

        public IActionResult Login(UserViewModel uvm)
        {
            if (uvm.Username != null && uvm.Password != null)
            {
                User u = uc.LoginUser(new User(uvm.Username, uvm.Password));

                if (u != null)
                {
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(u));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("ShowError", "Error", new { error = "Username or password is incorrect" });
                }
            }
            else
            {
                return RedirectToAction("ShowError", "Error", new { error = "Matje zulde gij nie eerst ekkes wa invullen jongen?" });
            }
                     
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");

            //return RedirectToAction("ShowError", "Error", new { error = "You're logged out" });
        }
    }
}