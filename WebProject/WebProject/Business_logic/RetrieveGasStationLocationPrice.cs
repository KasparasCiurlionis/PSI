using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebProject.Data;

namespace WebProject.Business_logic
{
    public class RetrieveGasStationLocationPrice
    {
        private readonly static string ConnectionString =
        ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        public static List<int> getGasTypesID(string[] gasTypes)
        {
            List<int> gasTypesID = new List<int>();
            string query = "SELECT GasTypeID FROM GasTypes WHERE GasTypeName = @Gas_Type_Name";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Gas_Type_Name", gasTypes.ToString());
                try
                {
                    foreach(string current in gasTypes)
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
                }
            }

        }
    }
}
/*
 * we are having some issues:
 * we need to select the gas types id if their name is equal to the gas types name that we have in the array
 * but we cannot do it because we have an array of strings and we cannot use it in the query
 * to solve this issue we need to use a loop to iterate through the array and select the gas types id
 */