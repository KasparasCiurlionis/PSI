using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
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

        public static GasStations getGasStationLocations(string GasStationName , int GasStationID, IGasStation iGasStation)
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
                    List<IGasStation> gasStationLocations = new List<IGasStation>();
                    while (reader.Read())
                    {
                        var objStr = JsonConvert.SerializeObject(iGasStation);
                        GasStation gasStation = JsonConvert.DeserializeObject<GasStation>(objStr); gasStation.setAddress(reader["LocationName"].ToString());
                        gasStation.setID(Convert.ToInt32(reader["LocationID"].ToString()));
                        gasStationLocations.Add(gasStation);
                        //gasStationLocations.Add(new GasStation(reader["LocationName"].ToString(), null, new Coords(0,0) , Convert.ToInt32(reader["LocationID"].ToString())));
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