using Chhipa_Motors.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chhipa_Motors.DL
{
    public class CarDL
    {
        DBConnection _dbCon;
        public CarDL()
        {
            _dbCon = new DBConnection();    
        }
        public List<CarDTO> GetCarsByManufacturer(string manufacturer)
        {
            List<CarDTO> cars = new List<CarDTO>();

            try
            {
                string query = "SELECT CarID, CarName, Manufacturer, ModelYear, Price, Stock, Active " +
                              "FROM Cars WHERE Manufacturer = @Manufacturer";
                
                _dbCon.OpenConnection();
                SqlCommand cmd = new SqlCommand(query, _dbCon.Con);
                cmd.Parameters.AddWithValue("@Manufacturer", manufacturer);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CarDTO car = new CarDTO
                    {
                        CarID = reader["CarID"].ToString(),
                        CarName = reader["CarName"].ToString(),
                        Make = reader["Manufacturer"].ToString(),
                        ModelYear = reader["ModelYear"] != DBNull.Value ? reader["ModelYear"].ToString() : "",
                        Price = reader["Price"].ToString(),
                        Stock = reader["Stock"].ToString(),
                        Status = reader["Active"] != DBNull.Value ? (Convert.ToBoolean(reader["Active"]) ? "Active" : "Inactive") : "Active"
                    };

                    cars.Add(car);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching cars: {ex.Message}");
            }
            finally
            {
                _dbCon.CloseConnection();
            }

            return cars;
        }

    }
}
