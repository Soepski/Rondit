using Data_layer.DTOs;
using RonditASP.Models;
using RonditASP.ViewModels;

namespace RonditASP.Interfaces
{
    public interface IUserContainer
    {
        bool CheckFollow(User follower, User followed);
        User ConvertDTO(UserDTO userdto);
        UserDTO ConvertUser(User user);
        bool CreateUser(RegisterViewModel rvm);
        bool FollowUser(User follower, User followed);
        User GetUser(int gebruikersid);
        User GetUser(string gebruikersnaam);
        User LoginUser(User u);
        bool UnfollowUser(User follower, User followed);
    }
}