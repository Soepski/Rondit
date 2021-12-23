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
    public class UserDAL : IUser
    {
        readonly string connectionString = "Server=mssql.fhict.local;Database=dbi413096;User Id=dbi413096;Password=boyke7r7_;";

        public List<UserDTO> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserDTO LoginUser(UserDTO user)
        {
            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand dbCommand = new SqlCommand($"SELECT COUNT(*) FROM dbo.Gebruiker WHERE gebruikersnaam = @username AND wachtwoord = @password", dbConn))
                        {
                            dbCommand.Transaction = dbTrans;

                            dbCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = user.Username;
                            dbCommand.Parameters.Add("@password", SqlDbType.VarChar).Value = user.Password;

                            int rowcount = int.Parse(dbCommand.ExecuteScalar().ToString());

                            if (rowcount == 1)
                            {
                                using (SqlCommand dbCommandUser = new SqlCommand($"SELECT ID, gebruikersnaam, email, rol from dbo.Gebruiker WHERE gebruikersnaam = @u", dbConn))
                                {
                                    dbCommandUser.Transaction = dbTrans;

                                    dbCommandUser.Parameters.Add("@u", SqlDbType.VarChar).Value = user.Username;

                                    using (SqlDataReader dr = dbCommandUser.ExecuteReader())
                                    {
                                        DataTable dt = new DataTable();
                                        dt.Load(dr);

                                        int ID = int.Parse(dt.Rows[0]["ID"].ToString());
                                        string usernamedto = dt.Rows[0]["gebruikersnaam"].ToString();
                                        string email = dt.Rows[0]["email"].ToString();
                                        string role = dt.Rows[0]["rol"].ToString();

                                        user = new UserDTO(ID, usernamedto, email, role);
                                    }
                                }
                            }
                            else
                            {
                                user = null;
                            }
                        }

                        dbTrans.Commit();
                    }
                    catch (SqlException)
                    {
                        dbTrans.Rollback();

                        throw; // bubble up the exception and preserve the stack trace
                    }
                }

                dbConn.Close();

                return user;
            }
        }

        public UserDTO GetUser(int gebruikersid)
        {
            UserDTO user = new UserDTO();

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("SELECT ID, gebruikersnaam, email, rol FROM Gebruiker WHERE ID = @ID", dbConn))
                    {
                        dbCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = gebruikersid;

                        using (SqlDataReader dr = dbCommand.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dr);

                            foreach (DataRow dtr in dt.Rows)
                            {

                                int userid = int.Parse(dtr["ID"].ToString());
                                string username = dtr["gebruikersnaam"].ToString();
                                string email = dtr["email"].ToString();
                                string role = dt.Rows[0]["rol"].ToString();

                                user = new UserDTO(userid, username, email, role);
                            }
                        }
                    }
                }
                catch (SqlException)
                {
                    throw; // bubble up the exception and preserve the stack trace
                }

                dbConn.Close();

            }

            return user;
        }

        public UserDTO GetUser(string gebruikersnaam)
        {
            UserDTO user = new UserDTO();

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("SELECT ID, gebruikersnaam, email, rol FROM Gebruiker WHERE Gebruikersnaam = @gebruikersnaam", dbConn))
                    {
                        dbCommand.Parameters.Add("@gebruikersnaam", SqlDbType.VarChar).Value = gebruikersnaam;

                        using (SqlDataReader dr = dbCommand.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dr);

                            foreach (DataRow dtr in dt.Rows)
                            {

                                int userid = int.Parse(dtr["ID"].ToString());
                                string username = dtr["gebruikersnaam"].ToString();
                                string email = dtr["email"].ToString();
                                string role = dt.Rows[0]["rol"].ToString();

                                user = new UserDTO(userid, username, email, role);
                            }
                        }
                    }
                }
                catch (SqlException)
                {
                    throw; // bubble up the exception and preserve the stack trace
                }

                dbConn.Close();

            }

            return user;
        }

        public bool CreateUser(string username, string email, string password)
        {
            int rowsaffected = 0;

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand dbCommand = new SqlCommand($"INSERT INTO dbi413096.dbo.Gebruiker(Gebruikersnaam, Email, Wachtwoord, rol) VALUES(@username, @email, @password, 'user'); ", dbConn))
                        {
                            dbCommand.Transaction = dbTrans;

                            dbCommand.Parameters.Add("username", SqlDbType.VarChar).Value = username;
                            dbCommand.Parameters.Add("email", SqlDbType.VarChar).Value = email;
                            dbCommand.Parameters.Add("password", SqlDbType.VarChar).Value = password;

                            rowsaffected = dbCommand.ExecuteNonQuery();
                        }

                        dbTrans.Commit();
                    }
                    catch (SqlException)
                    {
                        dbTrans.Rollback();

                        throw; // bubble up the exception and preserve the stack trace
                    }
                }

                dbConn.Close();

                return rowsaffected == 1 ? true : false;
            }
        }

        public bool RemoveUser()
        {
            throw new NotImplementedException();
        }

        public bool FollowUser(UserDTO follower, UserDTO followed)
        {
            int rowsaffected = 0;

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand dbCommand = new SqlCommand($"IF EXISTS (SELECT * FROM Follow WHERE GebruikerID = @followerid AND VolgID = @followedid) BEGIN SELECT 1 AS Result END ELSE BEGIN INSERT INTO Follow(GebruikerID, VolgID) VALUES(@followerid, @followedid) END", dbConn))
                        {
                            dbCommand.Transaction = dbTrans;

                            dbCommand.Parameters.Add("followerid", SqlDbType.VarChar).Value = follower.ID;
                            dbCommand.Parameters.Add("followedid", SqlDbType.VarChar).Value = followed.ID;

                            rowsaffected = dbCommand.ExecuteNonQuery();
                        }

                        dbTrans.Commit();
                    }
                    catch (SqlException)
                    {
                        dbTrans.Rollback();

                        throw; // bubble up the exception and preserve the stack trace
                    }
                }

                dbConn.Close();

                return rowsaffected == 1 ? true : false;
            }
        }

        public bool UnfollowUser(UserDTO follower, UserDTO followed)
        {
            int rowsaffected = 0;

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand dbCommand = new SqlCommand($"DELETE FROM Follow WHERE  GebruikerID = @followerid AND VolgID = @followedid ;", dbConn))
                        {
                            dbCommand.Transaction = dbTrans;

                            dbCommand.Parameters.Add("followerid", SqlDbType.VarChar).Value = follower.ID;
                            dbCommand.Parameters.Add("followedid", SqlDbType.VarChar).Value = followed.ID;

                            rowsaffected = dbCommand.ExecuteNonQuery();
                        }

                        dbTrans.Commit();
                    }
                    catch (SqlException)
                    {
                        dbTrans.Rollback();

                        throw; // bubble up the exception and preserve the stack trace
                    }
                }

                dbConn.Close();

                return rowsaffected == 1 ? true : false;
            }
        }

        public bool CheckFollow(UserDTO follower, UserDTO followed)
        {
            int result; 

            using (SqlConnection dbConn = new SqlConnection(connectionString))
            {
                dbConn.Open();

                using (SqlTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand dbCommand = new SqlCommand($"IF EXISTS (SELECT * FROM Follow WHERE GebruikerID = @followerid AND VolgID = @followedid) BEGIN SELECT 1 AS Result END ELSE BEGIN SELECT 0 AS Result END", dbConn))
                        {
                            dbCommand.Transaction = dbTrans;

                            dbCommand.Parameters.Add("followerid", SqlDbType.VarChar).Value = follower.ID;
                            dbCommand.Parameters.Add("followedid", SqlDbType.VarChar).Value = followed.ID;

                            result = (Int32)dbCommand.ExecuteScalar();
                        }

                        dbTrans.Commit();
                    }
                    catch (SqlException)
                    {
                        dbTrans.Rollback();

                        throw; // bubble up the exception and preserve the stack trace
                    }
                }

                dbConn.Close();

                return result == 1 ? true : false;
            }
        }
    }
}
