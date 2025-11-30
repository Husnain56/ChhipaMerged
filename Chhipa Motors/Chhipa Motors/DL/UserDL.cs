using Chhipa_Motors.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chhipa_Motors.DL
{
    public class UserDL
    {
        private DBConnection _dbCon;

        public UserDL()
        {
            _dbCon = new DBConnection();
        }

        public UserDTO GetUserInfo(string userId)
        {
            UserDTO user = null;
            try
            {
                _dbCon.OpenConnection();
                string query = "SELECT UserID, Username FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, _dbCon.Con);
                cmd.Parameters.AddWithValue("@UserID", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new UserDTO
                    {
                        Id = reader["UserID"].ToString(),
                        Username = reader["Username"].ToString()
                    };
                }
                reader.Close();
            }
            finally
            {
                _dbCon.CloseConnection();
            }
            return user;
        }

        public bool VerifyPassword(UserDTO userDTO)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, _dbCon.Con);
                cmd.Parameters.AddWithValue("@UserID", userDTO.Id);
                cmd.Parameters.AddWithValue("@Password", userDTO.Password);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            finally
            {
                _dbCon.CloseConnection();
            }
        }

        public bool UsernameExists(string username)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, _dbCon.Con);
                cmd.Parameters.AddWithValue("@Username", username);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            finally
            {
                _dbCon.CloseConnection();
            }
        }

        public int UpdateUsername(string userId, string newUsername)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "UPDATE Users SET Username = @Username WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, _dbCon.Con);
                cmd.Parameters.AddWithValue("@Username", newUsername);
                cmd.Parameters.AddWithValue("@UserID", userId);

                return cmd.ExecuteNonQuery();
            }
            finally
            {
                _dbCon.CloseConnection();
            }
        }

        public int DeleteUser(string userId)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "DELETE FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, _dbCon.Con);
                cmd.Parameters.AddWithValue("@UserID", userId);

                return cmd.ExecuteNonQuery();
            }
            finally
            {
                _dbCon.CloseConnection();
            }
        }
    }
}
