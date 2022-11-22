using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;
using Newtonsoft.Json;
using WebProject.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http.Headers;
using Microsoft.Ajax.Utilities;

namespace WebProject
{
    public class GasData
    {
        private readonly static string ConnectionString =
    ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        private static IGasStation gasStaionInj;
        public static List<GasStations> getData(IGasStation gas)
        {
            gasStaionInj = gas;
            List<GasStations> data = createGasStations(); 

            return data;
        }

        //read all brand names from DB, create a GasStations for each of them, put them in a list
        static List<GasStations> createGasStations()
        {
            List<GasStations> gasStations = new List<GasStations>();
            using (var conn = new SqlConnection(ConnectionString))
            {
                string sqlString = @"select GasStationName from GasStations WHERE GasStationName IS NOT NULL";
                try
                {
                    SqlCommand command = new SqlCommand(sqlString, conn);

                    conn.Open();
                    IAsyncResult result = command.BeginExecuteReader();

                    using (SqlDataReader reader = command.EndExecuteReader(result))
                    {
                        while (reader.Read())
                        {

                            string test = reader.GetValue(0).ToString();
                            gasStations.Add(new GasStations(createStation(reader.GetValue(0).ToString()).ToArray(), reader.GetValue(0).ToString()));


                        }
                    }
                }
                catch (Exception ex)
                {
                    _ = ExceptionLogger.log<Exception>(ex);
                }

                return gasStations;
            }
        }

        //read all station addreses from DB from a specific brand, create a GasStation for each of them, put them in a list
        static List<IGasStation> createStation(string stationName)
        {
            List<IGasStation> gasStation = new List<IGasStation>();
            using (var conn = new SqlConnection(ConnectionString))
            {
                string sqlString = @"select LocationName from Locations, GasStations WHERE Locations.GasStationID = GasStations.GasStationID AND GasStationName = '" + stationName + "'";
                try
                {
                    SqlCommand command = new SqlCommand(sqlString, conn);

                    conn.Open();
                    IAsyncResult result = command.BeginExecuteReader();

                    using (SqlDataReader reader = command.EndExecuteReader(result))
                    {
                        while (reader.Read())
                        {
                            
                            string test = reader.GetValue(0).ToString();
                            var objStr = JsonConvert.SerializeObject(gasStaionInj);
                            GasStation station = JsonConvert.DeserializeObject<GasStation>(objStr);
                            station.setAddress(reader.GetValue(0).ToString());
                            station.setPrices(createPrices(reader.GetValue(0).ToString()));
                            gasStation.Add(station);


                        }
                    }
                }
                catch (Exception ex)
                {
                    _ = ExceptionLogger.log<Exception>(ex);
                }

                return gasStation;
            }
        }

        //read all gas prices from DB from a specific station, create a price array 
        static string[] createPrices(string LocationName)
        {
            
            List<String> gasTypes = getTypes();
            string[] prices = new string[gasTypes.Count()];
            using (var conn = new SqlConnection(ConnectionString))
            {
                string sqlString = @"select GasTypeName, Price from Prices, Locations, GasTypes WHERE Locations.LocationID = Prices.LocationID AND GasTypes.GasTypeID = Prices.GasTypeID AND LocationName = '" + LocationName + "'";
                try
                {
                    SqlCommand command = new SqlCommand(sqlString, conn);

                    conn.Open();
                    IAsyncResult result = command.BeginExecuteReader();


                    using (SqlDataReader reader = command.EndExecuteReader(result))
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < gasTypes.Count(); i++)
                            {
                                if (reader.GetValue(0).ToString() == gasTypes[i])
                                {
                                    prices[i] = reader.GetValue(1).ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _ = ExceptionLogger.log<Exception>(ex);
                }

                return prices;
            }
        }


        //get a list of gas types from DB
        public static List<String> getTypes()
        {
            List<String> gasTypes = new List<String>();
            using (var conn = new SqlConnection(ConnectionString))
            {
                string sqlString = @"select GasTypeName from GasTypes";
                try
                {
                    SqlCommand command = new SqlCommand(sqlString, conn);

                    conn.Open();
                    IAsyncResult result = command.BeginExecuteReader();

                    using (SqlDataReader reader = command.EndExecuteReader(result))
                    {
                        while (reader.Read())
                        {

                            for (int i = 0; i < reader.FieldCount; i++)
                                gasTypes.Add(reader.GetValue(i).ToString());


                        }
                    }
                }
                catch (Exception ex)
                {
                    _ = ExceptionLogger.log<Exception>(ex);
                }
            }
            return gasTypes;
        }

        //get a list of brand names from DB
        public static List<String> getBrand()
        {
            List<String> gasTypes = new List<String>();
            using (var conn = new SqlConnection(ConnectionString))
            {
                string sqlString = @"select GasStationName from GasStations";
                try
                {
                    SqlCommand command = new SqlCommand(sqlString, conn);

                    conn.Open();
                    IAsyncResult result = command.BeginExecuteReader();

                    using (SqlDataReader reader = command.EndExecuteReader(result))
                    {
                        while (reader.Read())
                        {

                            for (int i = 0; i < reader.FieldCount; i++)
                                gasTypes.Add(reader.GetValue(i).ToString());


                        }
                    }
                }
                catch (Exception ex)
                {
                    _ = ExceptionLogger.log<Exception>(ex);
                }
            }
            return gasTypes;
        }

        
    }
}



