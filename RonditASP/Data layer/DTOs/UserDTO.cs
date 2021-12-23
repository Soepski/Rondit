using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.DTOs
{
    public class UserDTO
    {
        public UserDTO()
        {

        }
        public UserDTO(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public UserDTO(int ID, string Username, string Email, string Role)
        {
            this.ID = ID;
            this.Username = Username;
            this.Email = Email;
            this.Role = Role;
        }

        public string Email { get; set; }
        public int ID { get; private set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
