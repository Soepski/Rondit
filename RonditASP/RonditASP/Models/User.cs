using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.Models
{
    public class User
    {      
        public int ID { get; private set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

        [JsonConstructor]
        public User(int ID, string Username, string Email, string Role)
        {
            this.ID = ID;
            this.Username = Username;
            this.Email = Email;
            this.Role = Role;
        }

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public bool ValidateEmail()
        {
            if (Email.Contains("@"))
            {
                return true;
            }

            return false;
        }

    }
}
