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
    public class MessageController : Controller
    {
        MessageContainer mc = new MessageContainer(new MessageDAL());
        UserContainer uc = new UserContainer(new UserDAL());

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

                List<Message> messages = mc.GetAll(u);

                UserMessagesViewModel umvm = new UserMessagesViewModel(u, messages);

                return View(umvm);
            }
        }

        public IActionResult CreateView(string username)
        {
            MessageCreateViewModel mcvm = new MessageCreateViewModel();
            mcvm.receiver = username;
            return View(mcvm);
        }

        public IActionResult Create(MessageCreateViewModel message)
        {
            User u = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            User receiver = uc.GetUser(message.receiver);

            if (u != null)
            {
                bool rowsaffected = mc.Create(u, receiver, message.description);

                return View((object)rowsaffected);
            }
            else
            {
                return RedirectToAction("ShowError", "Error", new { error = "Je bent niet ingelogd" });
            }

            
        }

    }
}
