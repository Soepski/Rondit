using Data_layer.DTOs;
using Data_layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_layer.DALs
{
    public class MessageDAL : IMessage
    {
        string connectionString = "Server=mssql.fhict.local;Database=dbi413096;User Id=dbi413096;Password=boyke7r7_;";

        public bool DeleteMessage(MessageDTO message)
        {
            throw new NotImplementedException();
        }

        public List<MessageDTO> GetAllMessage(UserDTO user)
        {
            List<MessageDTO> messages = new List<MessageDTO>();

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlCommand dbCommand = new SqlCommand("SELECT " +
                    "ontvanger.ID AS 'OntvangerID', " +
                    "ontvanger.Gebruikersnaam AS 'Ontvanger', " +
                    "ontvanger.email AS 'Ontvanger Email', " +
                    "ontvanger.rol AS 'Ontvanger rol', " +
                    "zender.ID AS 'ZenderID', " +
                    "zender.Gebruikersnaam AS 'Zender', " +
                    "zender.email AS 'Zender Email', " +
                    "zender.rol AS 'Zender rol', " +
                    "Bericht, " +
                    "Datum " +
                    "FROM Message as m " +
                    "INNER JOIN Gebruiker AS ontvanger ON ontvanger.ID = m.OntvangerID " +
                    "INNER JOIN Gebruiker AS zender ON zender.ID = m.ZenderID " +
                    "WHERE m.OntvangerID = @userID", dbConn))
                {
                    dbCommand.Parameters.Add("@userID", SqlDbType.VarChar).Value = user.ID;

                    using (SqlDataReader dr = dbCommand.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        foreach (DataRow dtr in dt.Rows)
                        {
                            UserDTO ontvanger = new UserDTO(int.Parse(dtr["OntvangerID"].ToString()), dtr["Ontvanger"].ToString(), dtr["Ontvanger Email"].ToString(), dtr["Ontvanger rol"].ToString());
                            UserDTO zender = new UserDTO(int.Parse(dtr["ZenderID"].ToString()), dtr["Zender"].ToString(), dtr["Zender Email"].ToString(), dtr["Zender rol"].ToString());
                            string inhoud = dtr["Bericht"].ToString();
                            DateTime datum = DateTime.Parse(dtr["Datum"].ToString());


                            MessageDTO mdto = new MessageDTO(zender, ontvanger, inhoud, datum);

                            messages.Add(mdto);
                        }
                    }


                }
                dbConn.Close();

                return messages;
            }
        }

        public bool SendMessage(UserDTO sender, UserDTO reciever, string description)
        {
            int rowsaffected = 0;

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlCommand dbCommand = new SqlCommand($"INSERT INTO Message (ZenderID, OntvangerID, Bericht, Datum) VALUES (@senderid, @recieverid, @description, CURRENT_TIMESTAMP); ", dbConn))
                {


                    dbCommand.Parameters.Add("@description", SqlDbType.VarChar).Value = description;
                    dbCommand.Parameters.Add("@senderid", SqlDbType.VarChar).Value = sender.ID;
                    dbCommand.Parameters.Add("@recieverid", SqlDbType.VarChar).Value = reciever.ID;

                    rowsaffected = dbCommand.ExecuteNonQuery();
                }

                dbConn.Close();

                return rowsaffected == 1 ? true : false;
            }
        }
    }
}
