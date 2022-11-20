using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebProject.Data;

namespace WebProject.Business_logic
{
    public class RetrieveGasStationLocations
    {
        private readonly static string ConnectionString =
        ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        public static GasStations getGasStationLocations(string GasStationName , int GasStationID)
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
                        gasStationLocations.Add(new GasStation(reader["LocationName"].ToString(), null, new Coords(0,0) , Convert.ToInt32(reader["LocationID"].ToString())));
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
        public static int getGasStationLocationID(string LocationName)
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