﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using WebProject.Data;


// if con string is:Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="C:\Users\olgie\Desktop\New folder (2)\WebProject\WebProject\App_Data\Database1.mdf";Integrated Security=True
// in web.config, it will look like this: <add name="ConnectionString" connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=&quot;C:\Users\olgie\Desktop\New folder (2)\WebProject\WebProject\App_Data\Database1.mdf&quot;;Integrated Security=True" providerName="System.Data.SqlClient" />
namespace WebProject.Business_logic
{
    public class RetrieveGasStations
    {
        private readonly static string ConnectionString =
            ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

    
            
        public static List<GasStations> getGasStations()
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
        public static int getGasStationID(string GasStationName)
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

    }
}