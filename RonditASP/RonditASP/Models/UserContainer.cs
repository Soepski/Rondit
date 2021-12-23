using Data_layer.DTOs;
using Data_layer.Interfaces;
using RonditASP.Interfaces;
using RonditASP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.Models
{
    public class UserContainer : IUserContainer
    {
        IUser udal;

        public UserContainer(IUser iuser)
        {
            this.udal = iuser;
        }

        public bool ValidateUsernameLength(string username)
        {
            if (username.Length > 50)
            {
                return false;
            }

            return true;
        }

        public bool ValidateEmailLength(string email)
        {
            if (email.Length > 50)
            {
                return false;
            }

            return true;
        }

        public bool ValidatePasswordLength(string password)
        {
            if (password.Length > 50)
            {
                return false;
            }

            return true;
        }

        public UserDTO ConvertUser(User user)
        {
            UserDTO userdto = new UserDTO(user.ID, user.Username, user.Email, user.Role);

            return userdto;
        }

        public User ConvertDTO(UserDTO userdto)
        {
            if (userdto != null)
            {
                User user = new User(userdto.ID, userdto.Username, userdto.Email, userdto.Role);

                return user;
            }
            else
            {
                return null;
            }


        }

        void GetAll()
        {

        }

        public User LoginUser(User u)
        {
            User user = ConvertDTO(udal.LoginUser(new UserDTO(u.Username, u.Password)));

            if (user == null)
            {
                return null;
            }
            else
            {
                return user;
            }
        }

        public User GetUser(int gebruikersid)
        {
            UserDTO userdto = udal.GetUser(gebruikersid);

            User user = new User(userdto.ID, userdto.Username, userdto.Email, userdto.Role);

            return user;
        }
        public User GetUser(string gebruikersnaam)
        {
            UserDTO userdto = udal.GetUser(gebruikersnaam);

            User user = new User(userdto.ID, userdto.Username, userdto.Email, userdto.Role);

            return user;
        }

        public bool CreateUser(RegisterViewModel rvm)
        {
            if (!ValidateUsernameLength(rvm.username) || !ValidateEmailLength(rvm.email) || !ValidatePasswordLength(rvm.password))
            {
                throw new ArgumentException("Your username, email or password exceeds the 50 character limit");
            }

            bool succeeded = udal.CreateUser(rvm.username, rvm.email, rvm.password);

            return succeeded;
        }

        void Remove()
        {

        }

        public bool FollowUser(User follower, User followed)
        {
            bool succeeded = udal.FollowUser(ConvertUser(follower), ConvertUser(followed));

            return succeeded;
        }

        public bool UnfollowUser(User follower, User followed)
        {
            bool succeeded = udal.UnfollowUser(ConvertUser(follower), ConvertUser(followed));

            return succeeded;
        }

        public bool CheckFollow(User follower, User followed)
        {
            bool succeeded = udal.CheckFollow(ConvertUser(follower), ConvertUser(followed));

            return succeeded;
        }
    }
}
