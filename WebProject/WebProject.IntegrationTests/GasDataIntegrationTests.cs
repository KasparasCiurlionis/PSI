using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using WebApplication1;
using WebApplication1.Controllers;
using WebApplication1.Data;

namespace WebProject.IntegrationTests
{
    public class GasDataIntegrationTests
    {
        //testing if a copy of our database works when we are inserting and retrieving info
        [Fact]
        public void IntegrationTestBase_MockDatabaseRetrieval()
        {
            string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\kuora\\Desktop\\New folder\\PSI\\WebProject\\WebProject.IntegrationTests\\MockDatabase.mdf\";Integrated Security = True";
            var connection = new SqlConnection(ConnectionString);

            string sqlString = @"select GasStationName from GasStations WHERE GasStationName IS NOT NULL";
            SqlCommand command = new SqlCommand(sqlString, connection);
            connection.Open();

            IAsyncResult result = command.BeginExecuteReader();
            SqlDataReader reader = command.EndExecuteReader(result);

            List<string> res = new List<string>();
            while (reader.Read())
            {
                res.Add(reader.GetValue(0).ToString());
            }

            connection.Close();

            Assert.Equal("Circle_K", res[1]);
        }

        [Fact]
        public void IntegrationTestBase_MockDatabaseInsertion()
        {
            string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\kuora\\Desktop\\New folder\\PSI\\WebProject\\WebProject.IntegrationTests\\MockDatabase.mdf\";Integrated Security = True";
            SqlConnection connection = new SqlConnection(ConnectionString);

            string sqlString = "UPDATE Prices SET Price = @GasPrice, DateModified = '2000-11-22 16:19:01' WHERE LocationID = 1 AND GasTypeID = 1";
            SqlCommand command = new SqlCommand(sqlString, connection);

            command.Parameters.AddWithValue("@GasPrice", Math.Round((float)9.123, 3));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            //retrieving newly inserted data
            string sqlString2 = @"SELECT Price FROM Prices WHERE LocationID = 1 AND GasTypeID = 1";

            SqlCommand command2 = new SqlCommand(sqlString2, connection);
            connection.Open();

            IAsyncResult result = command2.BeginExecuteReader();
            SqlDataReader reader = command2.EndExecuteReader(result);

            float res = 0;
            if (reader.Read())
            {
                res = Convert.ToSingle(reader.GetValue(0));
            }

            connection.Close();
            float price = (float)9.123;

            Assert.Equal(price, res);
        }

        //testing GasData class
        [Fact]
        public void CreatePrices_CheckDataCorrectness()
        {
            string LocationName = "Gelezinio Vilko g. 2A";

            string[] prices = GasData.createPrices(LocationName);
            Regex rx = new Regex(@"(\d\.\d{3}?){1}$");

            foreach(var price in prices)
            {
                if (price == null)
                {
                    Assert.Null(price);
                }
                else
                {
                    Assert.Matches(rx, price);
                }
            }
        }
    }
}
