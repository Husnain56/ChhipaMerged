using Chhipa_Motors.GUI.Admin_Panel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chhipa_Motors.DTO;
using System.Reflection.Metadata;

namespace Chhipa_Motors.DL
{
    public class AdminDL
    {
        private DBConnection _dbCon;
        public AdminDL()
        {
            _dbCon = new DBConnection();
        }

        public int CreateAdmin(UserDTO userDTO)
        {
            try
            {
                _dbCon.Con.Open();
                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@Username", userDTO.Username);
                com.Parameters.AddWithValue("@Password", userDTO.Password);
                com.Parameters.AddWithValue("@Role", userDTO.Role);
                int rowAffected = com.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the admin.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }

        public DataTable GetAdmins()
        {
            try
            {
                _dbCon.Con.Open();
                string query = "SELECT UserID,Username,Role FROM Users WHERE Role = 'Admin'";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                SqlDataReader reader = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving admins.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }

        }

        public DataTable GetUsers()
        {
            try
            {
                _dbCon.Con.Open();
                string query = "SELECT UserID,Username,Role FROM Users WHERE Role = 'User' AND UserID NOT IN (SELECT UserID FROM Customers);";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                SqlDataReader reader = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }

        public DataTable GetCustomers()
        {
            try
            {
                _dbCon.Con.Open();
                string query = "SELECT U.UserID, U.Username, C.FullName, C.Email, C.PhoneNumber, C.Address FROM Users U JOIN Customers C ON U.UserID = C.CustomerID WHERE U.Role = 'User';";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                SqlDataReader reader = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving customers.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public int DeleteUser(UserDTO userDTO)
        {
            try
            {
                _dbCon.Con.Open();
                string query = "DELETE FROM Users WHERE UserID = @UserID AND UserID NOT IN (SELECT CustomerID FROM Customers)";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@UserID", userDTO.Id);
                int rowAffected = com.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the user.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public DataTable GetAllCars()
        {
            try
            {
                _dbCon.Con.Open();
                string query = "SELECT CarID, CarName, Manufacturer, ModelYear, Price, Stock, Active as Status FROM Cars";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                SqlDataReader reader = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving cars.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public int updateCarPrice(CarDTO carDTO)
        {
            try
            {
                _dbCon.Con.Open();
                string query = "UPDATE Cars SET Price = @Price WHERE CarID = @CarID";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@Price", carDTO.Price);
                com.Parameters.AddWithValue("@CarID", carDTO.CarID);

                int rowAffected = com.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the car price.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public int updateCarStock(CarDTO carDTO)
        {
            try
            {
                _dbCon.Con.Open();
                string query = "UPDATE Cars SET Stock = @Stock WHERE CarID = @CarID";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@Stock", carDTO.Stock);
                com.Parameters.AddWithValue("@CarID", carDTO.CarID);
                int rowAffected = com.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the car stock.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public int changeCarStatus(CarDTO carDTO)
        {
            try
            {
                _dbCon.Con.Open();
                string query = "UPDATE Cars SET Active = @Status WHERE CarID = @CarID";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@Status", carDTO.Status);
                com.Parameters.AddWithValue("@CarID", carDTO.CarID);
                int rowAffected = com.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while changing the car status.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public DataTable getBookedCars()
        {
            try
            {
                _dbCon.Con.Open();
                string query = "Select * from Bookings";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                SqlDataReader reader = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving booked cars.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public int updateBookingStatus(BookingDTO bookDto)
        {
            try
            {
                _dbCon.Con.Open();
                string query = "UPDATE Bookings SET Status = @Status, AdminNote = @AdminNote WHERE BookingId = @BookingId";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@Status", bookDto.Status);
                com.Parameters.AddWithValue("@AdminNote", bookDto.AdminNote ?? (object)DBNull.Value); // Handle null AdminNote
                com.Parameters.AddWithValue("@BookingId", bookDto.BookingID);

                int rowAffected = com.ExecuteNonQuery();

                return rowAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the booking status.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public int decrementCarStock(BookingDTO b_dto)
        {
            try
            {
                _dbCon.Con.Open();
                string query = "UPDATE Cars SET Stock = Stock - 1 WHERE CarID = (Select CarID from Bookings WHERE BookingID = @BookingId)";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@BookingId", b_dto.BookingID);
                int rowAffected = com.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while decrementing the car stock.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public int insertSalesRecord(BookingDTO b_dto)
        {
            try
            {
                _dbCon.Con.Open();

                // Get both CarID and UserID from Bookings table
                string query = @"INSERT INTO Sales (CarID, UserID, SaleDate, TotalAmount) 
                SELECT 
                    b.CarID, 
                    b.UserID, 
                    GETDATE(), 
                    c.Price
                FROM Bookings b
                INNER JOIN Cars c ON b.CarID = c.CarID
                WHERE b.BookingID = @BookingId";

                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@BookingId", b_dto.BookingID);

                int rowAffected = com.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting the sales record.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public DataTable getDailySales()
        {
            try
            {
                _dbCon.Con.Open();
                string query = "SELECT Sales.SaleID, Sales.UserID, Users.Username, Sales.CarID, Cars.Manufacturer, Cars.CarName, Sales.SaleDate, Sales.TotalAmount FROM Sales " +
                    "INNER JOIN Users ON Sales.UserID = Users.UserID " +
                    "INNER JOIN Cars ON Sales.CarID = Cars.CarID WHERE CAST(Sales.SaleDate AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) ORDER BY Sales.SaleDate DESC;";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                SqlDataReader reader = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving daily sales.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public DataTable getWeeklySales()
        {
            try
            {
                _dbCon.Con.Open();
                string query = "SELECT Sales.SaleID, Sales.UserID, Users.Username, Sales.CarID, Cars.Manufacturer, Cars.CarName, Sales.SaleDate, Sales.TotalAmount FROM Sales " +
                    "INNER JOIN Users ON Sales.UserID = Users.UserID " +
                    "INNER JOIN Cars ON Sales.CarID = Cars.CarID WHERE Sales.SaleDate >= DATEADD(DAY, -7, GETDATE()) ORDER BY Sales.SaleDate DESC;";

                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                SqlDataReader reader = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving weekly sales.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public DataTable getMonthlySales()
        {
            try
            {
                _dbCon.Con.Open();
                string query = "SELECT Sales.SaleID, Sales.UserID, Users.Username, Sales.CarID, Cars.Manufacturer, Cars.CarName, Sales.SaleDate, Sales.TotalAmount FROM Sales " +
                    "INNER JOIN Users ON Sales.UserID = Users.UserID " +
                    "INNER JOIN Cars ON Sales.CarID = Cars.CarID WHERE Sales.SaleDate >= DATEADD(MONTH, -1, GETDATE()) ORDER BY Sales.SaleDate DESC;";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                SqlDataReader reader = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving Monthly sales.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
        public DataTable getAllTimeSales()
        {
            try
            {
                _dbCon.Con.Open();
                string query = "SELECT Sales.SaleID, Sales.UserID, Users.Username, Sales.CarID, Cars.Manufacturer, Cars.CarName, Sales.SaleDate, Sales.TotalAmount FROM Sales " +
                    "INNER JOIN Users ON Sales.UserID = Users.UserID INNER JOIN Cars ON Sales.CarID = Cars.CarID ORDER BY Sales.SaleDate DESC;";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                SqlDataReader reader = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all-time sales.", ex);
            }
            finally
            {
                _dbCon.Con.Close();
            }
        }
    }
}
