using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Business_logic;
using System.Diagnostics;

namespace WebProjectTests
{
    [TestClass()]
    public class dbContextTests
    {
        private RetrieveGasStations _retrieveGasStations { get; set; } = null;
        private RetrieveGasStationLocations _retrieveGasStationLocations { get; set; } = null;
        private RetrieveGasStationLocationPrice _retrieveGasStationLocationPrice { get; set; } = null;
        private UpdateGasStationLocationPrice _updateGasStationLocationPrice { get; set; } = null;
        private GasData _gasData { get; set; } = null;

        [TestInitialize]
        public void Setup()
        {
            _retrieveGasStations = new RetrieveGasStations();
            _retrieveGasStationLocations = new RetrieveGasStationLocations();
            _retrieveGasStationLocationPrice = new RetrieveGasStationLocationPrice();
            _updateGasStationLocationPrice = new UpdateGasStationLocationPrice();
            _gasData = new GasData();
        }

        //        public static string[] createPrices(string LocationName)

        [TestMethod()]
        public void createPricesTest()
        {
            //Assign
            string LocationName = "P. Lukiskio g. 22";
            /*1.543, 1.199, 1.309, 1.264*/ // it should consist of 6 prices, these 4 and 2 other null
            string[] expected = { "1.543", "1.199", "1.309", "1.264", null, null };


            //Act
            string[] result = GasData.createPrices(LocationName);

            //Assert
            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }

        [TestMethod()]
        public void getDataTest()
        {
            //Assign
            var gasStation = _retrieveGasStations;
            List<String> ExpectedOutput = new List<string>();
            ExpectedOutput.Add("Circle_K");
            ExpectedOutput.Add("Neste");
            ExpectedOutput.Add("Viada");
            ExpectedOutput.Add("Baltic_Petroleum");
            ExpectedOutput.Add("Alauša");


            //Act
            var result = GasData.getData(new GasStation());

            //Assert
            foreach (var item in result)
            {
                Assert.IsTrue(ExpectedOutput.Contains(item.getName()));
            }
        }
        [TestMethod()]
        public void updateGasStationLocationPriceTestAndRetrieveFirstPrice()
        {
            //Assign
            int gasStationID = 1;
            int locationID = 1;
            List<int> gasType = new List<int>() { 1 };
            List<float> gasPrice = new List<float>() { 1.543f};

            //Act
            UpdateGasStationLocationPrice.updateGasStationLocationPrice(gasStationID, locationID, gasType, gasPrice);
            // select if the price was updated
            var rez = RetrieveGasStationLocationPrice.retrieveGasStationLocationPrice(locationID, 1);

            //Assert
            Assert.IsTrue(rez.Equals(1.543f));
        }
        [TestMethod()]
        public void getGasTypesIDTest()
        {
            //Assign
            string[] gasTypes = { "95", "98", "D", "D_PRO" };
            

            //Act
            var result = RetrieveGasStationLocationPrice.getGasTypesID(gasTypes);

            //Assert
            Assert.IsTrue(result.Count == 4);
            // assert if the result is 1 2 3 and 4
            Assert.IsTrue(result[0] == 1);
            Assert.IsTrue(result[1] == 2);
            Assert.IsTrue(result[2] == 3);
            Assert.IsTrue(result[3] == 4);
        }
        [TestMethod()]
        public void getGasStationLocationIDTest_LocationNameExists_LocationIDIsReturned()
        {
            //Assign
            string locationName = "P. Lukiskio g. 22";

            //Act
            var result = RetrieveGasStationLocations.getGasStationLocationID(locationName);

            //Assert
            Assert.IsTrue(result == 1);
        }
        [TestMethod()]
        public void getGasStationLocationsTest_CircleKGasStation_StatusIsRecieved()
        {
            //Assign
            string gasStation = "Circle_K";
            int gasStationID = 1;

            List<string> expected = new List<string>();
            expected.Add("P. Lukiskio g. 22");
            expected.Add("Kedru g. 2");
            expected.Add("Gelezinio Vilko g. 63");
            expected.Add("Gelezinio Vilko g. 37A");
            expected.Add("Zirmunu g. 54");
            expected.Add("Subaciaus g. 64");
            expected.Add("Buivydiskiu g. 5");
            expected.Add("Kauno g. 26");
            


            //Act
            var obj = RetrieveGasStationLocations.getGasStationLocations(gasStation, gasStationID, new GasStation());
            foreach(var item in obj.getStations())
            {
                Debug.WriteLine(item.getAddress());

            }
            //Assert
            foreach (var item2 in obj.getStations())
            {
                Assert.IsTrue(expected.Contains(item2.getAddress()));
            }
        
    }
        [TestMethod()]
        public void getGasStationID_RetrieveViadaGasStationId()
        {
            //Assign
            string gasStation = "Viada";

            //Act
            var result = RetrieveGasStations.getGasStationID(gasStation);

            //Assert
            Assert.IsTrue(result.Equals(3));
        }
        [TestMethod()]
        public void getGasStationsTest_RetrieveAllStations()
        {
            // that is integration testing

            //Neste 1
            //Circle_K 2
            //Viada 3
            //Baltic_Petroleum 4
            //Alauša 5
            //Arrange
            List<GasStations> expectedOutputFromQuery = new List<GasStations>();

            expectedOutputFromQuery.Add(new GasStations(null, "Neste", 1));
            expectedOutputFromQuery.Add(new GasStations(null, "Circle_K", 2));
            expectedOutputFromQuery.Add(new GasStations(null, "Viada", 3));
            expectedOutputFromQuery.Add(new GasStations(null, "Baltic_Petroleum", 4));
            expectedOutputFromQuery.Add(new GasStations(null, "Alauša", 5));
            //Act
            List<GasStations> queryResult = RetrieveGasStations.getGasStations();
            //Assert.IsTrue(queryResult.SequenceEqual(expectedOutputFromQuery));
            // we cannot use SequenceEqual because it compares the objects by reference, not by value
            // we need to compare the objects by value
            // we can use LINQ to compare the objects by value
            // for example, we can compare the objects by their Ids
            // assert if count is not equal
            //Assert
            Assert.IsTrue(queryResult.Count == expectedOutputFromQuery.Count);
            for (int i = 0; i < queryResult.Count; i++)
            {
                Assert.IsTrue(queryResult[i].getID() == expectedOutputFromQuery[i].getID());
                Assert.IsTrue(queryResult[i].getName() == expectedOutputFromQuery[i].getName());
            }
            // now lets do from the start
            // arrange
            // act
            // assert
            
        }
    }
}
