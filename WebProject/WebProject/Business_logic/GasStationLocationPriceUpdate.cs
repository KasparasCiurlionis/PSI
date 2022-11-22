using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using WebProject.Data;

namespace WebProject.Business_logic
{
    public class GasStationLocationPriceUpdate
    {
        private readonly static string ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        // it should get: Key of Gas Station, Key of Location and array of prices and types of gas
        public static void UpdateGasStationLocationPrice(int GasStationID, int LocationID, List<int> GasType, List<float> GasPrice)
        {
            DateTime date = DateTime.Now;
            // do it but also update the date
            string query = "UPDATE Prices SET Price = @GasPrice, DateModified = @date WHERE LocationID = @LocationID AND GasTypeID = @GasTypeID";
            try {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    // lets iterate through the list of prices and types (make it one loop)
                    for (int i = 0; i < GasPrice.Count; i++)
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@LocationID", LocationID);
                        command.Parameters.AddWithValue("@GasTypeID", GasType[i]);
                        //command.Parameters.AddWithValue("@GasPrice", GasPrice[i]);
                        // add float price but with 3 decimal places
                        command.Parameters.AddWithValue("@GasPrice", Math.Round(GasPrice[i], 3));
                        command.Parameters.AddWithValue("@date", date);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Inner Exception: " + ex.Message);
                    Console.WriteLine("Query executed: " + query);
                    ExceptionLogger.log<SqlException>(ex);
                    connection.Close();
                } // updating database: failed, no purpose to run ''finally'' block
            }
            }
            catch (Exception ex)
            {
                ExceptionLogger.log<Exception>(ex);
                Console.WriteLine("Outer Exception: " + ex.Message);
            }
        }
        
    }
}