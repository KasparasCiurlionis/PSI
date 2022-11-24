using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject;
using WebProject.Data;

namespace WebProjectTests
{
    [TestClass()]
    public class ExistingGasStationServiceTests
    {

        
        private SelectedGasStationStatus _getSelectedGasStationStatus;

        [TestInitialize]
        public void Setup()
        {
            _getSelectedGasStationStatus = new SelectedGasStationStatus();
        }

        [TestMethod()]
        public void GetSelectedGasStationStatusTest_CircleKGasStation_StatusIsRecieved()
        {
            //Assign
            string gasStation = "Circle_K";

            //Act
            var result = SelectedGasStationStatus.CircleKGas;

            //Assert
            Assert.IsTrue(result.Equals(SelectedGasStationStatus.CircleKGas));

        }

        [TestMethod()]
        public void GetSelectedGasStationStatusTest_CircleKGasStation_ArrayOfGasTypesStringsRecieved()
        {
            //Assign
            string gasStation = "Circle_K";

            //Act
            var result = SelectedGasStationStatus.CircleKGas;
            var result2 = _getSelectedGasStationStatus.GetGasTypes(result);

            string[] expected = { "95", "98", "D", "LPG" };

            //Assert
            Assert.IsTrue(result2.SequenceEqual(expected));

        }

    }
}
