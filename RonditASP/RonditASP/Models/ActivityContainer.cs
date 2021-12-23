using Data_layer.DALs;
using Data_layer.DTOs;
using Data_layer.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RonditASP.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RonditASP.Models
{
    public class ActivityContainer : IActivityContainer
    {
        UserContainer uc = new UserContainer(new UserDAL());
        IActivity activitydal;

        public ActivityContainer(IActivity iactivity)
        {
            this.activitydal = iactivity;
        }

        List<Activity> ConvertActivitiesDTO(List<ActivityDTO> dtos)
        {
            if (dtos != null)
            {
                List<Activity> activities = new List<Activity>();

                foreach (ActivityDTO dto in dtos)
                {
                    activities.Add(new Activity(dto.activityid, uc.ConvertDTO(dto.user), dto.message, dto.date));
                }

                return activities;
            }
            else
            {
                return null;
            }

        }

        public List<Activity> GetAllActivities()
        {
            List<Activity> activities = new List<Activity>();

            return activities;
        }

        public List<Activity> GetAllFriendFeed(User user)
        {
            List<Activity> activities = ConvertActivitiesDTO(activitydal.GetFriendFeed(uc.ConvertUser(user)));

            return activities;
        }
    }
}
