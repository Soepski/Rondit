using Data_layer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.Interfaces
{
    public interface IActivity
    {
        List<ActivityDTO> GetAllActivities();
        List<ActivityDTO> GetFriendFeed(UserDTO user);
    }
}
