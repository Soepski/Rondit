using RonditASP.Models;
using System.Collections.Generic;

namespace RonditASP.Interfaces
{
    public interface IActivityContainer
    {
        List<Activity> GetAllActivities();
        List<Activity> GetAllFriendFeed(User user);
    }
}