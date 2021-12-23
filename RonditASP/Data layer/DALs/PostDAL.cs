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
    public class PostDAL : IPost
    {
        string connectionString = "Server=mssql.fhict.local;Database=dbi413096;User Id=dbi413096;Password=boyke7r7_;";

        public List<PostDTO> GetAllPosts()
        {
            List<PostDTO> posts = new List<PostDTO>();

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlCommand dbCommand = new SqlCommand("SELECT PostID, GebruikersID, Titel, Inhoud, Datum, (SELECT COALESCE(SUM(Vote),0) FROM PostUpvote WHERE PostID = Post.PostID) AS Punten, Gebruiker.ID, Gebruiker.gebruikersnaam, Gebruiker.email FROM Post INNER JOIN Gebruiker ON Post.GebruikersID = Gebruiker.ID", dbConn))
                {

                    using (SqlDataReader dr = dbCommand.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        foreach (DataRow dtr in dt.Rows)
                        {
                            int postID = int.Parse(dtr["PostID"].ToString());
                            int gebruikersID = int.Parse(dtr["GebruikersID"].ToString());
                            string title = dtr["Titel"].ToString();
                            string description = dtr["Inhoud"].ToString();
                            DateTime date = DateTime.Parse(dtr["Datum"].ToString());
                            int points = int.Parse(dtr["Punten"].ToString());
                            int vote = 0;

                            int userid = int.Parse(dtr["ID"].ToString());
                            string username = dtr["gebruikersnaam"].ToString();
                            string email = dtr["email"].ToString();

                            PostDTO post = new PostDTO(postID, userid, title, description, date, points, vote);

                            posts.Add(post);
                        }
                    }


                }
                dbConn.Close();

                return posts;
            }
        }

        public List<PostUpvoteDTO> GetAllUserVotes(UserDTO user)
        {
            List<PostUpvoteDTO> upvotes = new List<PostUpvoteDTO>();

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlCommand dbCommand = new SqlCommand("SELECT * FROM PostUpvote WHERE UserID = @userid", dbConn))
                {
                    dbCommand.Parameters.Add("@userID", SqlDbType.VarChar).Value = user.ID;

                    using (SqlDataReader dr = dbCommand.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        foreach (DataRow dtr in dt.Rows)
                        {
                            int PostID = int.Parse(dtr["PostID"].ToString());
                            int UserID = int.Parse(dtr["UserID"].ToString());
                            int vote = int.Parse(dtr["Vote"].ToString());


                            PostUpvoteDTO pud = new PostUpvoteDTO(UserID, PostID, vote);

                            upvotes.Add(pud);
                        }
                    }


                }
                dbConn.Close();

                return upvotes;
            }
        }


        public bool CreatePost(PostDTO post)
        {
            int rowsaffected = 0;

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlCommand dbCommand = new SqlCommand($"INSERT INTO dbi413096.dbo.Post(GebruikersID, Titel, Inhoud, Datum) VALUES(@userID, @title, @description, CURRENT_TIMESTAMP); ", dbConn))
                {


                    dbCommand.Parameters.Add("@userID", SqlDbType.VarChar).Value = post.UserID;
                    dbCommand.Parameters.Add("@title", SqlDbType.VarChar).Value = post.Title;
                    dbCommand.Parameters.Add("@description", SqlDbType.VarChar).Value = post.Description;

                    rowsaffected = dbCommand.ExecuteNonQuery();
                }

                string queryActiviteit = "INSERT INTO Activiteit (GebruikersID, Bericht, Datum) SELECT Gebruiker.ID, Gebruiker.gebruikersnaam + ' heeft een post gemaakt ', CURRENT_TIMESTAMP FROM Gebruiker WHERE ID = @userid;";


                using (SqlCommand dbCommandActiviteit = new SqlCommand(queryActiviteit, dbConn))
                {
                    dbCommandActiviteit.Parameters.Add("@userid", SqlDbType.VarChar).Value = post.UserID;

                    int rowsaffectedActiviteit = dbCommandActiviteit.ExecuteNonQuery();
                }

                dbConn.Close();

                return rowsaffected == 1 ? true : false;
            }
        }

        public PostDTO GetPostByID(int id)
        {
            PostDTO post;

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlCommand dbCommand = new SqlCommand("SELECT PostID, GebruikersID, Titel, Inhoud, Datum, (SELECT COALESCE(SUM(Vote),0) FROM PostUpvote WHERE PostID = Post.PostID) AS Punten, Gebruiker.ID, Gebruiker.gebruikersnaam, Gebruiker.email FROM Post INNER JOIN Gebruiker ON Post.GebruikersID = Gebruiker.ID WHERE PostID = @postID;", dbConn))
                {
                    dbCommand.Parameters.Add("@postID", SqlDbType.VarChar).Value = id;

                    using (SqlDataReader dr = dbCommand.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        int postID = int.Parse(dt.Rows[0]["PostID"].ToString());
                        int gebruikersID = int.Parse(dt.Rows[0]["GebruikersID"].ToString());
                        string title = dt.Rows[0]["Titel"].ToString();
                        string description = dt.Rows[0]["Inhoud"].ToString();
                        DateTime date = DateTime.Parse(dt.Rows[0]["Datum"].ToString());
                        int points = int.Parse(dt.Rows[0]["Punten"].ToString());
                        int vote = 0;

                        int userid = int.Parse(dt.Rows[0]["ID"].ToString());
                        string username = dt.Rows[0]["gebruikersnaam"].ToString();
                        string email = dt.Rows[0]["email"].ToString();

                        post = new PostDTO(postID, userid, title, description, date, points, vote);
                    }


                }
                dbConn.Close();

                return post;
            }
        }

        public bool RemovePost(PostDTO post)
        {
            int rowsaffected = 0;

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlCommand dbCommand = new SqlCommand($"DELETE FROM dbi413096.dbo.PostUpvote WHERE  PostID=@postID; DELETE FROM dbi413096.dbo.Post WHERE  PostID = @postID;", dbConn))
                {
                    dbCommand.Parameters.Add("@postID", SqlDbType.VarChar).Value = post.PostID;

                    rowsaffected = dbCommand.ExecuteNonQuery();
                }

                dbConn.Close();

                if (rowsaffected == 1 || rowsaffected == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        void MaakActiviteit(int vote, SqlConnection dbConn, PostDTO post, UserDTO user)
        {
            string query = "INSERT INTO Activiteit (GebruikersID, Bericht, Datum) VALUES (@userid, 'heeft iets gedaan, CURRENT_TIMESTAMP);";

            if (vote == +1)
            {
                query = "INSERT INTO Activiteit (GebruikersID, Bericht, Datum) SELECT Gebruiker.ID, Gebruiker.gebruikersnaam + ' heeft post ' + @postid + ' geupvote' , CURRENT_TIMESTAMP FROM Gebruiker WHERE ID = @userid;";
            }
            else if (vote == -1)
            {
                query = "INSERT INTO Activiteit (GebruikersID, Bericht, Datum) SELECT Gebruiker.ID, Gebruiker.gebruikersnaam + ' heeft post ' + @postid + ' gedownvote' , CURRENT_TIMESTAMP FROM Gebruiker WHERE ID = @userid;";
            }            

            using (SqlCommand dbCommandActiviteit = new SqlCommand(query, dbConn))
            {
                dbCommandActiviteit.Parameters.Add("@postid", SqlDbType.VarChar).Value = post.PostID;
                dbCommandActiviteit.Parameters.Add("@userid", SqlDbType.VarChar).Value = user.ID;

                int rowsaffected = dbCommandActiviteit.ExecuteNonQuery();
            }
        }

        public bool UpdatePost(UserDTO user, PostDTO post, int vote)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                int result = 0;
                string query;
                int rowsaffected = 0;

                dbConn.Open();

                using (SqlCommand dbCommand = new SqlCommand($"SELECT ISNULL((SELECT Vote FROM PostUpvote WHERE PostID=@postid AND UserID=@userid), 0) AS Vote", dbConn))
                {
                    dbCommand.Parameters.Add("@postid", SqlDbType.VarChar).Value = post.PostID;
                    dbCommand.Parameters.Add("@userid", SqlDbType.VarChar).Value = user.ID;

                    //Check if post is upvoted, downvoted or doesn't exist yet
                    result = (Int32)dbCommand.ExecuteScalar();                  

                    if (result == 1 && vote == 1)
                    {
                        query = $"DELETE FROM PostUpvote WHERE PostID=@postid AND UserID=@userid";
                        //Post is al geupvote en moet geneutraliseerd worden

                        MaakActiviteit(vote, dbConn, post, user);
                    }
                    else if (result == -1 && vote == -1)
                    {
                        query = $"DELETE FROM PostUpvote WHERE PostID=@postid AND UserID=@userid";
                        //Post is al gedownvote en moet geneutraliseerd worden

                        MaakActiviteit(vote, dbConn, post, user);
                    }
                    else if (result == 1 && vote == -1)
                    {
                        query = $"UPDATE PostUpvote SET Vote = -1 where PostID=@postid AND UserID=@userid";
                        //Je hebt geupvote maar wil nu downvoten

                        MaakActiviteit(vote, dbConn, post, user);
                    }
                    else if (result == -1 && vote == 1)
                    {
                        query = $"UPDATE PostUpvote SET Vote = 1 where PostID=@postid AND UserID=@userid";
                        //Je hebt gedownvote maar wil nu upvoten

                        MaakActiviteit(vote, dbConn, post, user);
                    }
                    else //0
                    {
                        query = $"INSERT INTO PostUpvote(PostID, UserID, Vote) VALUES(@postid, @userid, @vote)";
                        //Je hebt nog niet geupvote en je wil upvoten of downvoten

                        MaakActiviteit(vote, dbConn, post, user);

                    }
                  
                }

                using (SqlCommand dbCommand = new SqlCommand(query, dbConn))
                {
                    dbCommand.Parameters.Add("@postid", SqlDbType.VarChar).Value = post.PostID;
                    dbCommand.Parameters.Add("@userid", SqlDbType.VarChar).Value = user.ID;
                    dbCommand.Parameters.Add("@vote", SqlDbType.VarChar).Value = vote;

                    rowsaffected = dbCommand.ExecuteNonQuery();
                }

                

                dbConn.Close();

                return rowsaffected == 1 ? true : false;              
            }
        }
    }
}
