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
    public class CommentDAL : IComment
    {
        string connectionString = "Server=mssql.fhict.local;Database=dbi413096;User Id=dbi413096;Password=boyke7r7_;";

        public bool DeleteComment(CommentDTO comment)
        {
            throw new NotImplementedException();
        }

        public List<CommentDTO> GetAllComments(PostDTO post)
        {
            List<CommentDTO> comments = new List<CommentDTO>();

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlCommand dbCommand = new SqlCommand("SELECT CommentID, Gebruiker.ID, Gebruiker.gebruikersnaam, Gebruiker.email, Gebruiker.rol, PostID, Inhoud, Datum FROM Comment INNER JOIN Gebruiker on Gebruiker.ID = Comment.GebruikersID WHERE PostID = @postid", dbConn))
                {
                    dbCommand.Parameters.Add("@postid", SqlDbType.VarChar).Value = post.PostID;

                    using (SqlDataReader dr = dbCommand.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        foreach (DataRow dtr in dt.Rows)
                        {
                            int CommentID = int.Parse(dtr["CommentID"].ToString());                           
                            int PostID = int.Parse(dtr["PostID"].ToString());
                            string Inhoud = dtr["Inhoud"].ToString();
                            DateTime Datum = DateTime.Parse(dtr["Datum"].ToString());

                            int ID = int.Parse(dtr["ID"].ToString());
                            string gebruikersnaam = dtr["gebruikersnaam"].ToString();
                            string email = dtr["email"].ToString();
                            string rol = dtr["rol"].ToString();

                            UserDTO user = new UserDTO(ID, gebruikersnaam, email, rol);

                            CommentDTO comment = new CommentDTO(CommentID, user, PostID, Inhoud, Datum);

                            comments.Add(comment);
                        }
                    }


                }
                dbConn.Close();

                return comments;
            }
        }

        public bool CreateComment(UserDTO auteur, PostDTO post, string comment)
        {
            int rowsaffected = 0;

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlCommand dbCommand = new SqlCommand($"INSERT INTO dbi413096.dbo.Comment (GebruikersID, PostID, Inhoud, Datum) VALUES (@authorID, @postID, @description, CURRENT_TIMESTAMP); ", dbConn))
                {
                    dbCommand.Parameters.Add("@authorID", SqlDbType.VarChar).Value = auteur.ID;
                    dbCommand.Parameters.Add("@postID", SqlDbType.VarChar).Value = post.PostID;
                    dbCommand.Parameters.Add("@description", SqlDbType.VarChar).Value = comment;

                    rowsaffected = dbCommand.ExecuteNonQuery();
                }

                dbConn.Close();

                return rowsaffected == 1 ? true : false;
            }
        }
    }
}
