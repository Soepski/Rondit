using Data_layer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.Interfaces
{
    public interface IUser
    {
        List<UserDTO> GetAllUsers();
        UserDTO LoginUser(UserDTO user);      
        bool CreateUser(string username, string email, string password);
        bool RemoveUser();
        UserDTO GetUser(int gebruikersid);
        UserDTO GetUser(string gebruikersnaam);
        bool FollowUser(UserDTO follower, UserDTO followed);
        bool UnfollowUser(UserDTO follower, UserDTO followed);
        bool CheckFollow(UserDTO follower, UserDTO followed);
    }
}
