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

namespace WebProject
{
    public class GasData
    {
        private readonly static string ConnectionString =
    ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        public static List<GasStations> getData()
        {
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
                    // You might want to pass these errors
                    // back out to the caller.
                    Console.WriteLine("Error: {0}", ex.Message);
                }

                return gasStations;
            }
        }

        //read all station addreses from DB from a specific brand, create a GasStation for each of them, put them in a list
        static List<GasStation> createStation(string stationName)
        {
            List<GasStation> gasStation = new List<GasStation>();
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
                            gasStation.Add(new GasStation(reader.GetValue(0).ToString(), createPrices(reader.GetValue(0).ToString())));


                        }
                    }
                }
                catch (Exception ex)
                {
                    // You might want to pass these errors
                    // back out to the caller.
                    Console.WriteLine("Error: {0}", ex.Message);
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
                    // You might want to pass these errors
                    // back out to the caller.
                    Console.WriteLine("Error: {0}", ex.Message);
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
                    // You might want to pass these errors
                    // back out to the caller.
                    Console.WriteLine("Error: {0}", ex.Message);
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
                    // You might want to pass these errors
                    // back out to the caller.
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
            return gasTypes;
        }

        
    }
}



