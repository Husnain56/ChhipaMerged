using Chhipa_Motors.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Chhipa_Motors.DL
{
    public class CustomerDL
    {
        private DBConnection _dbCon;
        public CustomerDL()
        {
            _dbCon = new DBConnection();
        }

        public DataTable FetchBookings(UserDTO userDTO)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "SELECT Bookings.BookingID, Bookings.CarID, Cars.CarName, Cars.Manufacturer, Bookings.BookingDate, Bookings.Status, Bookings.UpdatedAt, Bookings.AdminNote FROM Bookings " +
                     "INNER JOIN Cars ON Bookings.CarID = Cars.CarID WHERE Bookings.UserID = @UserID ORDER BY Bookings.BookingDate DESC;";

                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@UserID", userDTO.Id);
                SqlDataReader reader = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbCon.CloseConnection();
            }
        }
        public DataTable FetchPurchasedCars(UserDTO userDTO)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "SELECT Sales.SaleID, Sales.CarID, Cars.CarName, Cars.Manufacturer, Sales.SaleDate, Sales.TotalAmount FROM Sales " +
                    "INNER JOIN Cars ON Sales.CarID = Cars.CarID WHERE Sales.UserID = @UserID ORDER BY Sales.SaleDate DESC;";

                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@UserID", userDTO.Id);
                SqlDataReader reader = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbCon.CloseConnection();
            }
        }
        public int cancelBooking(BookingDTO bookDTO)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "UPDATE Bookings SET Status = @Status,AdminNote = @AdminNote, UpdatedAt = @UpdatedAt WHERE BookingID = @BookingID;";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@Status", bookDTO.Status);
                com.Parameters.AddWithValue("@AdminNote", bookDTO.AdminNote);
                com.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                com.Parameters.AddWithValue("@BookingID", bookDTO.BookingID);
                int result = com.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbCon.CloseConnection();
            }
        }

        public int addCustomer(CustomerDTO custDTO)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "INSERT INTO Customers (CustomerID, FullName, Email, PhoneNumber, Address, City) " +
                               "VALUES (@CustomerID, @FullName, @Email, @PhoneNumber, @Address, @City);";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@CustomerID", custDTO.CustomerID);
                com.Parameters.AddWithValue("@FullName", custDTO.FullName);
                com.Parameters.AddWithValue("@Email", custDTO.Email);
                com.Parameters.AddWithValue("@PhoneNumber", custDTO.PhoneNumber);
                com.Parameters.AddWithValue("@Address", custDTO.Address);
                com.Parameters.AddWithValue("@City", custDTO.City);
                int result = com.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbCon.CloseConnection();
            }
        }
        public bool customerExists(CustomerDTO custDTO)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "SELECT COUNT(1) FROM Customers WHERE CustomerID = @CustomerID;";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@CustomerID", custDTO.CustomerID);
                int count = Convert.ToInt32(com.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbCon.CloseConnection();
            }
        }
        public int updateCustomer(CustomerDTO custDTO)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "UPDATE Customers SET FullName = @FullName, Email = @Email, PhoneNumber = @PhoneNumber, Address = @Address, City = @City " +
                               "WHERE CustomerID = @CustomerID;";
                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@FullName", custDTO.FullName);
                com.Parameters.AddWithValue("@Email", custDTO.Email);
                com.Parameters.AddWithValue("@PhoneNumber", custDTO.PhoneNumber);
                com.Parameters.AddWithValue("@Address", custDTO.Address);
                com.Parameters.AddWithValue("@City", custDTO.City);
                com.Parameters.AddWithValue("@CustomerID", custDTO.CustomerID);
                int result = com.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dbCon.CloseConnection();
            }
            
        }
        public bool IsCustomer(string userId)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "SELECT COUNT(*) FROM Customers WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, _dbCon.Con);
                cmd.Parameters.AddWithValue("@CustomerID", userId);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            finally
            {
                _dbCon.CloseConnection();
            }
        }

        public CustomerDTO GetCustomerInfo(string userId)
        {
            CustomerDTO customer = null;
            try
            {
                _dbCon.OpenConnection();
                string query = "SELECT CustomerID, FullName, Email, PhoneNumber, Address, City FROM Customers WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, _dbCon.Con);
                cmd.Parameters.AddWithValue("@CustomerID", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer = new CustomerDTO
                    {
                        CustomerID = reader["CustomerID"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        City = reader["City"].ToString()
                    };
                }
                reader.Close();
            }
            finally
            {
                _dbCon.CloseConnection();
            }
            return customer;
        }

        public int UpdateEmail(string userId, string newEmail)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "UPDATE Customers SET Email = @Email WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, _dbCon.Con);
                cmd.Parameters.AddWithValue("@Email", newEmail);
                cmd.Parameters.AddWithValue("@CustomerID", userId);

                return cmd.ExecuteNonQuery();
            }
            finally
            {
                _dbCon.CloseConnection();
            }
        }

        public int UpdatePhone(string userId, string newPhone)
        {
            try
            {
                _dbCon.OpenConnection();
                string query = "UPDATE Customers SET PhoneNumber = @PhoneNumber WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, _dbCon.Con);
                cmd.Parameters.AddWithValue("@PhoneNumber", newPhone);
                cmd.Parameters.AddWithValue("@CustomerID", userId);

                return cmd.ExecuteNonQuery();
            }
            finally
            {
                _dbCon.CloseConnection();
            }
        }
    }
}
