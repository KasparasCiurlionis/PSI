using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using WebProject.Data;

namespace WebProject.Business_logic
{
    public class GasStationsInformation
    {
        private readonly static string ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        public static List<GasStations> GetGasStations()
        {
            List<GasStations> gasStations = new List<GasStations>();
            string query = "SELECT GasStationID, GasStationName FROM GasStations";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        gasStations.Add(new GasStations(null, reader["GasStationName"].ToString(), Convert.ToInt32(reader["GasStationID"].ToString())));
                    }
                    reader.Close();
                    return gasStations;

                }
                catch (Exception ex)
                {
                    ExceptionLogger.log<Exception>(ex);
                    throw ex;
                }
            }
        }

        // get selected gas station's primary key
        public static int GetGasStationID(string GasStationName)
        {
            int GasStationID = 0;
            string query = "SELECT GasStationID FROM GasStations WHERE GasStationName = @GasStationName";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GasStationName", GasStationName);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        GasStationID = Convert.ToInt32(reader["GasStationID"].ToString());
                    }
                    reader.Close();
                    return GasStationID;

                }
                catch (Exception ex)
                {
                    ExceptionLogger.log<Exception>(ex);
                    throw ex;
                }
            }
        }

        public static List<int> GetGasTypesID(string[] gasTypes)
        {
            List<int> gasTypesID = new List<int>();
            string query = "SELECT GasTypeID FROM GasTypes WHERE GasTypeName = @Gas_Type_Name";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Gas_Type_Name", gasTypes.ToString());
                try
                {
                    foreach (string current in gasTypes)
                    {
                        command.Parameters["@Gas_Type_Name"].Value = current;
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            gasTypesID.Add(Convert.ToInt32(reader["GasTypeID"].ToString()));
                        }
                        reader.Close();
                        connection.Close();
                    }
                    return gasTypesID;
                }
                catch (Exception ex)
                {
                    ExceptionLogger.log<Exception>(ex);
                    throw ex;
                }
            }

        }

        public static GasStations GetGasStationLocations(string GasStationName, int GasStationID)
        {
            // we know that GasStations contains a place for the name, id and array for locations
            // lets store one name, one id and then proceed for locations with their id's
            GasStations gasStations = new GasStations(null, GasStationName, GasStationID);
            string query = "SELECT LocationID, LocationName FROM Locations WHERE GasStationID = @GasStationID";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GasStationID", GasStationID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<GasStation> gasStationLocations = new List<GasStation>();
                    while (reader.Read())
                    {
                        gasStationLocations.Add(new GasStation(reader["LocationName"].ToString(), null, new Coords(0, 0), Convert.ToInt32(reader["LocationID"].ToString())));
                    }

                    reader.Close();
                    gasStations.setStations(gasStationLocations.ToArray());
                    return gasStations;

                }
                catch (Exception ex)
                {
                    ExceptionLogger.log<Exception>(ex);
                    throw ex;
                }
            }
        }

        // get LocationID
        public static int GetGasStationLocationID(string LocationName)
        {
            int LocationID = 0;
            string query = "SELECT LocationID FROM Locations WHERE LocationName = @LocationName";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocationName", LocationName);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        LocationID = Convert.ToInt32(reader["LocationID"].ToString());
                    }
                    reader.Close();
                    return LocationID;

                }
                catch (Exception ex)
                {
                    ExceptionLogger.log<Exception>(ex);
                    throw ex;
                }
            }
        }
    }
}