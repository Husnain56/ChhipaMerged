using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chhipa_Motors.DTO;
using Microsoft.Data.SqlClient;

namespace Chhipa_Motors.DL
{
    public class BookingDL
    {
        DBConnection _dbCon;
        public BookingDL() { 
            _dbCon = new DBConnection();
        }

        public int addBooking(BookingDTO bookDTO)
        {
            try
            { 
                _dbCon.OpenConnection();
                string query = "INSERT INTO Bookings (UserID, CarID, BookingDate, Status, UpdatedAt) " +
                               "VALUES (@UserID, @CarID, @BookingDate, @Status,@UpdatedAt);";

                SqlCommand com = new SqlCommand(query, _dbCon.Con);
                com.Parameters.AddWithValue("@UserID", bookDTO.UserID);
                com.Parameters.AddWithValue("@CarID", bookDTO.CarID);
                com.Parameters.AddWithValue("@BookingDate", DateTime.Now);
                com.Parameters.AddWithValue("@Status", bookDTO.Status);
                com.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                return com.ExecuteNonQuery();
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
    }
}
