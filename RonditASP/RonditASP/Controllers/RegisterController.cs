using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_layer.DALs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RonditASP.Models;
using RonditASP.ViewModels;

namespace RonditASP.Controllers
{
    public class RegisterController : Controller
    {
        UserContainer uc = new UserContainer(new UserDAL());
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register(RegisterViewModel rvm)
        {
            if (rvm.username == null || rvm.email == null || rvm.password == null || rvm.passwordrepeat == null)
            {
                return RedirectToAction("ShowError", "Error", new { error = "Please fill in all the fields, matje" });
            }

            if (rvm.password == rvm.passwordrepeat)
            {
                
                try
                {
                    bool result = uc.CreateUser(rvm);

                    if (result)
                    {
                        return RedirectToAction("ShowError", "Error", new { error = "Your account is created" });
                    }
                    
                     return RedirectToAction("ShowError", "Error", new { error = "Something went wrong creating your account" });
                                                            
                }

                catch (Exception ex)
                {
                    return RedirectToAction("ShowError", "Error", new { error = ex.Message });
                }

      
            }
            else
            {
                return RedirectToAction("ShowError", "Error", new { error = "Passwords are not the same" });
            }
        }
    }
}
