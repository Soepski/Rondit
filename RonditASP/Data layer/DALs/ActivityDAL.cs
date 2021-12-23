using Data_layer.DTOs;
using Data_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.DALs
{
    public class ActivityDAL : IActivity
    {
        string connectionString = "Server=mssql.fhict.local;Database=dbi413096;User Id=dbi413096;Password=boyke7r7_;";

        public List<ActivityDTO> GetAllActivities()
        {
            throw new NotImplementedException();
        }

        public List<ActivityDTO> GetFriendFeed(UserDTO user)
        {
            List<ActivityDTO> activities = new List<ActivityDTO>();
            List<int> gebruikerids = new List<int>();
            string query = "SELECT Activiteit.ID, Gebruiker.ID AS GebruikersID, Gebruiker.gebruikersnaam, Gebruiker.email, Gebruiker.rol, Bericht, Datum FROM Activiteit INNER JOIN Gebruiker on Gebruiker.ID = Activiteit.GebruikersID WHERE GebruikersID = 0";

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlCommand dbCommand = new SqlCommand("SELECT VolgID FROM Follow WHERE GebruikerID = @userid ", dbConn))
                {
                    dbCommand.Parameters.Add("@userid", sqlDbType: SqlDbType.VarChar).Value = user.ID;

                    using (SqlDataReader dr = dbCommand.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        foreach (DataRow dtr in dt.Rows)
                        {
                            int id = int.Parse(dtr["VolgID"].ToString());
                            gebruikerids.Add(id);
                        }

                    }

                }

                foreach (int id in gebruikerids)
                {
                    query = query + $" OR GebruikersID = {id}";
                }

                if (query == "SELECT Activiteit.ID, Gebruiker.ID AS GebruikersID, Gebruiker.gebruikersnaam, Gebruiker.email, Gebruiker.rol, Bericht, Datum FROM Activiteit INNER JOIN Gebruiker on Gebruiker.ID = Activiteit.GebruikersID WHERE GebruikersID = 0")
                {
                    List<ActivityDTO> geenactivites = null;

                    return geenactivites;
                }
                else
                {
                    query = query + "ORDER BY Datum DESC";

                    using (SqlCommand dbCommand = new SqlCommand(query, dbConn))
                    {
                        using (SqlDataReader dr = dbCommand.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dr);

                            foreach (DataRow dtr in dt.Rows)
                            {
                                int ID = int.Parse(dtr["ID"].ToString());
                                UserDTO activityuser = new UserDTO(int.Parse(dtr["GebruikersID"].ToString()), dtr["gebruikersnaam"].ToString(), dtr["email"].ToString(), dtr["rol"].ToString());
                                string inhoud = dtr["Bericht"].ToString();
                                DateTime datum = DateTime.Parse(dtr["Datum"].ToString());


                                ActivityDTO dto = new ActivityDTO(ID, activityuser, inhoud, datum);

                                activities.Add(dto);
                            }
                        }

                    }
                }
                
                dbConn.Close();

                return activities;
            }
        }
    }
}
